using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

/*
   File: Question.cs
   Description: Represents a trivia question with correct and incorrect answers.
   Last Modified: January 21, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// This class represents a trivia question.
/// </summary>
[Serializable]
public class Question
{
    /// <summary>
    /// The text of the question.
    /// </summary>
    public string questionText;

    /// <summary>
    /// The correct answer for the question.
    /// </summary>
    public Answer correctAnswer;

    /// <summary>
    /// The list of answers for the question.
    /// </summary>
    /// <typeparam name="Answer">An instance of the Answer class.</typeparam>
    public List<Answer> allAnswers = new List<Answer>();

    /// <summary>
    /// Represents a pseudo-random number generator.
    /// </summary>
    private System.Random rnd = new System.Random();

    /// <summary>
    /// Represents an answer to a trivia question.
    /// </summary>
    [Serializable]
    public class Answer
    {
        /// <summary>
        /// The text of the answer.
        /// </summary>
        public string text;

        /// <summary>
        /// Initializes a new instance of the Answer class.
        /// </summary>
        /// <param name="text">The text of the answer.</param>
        public Answer(string text)
        {
            this.text = text;
        }
    }

    /// <summary>
    /// Sets the text of the question to be answered.
    /// </summary>
    /// <param name="text">The text of the question to be answered.</param>
    public void SetQuestionText(string text)
    {
        questionText = text;
    }

    /// <summary>
    /// Gets the text of the question to be answered.
    /// </summary>
    /// <returns>The text of the question to be answered.</returns>
    public string GetQuestionText()
    {
        return questionText;
    }

    /// <summary>
    /// Gets the correct answer for the question.
    /// </summary>
    /// <returns>The correct answer.</returns>
    public Answer GetCorrectAnswer()
    {
        return correctAnswer;
    }

    /// <summary>
    /// Sets the correct answer for the question.
    /// </summary>
    /// <param name="text">The text of the right answer.</param>
    public void SetCorrectAnswer(string text)
    {
        correctAnswer = new Answer(text);
    }

    /// <summary>
    /// Gets an answer for the question.
    /// </summary>
    /// <returns>An answer.</returns>
    public Answer GetAnswer(int i)
    {
        return allAnswers[i];
    }

    /// <summary>
    /// Adds an answer to the list of all answers.
    /// </summary>
    /// <param name="text">The text of the answer.</param>
    public void AddAnswer(string text)
    {
        Answer thisAnswer = new Answer(text);
        allAnswers.Add(thisAnswer);
    }

    /// <summary>
    /// Randomizes all answers.
    /// </summary>
    /// <param name="q">The question that needs its answers randomized.</param>
    public void RandomizeAnswers(Question q)
    {
        int n = 4;
        while (n > 1)
        {
            int k = rnd.Next(n--);
            Answer temp = q.allAnswers[n];
            q.allAnswers[n] = q.allAnswers[k];
            q.allAnswers[k] = temp;
        }
    }
}