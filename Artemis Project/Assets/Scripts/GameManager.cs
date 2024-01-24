using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

 /*
    File: GameManager.cs
    Description: Manages the core functions of the trivia game.
    Last Modified: January 24, 2024
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
    /// A TextMeshProUGUI component that will be filled with the question to be answered.
    /// </summary>
    private TextMeshProUGUI questionT;

    /// <summary>
    /// A TextMeshProUGUI component that will be filled with an answer to the question.
    /// </summary>
    private TextMeshProUGUI answer1T, answer2T, answer3T, answer4T, numberCorrect;

    /// <summary>
    /// The selected button.
    /// </summary>
    GameObject selectedButton;

    /// <summary>
    /// An instance of the Question class.
    /// </summary>
    private Question questionScript = new Question( );

    /// <summary>
    /// Tracks the current stage number.
    /// </summary>
    private int currentStageNumber = 0;

    /// <summary>
    /// Start is called before the first frame update. Initializes TextMeshProUGUI components and first stage of game.
    /// </summary>
    void Start( )
    {
        questionT = GameObject.Find( "QuestionText" ).GetComponent< TextMeshProUGUI >( );
        answer1T = GameObject.Find( "Answer1Text" ).GetComponent< TextMeshProUGUI >( );
        answer2T = GameObject.Find( "Answer2Text" ).GetComponent< TextMeshProUGUI >( );
        answer3T = GameObject.Find( "Answer3Text" ).GetComponent< TextMeshProUGUI >( );
        answer4T = GameObject.Find( "Answer4Text" ).GetComponent< TextMeshProUGUI >( );
        numberCorrect = GameObject.Find( "NumberCorrect" ).GetComponent< TextMeshProUGUI >( );
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
            Debug.Log( "Answered 5 questions correctly." );
            ResetTrivia( );
            currentStageNumber++;
            numberOfQuestionsRight = 0;
            HandleStage( stageNumber: currentStageNumber );
        }
        else if( numberOfQuestionsRight < 5 && questions.Count == 0 )
        {
            Debug.Log( "Lost game at stage " + currentStageNumber + " !" );
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
            case 0: 
                ReadCSVAndStore( csvToRead: "./Assets/Questions/Stage0.csv" );
                AskQuestion( );
                break;
            case 1: 
                Debug.Log( "Now on stage 1!" );
                ReadCSVAndStore( csvToRead: "./Assets/Questions/Stage1.csv" );
                AskQuestion( );
                break;
            default:
                Debug.Log( "Please enter valid stage number!" );
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
        StreamReader reader = new StreamReader( csvToRead );

        //clear previous questions from List
        questions.Clear( );

        //discard header
        string lineRead = reader.ReadLine( );
        
        //loop through questions and answers in csv and store in List
        while( ( lineRead = reader.ReadLine( ) ) != "//.end.//" )
        {
            string[] values = lineRead.Split( '^' );
            Question q = new Question( );
            q.SetQuestionText( values[ 0 ] );
            q.SetCorrectAnswer( values[ 1 ] );
            for( int i = 1; i < 5; i++ )
            {
                q.AddAnswer( values[ i ] );
            }
            questions.Add( q );
        }
    }

    /// <summary>
    /// Asks a random question from List and randomizes answers.
    /// </summary>
    private void AskQuestion( )
    {
        if( questions.Count > 0 )
        {
            randomQuestionNumber = rnd.Next( 0, questions.Count );
            // Debug.Log( questions.Count + " questions left!" );
            questionT.text = questions[ randomQuestionNumber ].GetQuestionText( );
            questionScript.RandomizeAnswers( questions[ randomQuestionNumber ] );
            answer1T.text = questions[ randomQuestionNumber ].GetAnswer( 0 ).text;
            answer2T.text = questions[ randomQuestionNumber ].GetAnswer( 1 ).text;
            answer3T.text = questions[ randomQuestionNumber ].GetAnswer( 2 ).text;
            answer4T.text = questions[ randomQuestionNumber ].GetAnswer( 3 ).text;
        } 
    }

    /// <summary>
    /// Checks to see if the answer selected is the correct answer from the current Question. Then, it removes the question and asks a new one.
    /// </summary>
    /// <param name="incomingAnswerText">The text of the answer selected to be compared with the text of the correct Answer.</param>
    public void CheckAnswer( string incomingAnswerText )
    {   
        selectedButton = GameObject.Find( incomingAnswerText );
        if( questions.Count > 0 )
        {
            if( questions[ randomQuestionNumber ].GetCorrectAnswer( ).text == selectedButton.GetComponent< TextMeshProUGUI >( ).text )
            {
                Debug.Log( "Found correct answer!" );
                numberOfQuestionsRight++;
                DisplayTrue( );
                //managepoints()  
            }
            else
            {
                Debug.Log( "Incorrect answer!" );
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
        questions.Remove( questions[ randomQuestionNumber ] );
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
    /// Prints the questions within the current stage or level.
    /// </summary>
    private void PrintStageQuestions( )
    {
        for ( int i = 0; i < questions.Count; i++ ) 
        {
            Debug.Log( "Question: " + questions[ i ].GetQuestionText( ) + "\n" );
        }
    }

    /// <summary>
    /// Displays the visuals for selecting a true answer.
    /// </summary>
    private void DisplayTrue( )
    {
        selectedButton.GetComponentInParent< Image >( ).color = Color.green;
        numberCorrect.color = Color.green;
        StartCoroutine( WaitForColor( timeInSeconds: 0.2f ) );
    }

    /// <summary>
    /// Displays the visuals for selecting a false answer.
    /// </summary>
    private void DisplayFalse( )
    {
        selectedButton.GetComponentInParent< Image >( ).color = Color.red;
        numberCorrect.color = Color.red;
        StartCoroutine( WaitForColor( timeInSeconds: 0.2f ) );
    }

    /// <summary>
    /// Waits for an alloted time before switching button color back to its original color.
    /// </summary>
    /// <param name="timeInSeconds">The time button will wait before returning to original color.</param>
    /// <returns></returns>
    IEnumerator WaitForColor( float timeInSeconds )
    {
        yield return new WaitForSeconds( timeInSeconds );
        selectedButton.GetComponentInParent< Image >( ).color = Color.white;
        numberCorrect.color = new Color( 108f, 126f, 162f );
    }
}
