using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

/*
   File: GameManager.cs
   Description: Manages the core functions of the trivia game.
   Last Modified: February 7, 2024
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
    private List< Questions > questions = new List< Questions >( );

    /// <summary>
    /// The random question to be answered.
    /// </summary>
    private int randomQuestionNumber = 0;

    /// <summary>
    /// The number of questions that have been answered correctly.
    /// </summary>
    private int numberOfQuestionsRight = 0;

    /// <summary>
    /// Represents a pseudo-random number generator.
    /// </summary>
    private System.Random rnd = new System.Random( );

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
    /// The selected button.
    /// </summary>
    private GameObject selectedButton;

    /// <summary>
    /// An instance of the Question class.
    /// </summary>
    private Question questionScript = new Question( );

    /// <summary>
    /// Tracks the current stage number.
    /// </summary>
    [ SerializeField ] private int currentStageNumber = 0;

    /// <summary>
    /// Will be used to control the player.
    /// </summary>
    private PlayerController playerController;

    /// <summary>
    /// Start is called before the first frame update. Initializes TextMeshProUGUI components and first stage of game.
    /// Loads all questions from .csv files for each Stage of game.
    /// </summary>
    void Start( )
    {
        //Reset the last Player's score to 0.
        SaveSystem.SetInt( name: "LastPlayerScore", val: 0 );
        SaveSystem.SaveToDisk( );

        //Grab the PlayerController from the Scene and check if null
        playerController = FindAndInit.InitializeGameObject( gameObjectName: "Player", sceneName: "GameManager.cs" ).GetComponent< PlayerController >( );

        //Initialize TextMeshProUGUI components
        questionT = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "QuestionText", sceneName: "GameManager.cs" );
        answer1T = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "Answer1Text", sceneName: "GameManager.cs" );
        answer2T = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "Answer2Text", sceneName: "GameManager.cs" );
        answer3T = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "Answer3Text", sceneName: "GameManager.cs" );
        answer4T = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "Answer4Text", sceneName: "GameManager.cs" );
        numberCorrect = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "NumberCorrect", sceneName: "GameManager.cs" );

        //Load Questions
        LoadQuestions( );
        
        //Begin game by handling stage
        HandleStage( stageNumber: currentStageNumber );
    }

    /// <summary>
    /// Update is called once per frame. Checks correct questions are met before moving to next stage.
    /// Updates the number of correct answers on UI. Checks to see if lost game. Sets TopPlayerScore
    /// if current score is greater. Changes Scene to LostGame accordingly.
    /// </summary>
    void Update( )
    {
        numberCorrect.text = numberOfQuestionsRight.ToString( );

        if( numberOfQuestionsRight == 5 )
        {
            numberOfQuestionsRight = 0;
            HandleStage( stageNumber: ++currentStageNumber );
        }
        else if( numberOfQuestionsRight < 5 && questions[ index: currentStageNumber ].stageQuestions.Count == 0 )
        {
            HandleStage( stageNumber: -1 );
        }
    }

    /// <summary>
    /// Handles each stage or level of the game.
    /// </summary>
    /// <param name="stageNumber">The stage or level number to be handled.</param>
    private void HandleStage( int stageNumber )
    {
        switch( stageNumber )
        {
            case -1:
                //Current lost game
                Debug.Log( message: "Lost game at stage " + currentStageNumber + " !" );
                PlayerPrefs.SetInt( key: "LastPlayerScore", value: playerController.player.GetScore( ) );
                if( playerController.player.GetScore( ) > PlayerPrefs.GetInt( key: "TopPlayerScore" ) )
                {
                    SaveSystem.SetInt( name: "TopPlayerScore", val: playerController.player.GetScore( ) );
                    SaveSystem.SaveToDisk( );
                }
                SceneTransitions.LostGameScene( );
                break;
            case 0: 
                //Stage 0
                if( questions[ index: currentStageNumber ].stageQuestions.Count > 0 )
                {
                    AskQuestion( );
                }
                break;
            case 1:
                //Current won game. Update case # to always be the last before default.
                Debug.Log( message: "Won game at stage " + currentStageNumber + " !" );
                PlayerPrefs.SetInt( key: "LastPlayerScore", value: playerController.player.GetScore( ) );
                if( playerController.player.GetScore( ) > PlayerPrefs.GetInt( key: "TopPlayerScore" ) )
                {
                    SaveSystem.SetInt( name: "TopPlayerScore", val: playerController.player.GetScore( ) );
                    SaveSystem.SaveToDisk( );
                }
                SceneTransitions.WonGameScene( );
                break;
            default:
                Debug.Log( message: "Please enter valid stage number!" );
                SceneTransitions.MainMenuScene( );
                break;
        }
    }

    /// <summary>
    /// Finds the path to each .csv and then adds a Questions object to questions List for each.
    /// </summary>
    private void LoadQuestions( )
    {
        string basePath;
        if( Application.isEditor )
        {
            basePath = Path.Combine( Application.dataPath, "Questions" );
        }
        else
        {
            basePath = Path.Combine( Application.streamingAssetsPath, "Questions" );
        }
        Questions q0 = new Questions( );
        questions.Add( item: q0 );
        Questions q1 = new Questions( );
        questions.Add( item: q1 );
        ReadCSVAndStore( csvPath: Path.Combine( basePath, "Stage0.csv" ), csv: "Stage0.csv" );
        ReadCSVAndStore( csvPath: Path.Combine( basePath, "Stage1.csv" ), csv: "Stage1.csv" );
    }

    /// <summary>
    /// Reads a .csv and stores it into a Stage for the game.
    /// </summary>
    /// <param name="csvPath">Path to where .csv files are located in game directory.</param>
    /// <param name="csv">The name of the .csv file to read</param>
    private void ReadCSVAndStore( string csvPath, string csv )
    {
        //initialize reader
        StreamReader reader = new StreamReader( path: csvPath );

        //discard header
        string lineRead = reader.ReadLine( );
        
        //loop through questions and answers in csv and store in List
        while( ( lineRead = reader.ReadLine( ) ) != "//.end.//" )
        {
            string[] values = lineRead.Split( separator: '^' );
            Question q = new Question( );
            q.SetQuestionText( text: values[ 0 ] );
            q.SetCorrectAnswer( text: values[ 1 ] );
            for( int i = 1; i < 5; i++ )
            {
                q.AddAnswer( text: values[ i ] );
            }
            switch( csv )
            {
                case "Stage0.csv":
                    questions[ index: 0 ].stageQuestions.Add( item: q );
                    break;
                case "Stage1.csv":
                    questions[ index: 1 ].stageQuestions.Add( item: q );
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Asks a random question from List and randomizes answers.
    /// </summary>
    private void AskQuestion( )
    {
        if( questions[ index: currentStageNumber ].stageQuestions.Count > 0 )
        {
            randomQuestionNumber = rnd.Next( minValue: 0, maxValue: questions.Count - 1 );
            questionT.text = questions[ index: currentStageNumber ].stageQuestions[ index: randomQuestionNumber ].GetQuestionText( );
            questionScript.RandomizeAnswers( q: questions[ index: currentStageNumber ].stageQuestions[ index: randomQuestionNumber ] );
            answer1T.text = questions[ index: currentStageNumber ].stageQuestions[ index: randomQuestionNumber ].GetAnswer( i: 0 ).GetAnswerText( );
            answer2T.text = questions[ index: currentStageNumber ].stageQuestions[ index: randomQuestionNumber ].GetAnswer( i: 1 ).GetAnswerText( );
            answer3T.text = questions[ index: currentStageNumber ].stageQuestions[ index: randomQuestionNumber ].GetAnswer( i: 2 ).GetAnswerText( );
            answer4T.text = questions[ index: currentStageNumber ].stageQuestions[ index: randomQuestionNumber ].GetAnswer( i: 3 ).GetAnswerText( );
        } 
    }

    /// <summary>
    /// Checks to see if the answer selected is the correct answer from the current Question. Then, it removes the 
    /// question and asks a new one. Updates current player score accordingly.
    /// </summary>
    /// <param name="incomingAnswerText">The text of the answer selected to be compared with the text of the correct Answer.</param>
    public void CheckAnswer( string incomingAnswerText )
    {   
        selectedButton = GameObject.Find( name: incomingAnswerText );
        if( selectedButton == null )
        {
            Debug.LogWarning( message: "selectedButton variable in GameManager.cs is null!" , context: gameObject );
            Application.Quit( );
        }

        if( questions[ index: currentStageNumber ].stageQuestions.Count > 0 )
        {
            int currentScore;
            if( questions[ index: currentStageNumber ].stageQuestions[ index: randomQuestionNumber ].GetCorrectAnswer( ).GetAnswerText( ) == selectedButton.GetComponent< TextMeshProUGUI >( ).text )
            {
                Debug.Log( message: "Found correct answer!" );
                numberOfQuestionsRight++;
                DisplayTrue( );
                currentScore = playerController.player.GetScore( );
                playerController.player.SetScore( currentScore += 5 );
            }
            else
            {
                Debug.Log( message: "Incorrect answer!" );
                DisplayFalse( );
                currentScore = playerController.player.GetScore( );
                playerController.player.SetScore( currentScore -= 5 );
            }          
        }
    }

    /// <summary>
    /// Removes a question from the List.
    /// </summary>
    private void RemoveQuestion( )
    {
        questions[ index: currentStageNumber ].stageQuestions.Remove( item: questions[ index: currentStageNumber ].stageQuestions[ index: randomQuestionNumber ] );
    }

    /// <summary>
    /// Displays the visuals for selecting a true answer.
    /// </summary>
    private void DisplayTrue( )
    {
        selectedButton.GetComponentInParent< Image >( ).color = Color.green;
        numberCorrect.color = Color.green;
        StartCoroutine( routine: WaitForColor( timeInSeconds: 0.2f ) );
    }

    /// <summary>
    /// Displays the visuals for selecting a false answer.
    /// </summary>
    private void DisplayFalse( )
    {
        selectedButton.GetComponentInParent< Image >( ).color = Color.red;
        numberCorrect.color = Color.red;
        StartCoroutine( routine: WaitForColor( timeInSeconds: 0.2f ) );
    }

    /// <summary>
    /// Waits for an alloted time before switching button color back to its original color.
    /// </summary>
    /// <param name="timeInSeconds">The time button will wait before returning to original color.</param>
    /// <returns></returns>
    private IEnumerator WaitForColor( float timeInSeconds )
    {
        yield return new WaitForSeconds( seconds: timeInSeconds );
        selectedButton.GetComponentInParent< Image >( ).color = Color.white;
        numberCorrect.color = new Color( r: 108f, g: 126f, b: 162f );
        RemoveQuestion( );
        if( questions[ index: currentStageNumber ].stageQuestions.Count > 0 )
        {
            AskQuestion( );
        }
    }
}
