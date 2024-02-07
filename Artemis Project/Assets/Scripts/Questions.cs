using System.Collections.Generic;

/*
   File: Questions.cs
   Description: Script to represent each .csv as a list of Questions for each Stage in game.
   Last Modified: January 27, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Represents an object that will hold the list of Questions from a .csv.
/// </summary>
public class Questions
{
    /// <summary>
    /// A list of Question objects from the Questions class to be stored in a Questions object.
    /// </summary>
    /// <typeparam name="Question">Represents a Question object from the Question class.</typeparam>
    public List< Question > stageQuestions = new List< Question >( );
}
