using System.Collections.Generic;
using System;

/*
   File: Questions.cs
   Last Modified: February 9, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Script to represent each .csv as a list of Questions for each Stage in game. Also defines a Question class that will represent a Question object.
/// </summary>
public class Questions
{
    /// <summary>
    /// A list of Question objects from the Questions class to be stored in a Questions object.
    /// </summary>
    /// <typeparam name="Question">Represents a Question object from the Question class.</typeparam>
    public List< Question > stageQuestions = new List< Question >( );

    /// <summary>
    /// Randomizes all Question objects in List.
    /// </summary>
    /// <param name="questions">The questions List that needs its Question objects randomized.</param>
    public void RandomizeQuestions( List< Questions > questionsList )
    {
        /// <summary>
        /// Represents a pseudo-random number generator.
        /// </summary>
        Random rnd = new Random( );

        foreach ( Questions questions in questionsList )
        {
            int n = questions.stageQuestions.Count;
            while ( n > 1 )
            {
                int k = rnd.Next( maxValue: n-- );
                Question temp = questions.stageQuestions[ index: n ];
                questions.stageQuestions[ index: n ] = questions.stageQuestions[ index: k ];
                questions.stageQuestions[ index: k ] = temp;
            }            
        }
    }

    /// <summary>
    /// This class represents a trivia question.
    /// </summary>
    [ Serializable ]
    public class Question
    {
        /// <summary>
        /// The text of the question.
        /// </summary>
        private string questionText;

        /// <summary>
        /// The correct answer for the question.
        /// </summary>
        private Answer correctAnswer;

        /// <summary>
        /// The list of answers for the question.
        /// </summary>
        /// <typeparam name="Answer">An instance of the Answer class.</typeparam>
        private List< Answer > allAnswers = new List< Answer >( );

        /// <summary>
        /// Represents a pseudo-random number generator.
        /// </summary>
        private Random rnd = new Random( );

        /// <summary>
        /// Represents an answer to a trivia question.
        /// </summary>
        [ Serializable ]
        public class Answer
        {
            /// <summary>
            /// The text of the answer.
            /// </summary>
            private string text;

            /// <summary>
            /// Initializes a new instance of the Answer class.
            /// </summary>
            /// <param name="text">The text of the answer.</param>
            public Answer( string text )
            {
                this.text = text;
            }

            /// <summary>
            /// Gets the text of the Answer.
            /// </summary>
            /// <returns>The text of the Answer.</returns>
            public string GetAnswerText( )
            {
                return text;
            }
        }

        /// <summary>
        /// Sets the text of the question to be answered.
        /// </summary>
        /// <param name="text">The text of the question to be answered.</param>
        public void SetQuestionText( string text )
        {
            questionText = text;
        }

        /// <summary>
        /// Gets the text of the question to be answered.
        /// </summary>
        /// <returns>The text of the question to be answered.</returns>
        public string GetQuestionText( )
        {
            return questionText;
        }

        /// <summary>
        /// Gets the correct answer for the question.
        /// </summary>
        /// <returns>The correct answer.</returns>
        public Answer GetCorrectAnswer( )
        {
            return correctAnswer;
        }

        /// <summary>
        /// Sets the correct answer for the question.
        /// </summary>
        /// <param name="text">The text of the right answer.</param>
        public void SetCorrectAnswer( string text )
        {
            correctAnswer = new Answer( text );
        }

        /// <summary>
        /// Gets an answer for the question.
        /// </summary>
        /// <returns>An answer.</returns>
        public Answer GetAnswer( int i )
        {
            return allAnswers[ index: i ];
        }

        /// <summary>
        /// Adds an answer to the list of all answers.
        /// </summary>
        /// <param name="text">The text of the answer.</param>
        public void AddAnswer( string text )
        {
            Answer thisAnswer = new Answer( text );
            allAnswers.Add( item: thisAnswer );
        }

        /// <summary>
        /// Randomizes all answers.
        /// </summary>
        /// <param name="q">The question that needs its answers randomized.</param>
        public void RandomizeAnswers( Question q )
        {
            int n = 4;
            while ( n > 1 )
            {
                int k = rnd.Next( maxValue: n-- );
                Answer temp = q.allAnswers[ index: n ];
                q.allAnswers[ index: n ] = q.allAnswers[ index: k ];
                q.allAnswers[ index: k ] = temp;
            }
        }
    }
}
