using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
   File: GameManager.cs
   Last Modified: March 9, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// The class represents the game manager.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The list of questions for stage.
    /// </summary>
    // /// <typeparam name="Questions">An instance of the Questions class.</typeparam>
    private List<Questions> questions = new List<Questions>();

    /// <summary>
    /// The random question to be answered.
    /// </summary>
    private int randomQuestionNumber = 0;

    /// <summary>
    /// The number of questions that have been answered correctly.
    /// </summary>
    public static int numberOfQuestionsRight = 0;

    /// <summary>
    /// The number of questions needed to be answered in stage.
    /// </summary>
    public static int numberOfQuestionsNeeded = 5;

    /// <summary>
    /// Represents a pseudo-random number generator.
    /// </summary>
    private System.Random rnd = new System.Random();

    /// <summary>
    /// A TextMeshProUGUI component from the question GameObject in Heirarchy that will be filled with the question to be answered.
    /// </summary>
    private TextMeshProUGUI questionT;

    /// <summary>
    /// A TextMeshProUGUI component from the anser GameObjects in Heirarchy that will be filled with an answer to the question.
    /// </summary>
    private TextMeshProUGUI answer1T, answer2T, answer3T, answer4T;

    /// <summary>
    /// The TextMeshProUGUI component from the number of correct answers GameObject in Heirarchy that will be filled
    /// with the number of correct answers in stage.
    /// </summary>
    private TextMeshProUGUI numberCorrect;

    /// <summary>
    /// The TextMeshProUGUI component from the number of answers needed GameObject in Heirarchy that will be filled
    /// with the number of answers needed in stage.
    /// </summary>
    private TextMeshProUGUI numberNeeded;

    /// <summary>
    /// The selected button.
    /// </summary>
    private GameObject selectedButton;

    /// <summary>
    /// An instance of the Question class.
    /// </summary>
    private Questions.Question questionScript = new Questions.Question();

    /// <summary>
    /// Tracks the current stage number.
    /// </summary>
    public static int currentStageNumber = 0;

    /// <summary>
    /// Tracks the current stage number for EndGame.unity Scene.
    /// </summary>
    public static int endGameStageNumber = 0;

    /// <summary>
    /// Will be used to control the player.
    /// </summary>
    private PlayerController playerController;

    /// <summary>
    /// The stage number displayed to screen.
    /// </summary>
    private TextMeshProUGUI stageDisplay;

    /// <summary>
    /// The score to increment in Stage.
    /// </summary>
    [SerializeField] private int scoreIncrement = 5;

    /// <summary>
    /// The score to decrement in Stage.
    /// </summary>
    [SerializeField] private int scoreDecrement = 2;

    /// <summary>
    /// The percentage to move the stage progress slider to.
    /// </summary>
    public static int sliderPercentageTo = 20;

    /// <summary>
    /// The duration to move the stage progress slider from its current position to sliderPercentageTo.
    /// </summary>
    public static int sliderDuration = 30;

    /// <summary>
    /// The percentage to move the stage progress slider from.
    /// </summary>
    public static int sliderPercentageFrom = 0;

    /// <summary>
    /// The Coroutine that will hold ones that will need to be stopped during certain times.
    /// </summary>
    private Coroutine coroutine;

    /// <summary>
    /// Start is called before the first frame update. Initializes TextMeshProUGUI components and first stage of game.
    /// Loads all questions from .csv files for each Stage of game.
    /// </summary>
    void Awake()
    {
        //Reset the last Player's score to 0 and Stage Finishes to false.
        SaveSystem.SetInt(name: "PlayerScore", val: 0);
        SaveSystem.SetBool(name: "StageFinish", val: false);

        //Grab the PlayerController from the Scene and check if null
        playerController = FindAndInit.InitializeGameObject(gameObjectName: "Player", scriptName: "GameManager.cs").GetComponent<PlayerController>();

        //Initialize TextMeshProUGUI components
        questionT = FindAndInit.InitializeTextMeshProUGUI(gameObjectName: "QuestionText", scriptName: "GameManager.cs");
        answer1T = FindAndInit.InitializeTextMeshProUGUI(gameObjectName: "Answer1Text", scriptName: "GameManager.cs");
        answer2T = FindAndInit.InitializeTextMeshProUGUI(gameObjectName: "Answer2Text", scriptName: "GameManager.cs");
        answer3T = FindAndInit.InitializeTextMeshProUGUI(gameObjectName: "Answer3Text", scriptName: "GameManager.cs");
        answer4T = FindAndInit.InitializeTextMeshProUGUI(gameObjectName: "Answer4Text", scriptName: "GameManager.cs");
        numberCorrect = FindAndInit.InitializeTextMeshProUGUI(gameObjectName: "NumberCorrect", scriptName: "GameManager.cs");
        numberNeeded = FindAndInit.InitializeTextMeshProUGUI(gameObjectName: "NumberNeeded", scriptName: "GameManager.cs");
        stageDisplay = FindAndInit.InitializeTextMeshProUGUI(gameObjectName: "stageNumberDisplay", scriptName: "MenuScene.cs");

        //Load Questions
        LoadQuestions();

        //Begin game by handling stage
        numberOfQuestionsRight = 0;
        currentStageNumber = 0;
        endGameStageNumber = 0;
        HandleStage(stageNumber: currentStageNumber);
        stageDisplay.text = currentStageNumber.ToString();
    }

    /// <summary>
    /// Update is called once per frame. Checks correct questions are met before moving to next stage.
    /// Updates the number of correct answers on UI. Checks to see if lost game. Sets TopPlayerScore
    /// if current score is greater. Changes Scene to LostGame accordingly.
    /// </summary>
    void Update()
    {
        numberCorrect.text = numberOfQuestionsRight.ToString();
        numberNeeded.text = numberOfQuestionsNeeded.ToString( );
        stageDisplay.text = currentStageNumber.ToString();

        if( currentStageNumber != -1 && currentStageNumber != 2 )
            CheckForStageCompletion();
    }

    /// <summary>
    /// Logic to proceed to the next stage of the game. Resets questions answered correctly, increments stage number on,
    /// sets up the progress bar for current stage, and handles stage questions.
    /// </summary>
    void ProceedToNextStage()
    {
        currentStageNumber++;
        endGameStageNumber++;
        numberOfQuestionsRight = 0;
        ProgressBar.Instance.StopAllCoroutines( );
        ProgressBar.Instance.SetupStage(stageNumber: currentStageNumber);
        HandleStage(stageNumber: currentStageNumber);
    }

    /// <summary>
    /// Method that checks to see if the current stage is completed correctly and can move on, or if 
    /// it has not been completed and needs to stop game.
    /// </summary>
    public void CheckForStageCompletion()
    {
        // Check if the current stage's objectives are met
        bool objectivesMet = ( numberOfQuestionsRight >= numberOfQuestionsNeeded ) && (ProgressBar.slider.value < sliderPercentageTo) && ( Play1Scene.doneWaiting == true );
        if ( objectivesMet )
        {
            ProceedToNextStage();
        }
        else if ( SaveSystem.GetBool( name: "StageFinish" ) && !objectivesMet )
        {
            currentStageNumber = -1;
            numberOfQuestionsRight = 0;
            HandleStage( stageNumber: currentStageNumber );
        }
    }

    /// <summary>
    /// Handles each stage or level of the game.
    /// </summary>
    /// <param name="stageNumber">The stage or level number to be handled.</param>
    private void HandleStage(int stageNumber)
    {
        switch (stageNumber)
        {
            case -1:
                //Current lost game
                if( coroutine != null )
                    StopCoroutine( routine: coroutine );
                    
                if( ProgressBar.Instance != null )
                    Destroy( obj: ProgressBar.Instance );

                SaveSystem.SetInt( name: "LastPlayerScore", val: SaveSystem.GetInt(name: "PlayerScore") );
                Debug.Log(message: "Lost game at stage " + endGameStageNumber + " !");
                if ((SaveSystem.GetInt(name: "LastPlayerScore") <= 0 && SaveSystem.GetInt(name: "TopPlayerScore") == 0)
                    || (SaveSystem.GetInt(name: "LastPlayerScore") > SaveSystem.GetInt(name: "TopPlayerScore"))
                )
                {
                    SaveSystem.SetInt(name: "TopPlayerScore", val: SaveSystem.GetInt(name: "LastPlayerScore"));
                    SaveSystem.SetString( name: "TopPlayerName", val: SaveSystem.GetString(name: "LastPlayerName" ) );
                }
                SceneTransitions.EndGameScene(won: false);
                break;
            case 0:
                //Stage 0
                numberOfQuestionsNeeded = 5;
                LoadQuestions();
                SaveSystem.SetBool(name: "StageFinish", val: false);
                if ( questions[index: currentStageNumber].stageQuestions.Count > 2 )
                {
                    AskQuestion();
                }
                break;
            case 1:
                //Stage 1
                SaveSystem.SetInt( name: "LastPlayerScore", val: SaveSystem.GetInt(name: "PlayerScore") );
                numberOfQuestionsNeeded = 5;
                SaveSystem.SetBool(name: "StageFinish", val: false);
                break;
            case 2:
                if( coroutine != null )
                    StopCoroutine( routine: coroutine );

                if( ProgressBar.Instance != null )
                    Destroy( obj: ProgressBar.Instance );

                SaveSystem.SetInt( name: "LastPlayerScore", val: SaveSystem.GetInt(name: "PlayerScore") );
                Debug.Log(message: "Won game at stage " + endGameStageNumber + " !");
                if ((SaveSystem.GetInt(name: "LastPlayerScore") <= 0 && SaveSystem.GetInt(name: "TopPlayerScore") == 0)
                    || (SaveSystem.GetInt(name: "LastPlayerScore") > SaveSystem.GetInt(name: "TopPlayerScore"))
                )
                {
                    SaveSystem.SetInt(name: "TopPlayerScore", val: playerController.player.GetScore());
                    SaveSystem.SetString(name: "TopPlayerName", val: playerController.player.GetPlayerName());
                }
                SceneTransitions.EndGameScene(won: true);
                break;
            default:
                Debug.Log(message: "Please enter valid stage number!");
                SceneTransitions.MainMenuScene();
                break;
        }
    }

    /// <summary>
    /// Finds the path to each .csv and then adds a Questions object to questions List for each.
    /// </summary>
    private void LoadQuestions()
    {
        string basePath;
        if (Application.isEditor)
        {
            basePath = Path.Combine(Application.dataPath, "Questions");
        }
        else
        {
            basePath = Path.Combine(Application.streamingAssetsPath, "Questions");
        }
        Questions q0 = new Questions();
        questions.Add(item: q0);
        Questions q1 = new Questions();
        questions.Add(item: q1);
        ReadCSVAndStore(csvPath: Path.Combine(basePath, "Stage0.csv"), csv: "Stage0.csv");
        ReadCSVAndStore(csvPath: Path.Combine(basePath, "Stage1.csv"), csv: "Stage1.csv");
        new Questions().RandomizeQuestions(questionsList: questions);
    }

    /// <summary>
    /// Reads a .csv and stores it into a Stage for the game.
    /// </summary>
    /// <param name="csvPath">Path to where .csv files are located in game directory.</param>
    /// <param name="csv">The name of the .csv file to read</param>
    private void ReadCSVAndStore(string csvPath, string csv)
    {
        //initialize reader
        StreamReader reader = new StreamReader(path: csvPath);

        //discard header
        string lineRead = reader.ReadLine();

        //loop through questions and answers in csv and store in List
        while ((lineRead = reader.ReadLine()) != "//.end.//")
        {
            string[] values = lineRead.Split(separator: '^');
            Questions.Question q = new Questions.Question();
            q.SetQuestionText(text: values[0]);
            q.SetCorrectAnswer(text: values[1]);
            for (int i = 1; i < 5; i++)
            {
                q.AddAnswer(text: values[i]);
            }
            switch (csv)
            {
                case "Stage0.csv":
                    questions[index: 0].stageQuestions.Add(item: q);
                    break;
                case "Stage1.csv":
                    questions[index: 1].stageQuestions.Add(item: q);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Asks a random question from List and randomizes answers.
    /// </summary>
    private void AskQuestion()
    {
        if (questions[index: currentStageNumber].stageQuestions.Count > 2 )
        {
            randomQuestionNumber = rnd.Next(minValue: 0, maxValue: questions.Count - 1);
            questionT.text = questions[index: currentStageNumber].stageQuestions[index: randomQuestionNumber].GetQuestionText();
            questionScript.RandomizeAnswers(q: questions[index: currentStageNumber].stageQuestions[index: randomQuestionNumber]);
            answer1T.text = questions[index: currentStageNumber].stageQuestions[index: randomQuestionNumber].GetAnswer(i: 0).GetAnswerText();
            answer2T.text = questions[index: currentStageNumber].stageQuestions[index: randomQuestionNumber].GetAnswer(i: 1).GetAnswerText();
            answer3T.text = questions[index: currentStageNumber].stageQuestions[index: randomQuestionNumber].GetAnswer(i: 2).GetAnswerText();
            answer4T.text = questions[index: currentStageNumber].stageQuestions[index: randomQuestionNumber].GetAnswer(i: 3).GetAnswerText();
        }
        else
        {
            currentStageNumber = -1;
            numberOfQuestionsRight = 0;
            HandleStage( stageNumber: currentStageNumber );
        }
    }

    /// <summary>
    /// Checks to see if the answer selected is the correct answer from the current Question. Then, it removes the 
    /// question and asks a new one. Updates current player score accordingly.
    /// </summary>
    /// <param name="incomingAnswerText">The text of the answer selected to be compared with the text of the correct Answer.</param>
    public void CheckAnswer(string incomingAnswerText)
    {
        selectedButton = FindAndInit.InitializeGameObject(gameObjectName: incomingAnswerText, scriptName: "GameManager.cs");
        if ( questions[index: currentStageNumber].stageQuestions.Count > 2 )
        {
            int currentScore;
            if (questions[index: currentStageNumber].stageQuestions[index: randomQuestionNumber].GetCorrectAnswer().GetAnswerText() == selectedButton.GetComponent<TextMeshProUGUI>().text)
            {
                Debug.Log(message: "Found correct answer!");
                numberOfQuestionsRight++;
                DisplayTrue();
                currentScore = playerController.player.GetScore();
                playerController.player.SetScore(currentScore += scoreIncrement);
            }
            else
            {
                Debug.Log(message: "Incorrect answer!");
                DisplayFalse();
                currentScore = playerController.player.GetScore();
                playerController.player.SetScore(currentScore -= scoreDecrement);
            }
        }
    }

    /// <summary>
    /// Removes a question from the List.
    /// </summary>
    private void RemoveQuestion()
    {
        if( currentStageNumber != -1 && currentStageNumber != 2 )
            questions[index: currentStageNumber].stageQuestions.Remove(item: questions[index: currentStageNumber].stageQuestions[index: randomQuestionNumber]);
    }

    /// <summary>
    /// Displays the visuals for selecting a true answer.
    /// </summary>
    private void DisplayTrue()
    {
        if( coroutine != null )
            StopCoroutine( routine: coroutine );
        coroutine = StartCoroutine(routine: WaitForColor(timeInSeconds: 0.1f, color: "green"));
        RemoveQuestion();
    }

    /// <summary>
    /// Displays the visuals for selecting a false answer.
    /// </summary>
    private void DisplayFalse()
    {
        if( coroutine != null )
            StopCoroutine( routine: coroutine );
        coroutine = StartCoroutine(routine: WaitForColor(timeInSeconds: 0.1f, color: "red"));
        RemoveQuestion();
    }

    /// <summary>
    /// Waits for an alloted time before switching button color back to its original color.
    /// </summary>
    /// <param name="timeInSeconds">The time button will wait before returning to original color.</param>
    /// <param name="color">The color to turn the button.</param>
    IEnumerator WaitForColor(float timeInSeconds, string color)
    {
        Color origColor = selectedButton.GetComponentInParent<Image>().color;

        if (color == "green")
        {
            selectedButton.GetComponentInParent<Image>().color = Color.green;
            numberCorrect.color = Color.green;
            playerController.playerScoreText.color = Color.green;
        }
        else if (color == "red")
        {
            selectedButton.GetComponentInParent<Image>().color = Color.red;
            numberCorrect.color = Color.red;
            playerController.playerScoreText.color = Color.red;
        }

        yield return new WaitForSeconds(seconds: timeInSeconds);

        if( selectedButton )
        {
            selectedButton.GetComponentInParent<Image>().color = origColor;
            numberCorrect.color = origColor;
            playerController.playerScoreText.color = origColor; 
        }

        // RemoveQuestion();

        if ( currentStageNumber != -1 && currentStageNumber != 2 )
        {
            AskQuestion();
        }           
        EventSystem.current.SetSelectedGameObject(selected: null);
    }
}
