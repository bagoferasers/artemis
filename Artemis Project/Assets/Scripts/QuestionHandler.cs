using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class ReadQuestions : MonoBehaviour
{

    private List< string[] > questionsAndAnswers = new List< string[] >( );
    private System.Random rnd = new System.Random( );
    private int randomQuestionNumber, randomAnswerNumber, numberOfQuestionsRight = 0;
    private string correctAnswer;
    public BackgroundMove backgroundMove;
    
    // Start is called before the first frame update
    void Start( )
    {
        //start off game with questions on stage 0
        HandleStage( stageNumber: 0 );
    }

    void Update( )
    {
        if( numberOfQuestionsRight == 5 )
        {
            questionsAndAnswers.Clear( );
            backgroundMove.paused = false;
            CheckIfNoMoreQuestions( );
        }

        //when collide with next stage, start that stage
    }

    private void HandleStage( int stageNumber )
    {
        //read correct csv per stage number into List questionsAndAnswers
        switch( stageNumber )
        {
            case 0: 
                ReadCSVAndStore( csvToRead: "./Assets/Questions/PreLaunchQuestions.csv" );
                // PrintCurrentStage( );
                AskQuestion( );
                break;
            case 1: 
                // won game
                break;
            default:
                Debug.Log( "Please enter valid stage number!" );
                Application.Quit( );
                break;
        }
    }

    private void AskQuestion( )
    {
        //check if there are any questions left
        CheckIfNoMoreQuestions( );

        if( questionsAndAnswers.Count > 0 )
        {
            //generate random number between 0 and number left
            randomQuestionNumber = rnd.Next( 0, questionsAndAnswers.Count );
            // Debug.Log( questionsAndAnswers.Count + " questions left!" );

            //display question
            GameObject.Find( "QuestionText" ).GetComponent< TextMeshProUGUI >( ).text = questionsAndAnswers[ randomQuestionNumber ][ 0 ].ToString( );

            //store correct answer
            correctAnswer = questionsAndAnswers[ randomQuestionNumber ][ 1 ].ToString( );
            // Debug.Log( "Correct Answer: " + correctAnswer );

            //randomize answers
            int[] numbers = new int[] { 1, 2, 3, 4 };
            numbers = RandomizeAnswers( ints: numbers );

            //display answers
            GameObject.Find( "Answer1Text" ).GetComponent< TextMeshProUGUI >( ).text = questionsAndAnswers[ randomQuestionNumber ][ numbers[ 0 ] ].ToString( );
            GameObject.Find( "Answer2Text" ).GetComponent< TextMeshProUGUI >( ).text = questionsAndAnswers[ randomQuestionNumber ][ numbers[ 1 ] ].ToString( );
            GameObject.Find( "Answer3Text" ).GetComponent< TextMeshProUGUI >( ).text = questionsAndAnswers[ randomQuestionNumber ][ numbers[ 2 ] ].ToString( );
            GameObject.Find( "Answer4Text" ).GetComponent< TextMeshProUGUI >( ).text = questionsAndAnswers[ randomQuestionNumber ][ numbers[ 3 ] ].ToString( );  
        } 
    }

    private int[] RandomizeAnswers( int[] ints )
    {
        int n = 4;
        while( n > 1 )
        {
            int k = rnd.Next( n-- );
            int temp = ints[ n ];
            ints[ n ] = ints[ k ];
            ints[ k ] = temp;
        }
        return ints;
    }

    public void CheckAnswer( string incomingAnswerText )
    {
        if( questionsAndAnswers.Count > 0 )
        {
            if( correctAnswer == GameObject.Find( incomingAnswerText ).GetComponent< TextMeshProUGUI >( ).text )
            {
                // Debug.Log( "Found correct answer!" );
                numberOfQuestionsRight++;
                //displaycolor()
                //managepoints()  
            }
            else
            {
                // Debug.Log( "Incorrect answer!" );
                //displaycolor()
                //managepoints()
            }

            //remove question from list
            RemoveQuestion( );

            //ask a new question
            AskQuestion( );            
        }
    }

    private void RemoveQuestion( )
    {
        ResetTrivia( );
        questionsAndAnswers.Remove( questionsAndAnswers[ randomQuestionNumber ] );
    }
    private void ResetTrivia( )
    {
        GameObject.Find( "QuestionText" ).GetComponent< TextMeshProUGUI >( ).text = "This will be the question";
        GameObject.Find( "Answer1Text" ).GetComponent< TextMeshProUGUI >( ).text = "Default answer";
        GameObject.Find( "Answer2Text" ).GetComponent< TextMeshProUGUI >( ).text = "Default answer";
        GameObject.Find( "Answer3Text" ).GetComponent< TextMeshProUGUI >( ).text = "Default answer";
        GameObject.Find( "Answer4Text" ).GetComponent< TextMeshProUGUI >( ).text = "Default answer";      
    }

    private void CheckIfNoMoreQuestions( )
    {
        //check if no more questions
        if( questionsAndAnswers.Count <= 0 )
        {
            Debug.Log( numberOfQuestionsRight + " questions right!" );
            Debug.Log( "Exiting application!" );
            ResetTrivia( );
            Application.Quit( );
        } 
    }

    private void ReadCSVAndStore( string csvToRead )
    {
        //initialize reader
        StreamReader reader = new StreamReader( csvToRead );

        //clear previous questions and answers from List questionsAndAnswers
        questionsAndAnswers.Clear( );

        //discard header
        string lineRead = reader.ReadLine( );
        
        //loop through questions and answers in csv and store in List currentQuestions
        while( ( lineRead = reader.ReadLine( ) ) != "//.end.//" )
        {
            string[] values = lineRead.Split( ',' );
            questionsAndAnswers.Add( values );
        }
    }

    private void PrintCurrentStage( )
    {
        for( int i = 0; i < questionsAndAnswers.Count; i++ )
        {
            Debug.Log( 
                "Number in List: " + ( i + 1 ) + "\n"
                + "Question: " + questionsAndAnswers[ i ][ 0 ].ToString( ) + "\n"
                    + "\tCorrect Answer: " + questionsAndAnswers[ i ][ 1 ].ToString( ) + "\n"
                    + "\tWrong Anwer 1: " + questionsAndAnswers[ i ][ 2 ].ToString( ) + "\n"
                    + "\tWrong Anwer 2: " + questionsAndAnswers[ i ][ 3 ].ToString( ) + "\n"
                    + "\tWrong Anwer 3: " + questionsAndAnswers[ i ][ 4 ].ToString( ) + "\n"
            );
        }
    }
}
