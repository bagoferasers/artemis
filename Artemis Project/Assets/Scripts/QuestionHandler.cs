using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class ReadQuestions : MonoBehaviour
{

    private List< string[] > questionsAndAnswers = new List< string[] >( );
    private bool questionRight = false;
    private System.Random rnd = new System.Random( );
    private int randomQuestionNumber, randomAnswerNumber, numberOfQuestionsRight = 0;
    private string correctAnswer;
    
    // Start is called before the first frame update
    void Start( )
    {
        //start off game with questions on stage 0
        HandleStage( 0 );
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
            default:
                Debug.Log( "Please enter valid stage number!" );
                Application.Quit( );
                break;
        }
    }

    private void AskQuestion( )
    {
        //generate random number between 0 and number left
        randomQuestionNumber = rnd.Next( 1, questionsAndAnswers.Count );
        Debug.Log( "Random Question Number: " + randomQuestionNumber );

        //display question
        GameObject.Find( "QuestionText" ).GetComponent< TextMeshProUGUI >( ).text = questionsAndAnswers[ randomQuestionNumber ][ 0 ].ToString( );

        //store correct answer
        correctAnswer = questionsAndAnswers[ randomQuestionNumber ][ 1 ].ToString( );
        Debug.Log( "Correct Answer: " + correctAnswer );

        //randomize answers
        int[] numbers = new int[] { 1, 2, 3, 4 };
        numbers = RandomizeAnswers( ints: numbers );

        //display answers
        GameObject.Find( "Answer1Text" ).GetComponent< TextMeshProUGUI >( ).text = questionsAndAnswers[ randomQuestionNumber ][ numbers[ 0 ] ].ToString( );
        GameObject.Find( "Answer2Text" ).GetComponent< TextMeshProUGUI >( ).text = questionsAndAnswers[ randomQuestionNumber ][ numbers[ 1 ] ].ToString( );
        GameObject.Find( "Answer3Text" ).GetComponent< TextMeshProUGUI >( ).text = questionsAndAnswers[ randomQuestionNumber ][ numbers[ 2 ] ].ToString( );
        GameObject.Find( "Answer4Text" ).GetComponent< TextMeshProUGUI >( ).text = questionsAndAnswers[ randomQuestionNumber ][ numbers[ 3 ] ].ToString( );   
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

    private void AskQuestions( )
    {
        // //loop through questions and ask
        // while( numberOfQuestionsRight != 5 )
        // {
        //     questionRight = AskQuestion( );
        //     numberOfQuestionsRight = 5;
        //     // //if right answer
        //     // if( questionRight == true )
        //     // {
        //     //     //increase number of questions right by one
        //     //     numberOfQuestionsRight++;

        //     //     //displaycolor()
        //     //     //managepoints()
        //     // }
        //     // //else if wrong answer
        //     // else
        //     // {
        //     //     //displaycolor()
        //     //     //managepoints()                
        //     // }
            
        //     // //check if no more questions
        //     // CheckIfNoMoreQuestions( );

        //     // //reset questionRight
        //     // questionRight = false;
        // }
        //if button with correct answer is selected set questionRight to true else false

        //remove question from list
        // questionsAndAnswers.Remove( questionsAndAnswers[ randomQuestionNumber ] );
    }

    private void CheckIfNoMoreQuestions( )
    {
        //check if no more questions
        if( questionsAndAnswers.Count == 0 )
        {
            Debug.Log( "No more questions in stage!" );
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
