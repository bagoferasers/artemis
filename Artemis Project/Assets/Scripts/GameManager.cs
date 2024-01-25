using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

 /*
    File: GameManager.cs
    Description: Manages the core functions of the trivia game.
    Last Modified: January 25, 2024
    Last Modified By: Colby Bailey
 */

/// <summary>
/// The class represents the game manager.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The list of questions for the current stage.
    /// </summary>
    /// <typeparam name="Question">An instance of the Question class.</typeparam>
    private List< Question > questions = new List< Question >( );

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
    /// The question GameObject in Heirarchy.
    /// </summary>
    private GameObject questionTGO;

    /// <summary>
    /// A TextMeshProUGUI component from the question GameObject in Heirarchy that will be filled with the question to be answered.
    /// </summary>
    private TextMeshProUGUI questionT;

    /// <summary>
    /// The answer GameObjects in Heirarchy.
    /// </summary>
    private GameObject answer1GO, answer2GO, answer3GO, answer4GO;

    /// <summary>
    /// A TextMeshProUGUI component from the anser GameObjects in Heirarchy that will be filled with an answer to the question.
    /// </summary>
    private TextMeshProUGUI answer1T, answer2T, answer3T, answer4T;

    /// <summary>
    /// The number of correct answers GameObject in Heriarchy.
    /// </summary>
    private GameObject numberCorrectGO;

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
    /// Start is called before the first frame update. Initializes TextMeshProUGUI components and first stage of game.
    /// </summary>
    void Start( )
    {
        //Grab GameObjects and check if null
        questionTGO = GameObject.Find( name: "QuestionText" );
        if( questionTGO == null )
        {
            Debug.LogWarning( message: "questionTGO variable in GameManager.cs is null!" , context: gameObject );
            Application.Quit( );
        }

        answer1GO = GameObject.Find( name: "Answer1Text" );
        if( answer1GO == null )
        {
            Debug.LogWarning( message: "answer1GO variable in GameManager.cs is null!" , context: gameObject );
            Application.Quit( );
        }

        answer2GO = GameObject.Find( name: "Answer2Text" );
        if( answer2GO == null )
        {
            Debug.LogWarning( message: "answer2GO variable in GameManager.cs is null!" , context: gameObject );
            Application.Quit( );
        }

        answer3GO = GameObject.Find( name: "Answer3Text" );
        if( answer3GO == null )
        {
            Debug.LogWarning( message: "answer3GO variable in GameManager.cs is null!" , context: gameObject );
            Application.Quit( );
        }

        answer4GO = GameObject.Find( name: "Answer4Text" );
        if( answer4GO == null )
        {
            Debug.LogWarning( message: "answer4GO variable in GameManager.cs is null!" , context: gameObject );
            Application.Quit( );
        }

        numberCorrectGO = GameObject.Find( name: "NumberCorrect" );
        if( numberCorrectGO == null )
        {
            Debug.LogWarning( message: "numberCorrectGO variable in GameManager.cs is null!" , context: gameObject );
            Application.Quit( );
        }

        //Grab TextMeshProUGUI components
        questionT = questionTGO.GetComponent< TextMeshProUGUI >( );
        answer1T = answer1GO.GetComponent< TextMeshProUGUI >( );
        answer2T = answer2GO.GetComponent< TextMeshProUGUI >( );
        answer3T = answer3GO.GetComponent< TextMeshProUGUI >( );
        answer4T = answer4GO.GetComponent< TextMeshProUGUI >( );
        numberCorrect = numberCorrectGO.GetComponent< TextMeshProUGUI >( );
        
        //Begin game by handling stage
        HandleStage( stageNumber: currentStageNumber );
    }

    /// <summary>
    /// Update is called once per frame. Checks correct questions are met before moving to next stage.
    /// Updates the number of correct answers on UI. Checks to see if lost game.
    /// </summary>
    void Update( )
    {
        numberCorrect.text = numberOfQuestionsRight.ToString( );

        if( numberOfQuestionsRight == 5 )
        {
            Debug.Log( message: "Answered 5 questions correctly." );
            ResetTrivia( );
            currentStageNumber++;
            numberOfQuestionsRight = 0;
            HandleStage( stageNumber: currentStageNumber );
        }
        else if( numberOfQuestionsRight < 5 && questions.Count == 0 )
        {
            Debug.Log( message: "Lost game at stage " + currentStageNumber + " !" );
        }
    }

    /// <summary>
    /// Handles each stage or level of the game.
    /// </summary>
    /// <param name="stageNumber">The stage or level number to be handled.</param>
    private void HandleStage( int stageNumber )
    {
        Debug.Log( message: "Now on stage " + stageNumber + "!" );
        switch( stageNumber )
        {
            case 0: 
                ReadCSVAndStore( csvToRead: "./Assets/Questions/Stage0.csv" );
                AskQuestion( );
                break;
            case 1: 
                ReadCSVAndStore( csvToRead: "./Assets/Questions/Stage1.csv" );
                AskQuestion( );
                break;
            default:
                Debug.Log( message: "Please enter valid stage number!" );
                break;
        }
    }

    /// <summary>
    /// Reads the incoming csv and adds the questions to the stage or level.
    /// </summary>
    /// <param name="csvToRead">The csv of the current stage or level.</param>
    private void ReadCSVAndStore( string csvToRead )
    {
        //initialize reader
        StreamReader reader = new StreamReader( path: csvToRead );

        //clear previous questions from List
        questions.Clear( );

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
            questions.Add( item: q );
        }
    }

    /// <summary>
    /// Asks a random question from List and randomizes answers.
    /// </summary>
    private void AskQuestion( )
    {
        if( questions.Count > 0 )
        {
            randomQuestionNumber = rnd.Next( minValue: 0, maxValue: questions.Count );
            // Debug.Log( questions.Count + " questions left!" );
            questionT.text = questions[ index: randomQuestionNumber ].GetQuestionText( );
            questionScript.RandomizeAnswers( q: questions[ index: randomQuestionNumber ] );
            answer1T.text = questions[ index: randomQuestionNumber ].GetAnswer( i: 0 ).GetAnswerText( );
            answer2T.text = questions[ index: randomQuestionNumber ].GetAnswer( i: 1 ).GetAnswerText( );
            answer3T.text = questions[ index: randomQuestionNumber ].GetAnswer( i: 2 ).GetAnswerText( );
            answer4T.text = questions[ index: randomQuestionNumber ].GetAnswer( i: 3 ).GetAnswerText( );
        } 
    }

    /// <summary>
    /// Checks to see if the answer selected is the correct answer from the current Question. Then, it removes the question and asks a new one.
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

        if( questions.Count > 0 )
        {
            if( questions[ index: randomQuestionNumber ].GetCorrectAnswer( ).GetAnswerText( ) == selectedButton.GetComponent< TextMeshProUGUI >( ).text )
            {
                Debug.Log( message: "Found correct answer!" );
                numberOfQuestionsRight++;
                DisplayTrue( );
                //managepoints()  
            }
            else
            {
                Debug.Log( message: "Incorrect answer!" );
                DisplayFalse( );
                //managepoints()
            }
            RemoveQuestion( );
            AskQuestion( );            
        }
    }

    /// <summary>
    /// Removes a question from the List.
    /// </summary>
    private void RemoveQuestion( )
    {
        questions.Remove( item: questions[ index: randomQuestionNumber ] );
    }

    /// <summary>
    /// Resets the TextMeshProUGUI components to the default values and clears questions in List.
    /// </summary>
    private void ResetTrivia( )
    {
        questions.Clear( );
        questionT.text = "This will be the question";
        answer1T.text = "Default answer";
        answer2T.text = "Default answer";
        answer3T.text = "Default answer";
        answer4T.text = "Default answer";      
    }

    /// <summary>
    /// Used for testing. Prints the questions within the current stage or level.
    /// </summary>
    private void PrintStageQuestions( )
    {
        for ( int i = 0; i < questions.Count; i++ ) 
        {
            Debug.Log( message: "Question: " + questions[ index: i ].GetQuestionText( ) + "\n" );
        }
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
    IEnumerator WaitForColor( float timeInSeconds )
    {
        yield return new WaitForSeconds( seconds: timeInSeconds );
        selectedButton.GetComponentInParent< Image >( ).color = Color.white;
        numberCorrect.color = new Color( r: 108f, g: 126f, b: 162f );
    }
}
