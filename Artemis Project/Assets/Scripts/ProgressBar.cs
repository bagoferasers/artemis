using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
   File: PlayerController.cs
   Description: Represents a progress bar in game.
   Last Modified: February 12, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Handles the progress bar in game.
/// </summary>
public class ProgressBar
{
    /// <summary>
    /// Makes progress bar green.
    /// </summary>
    public static void DisplayGreen( )
    {
        FindAndInit.InitializeGameObject( gameObjectName: "ProgressBarFill", sceneName: "ProgressBar.cs" ).GetComponent< Image >( ).color = Color.green;
    }

    /// <summary>
    /// Makes progress bar yellow.
    /// </summary>
    public static void DisplayYellow( )
    {
        FindAndInit.InitializeGameObject( gameObjectName: "ProgressBarFill", sceneName: "ProgressBar.cs" ).GetComponent< Image >( ).color = Color.yellow;
    }

    /// <summary>
    /// Makes progress bar red.
    /// </summary>
    public static void DisplayRed( )
    {
        FindAndInit.InitializeGameObject( gameObjectName: "ProgressBarFill", sceneName: "ProgressBar.cs" ).GetComponent< Image >( ).color = Color.red;
    }

    /// <summary>
    /// Makes progress bar white.
    /// </summary>
    public static void DisplayWhite( )
    {
        FindAndInit.InitializeGameObject( gameObjectName: "ProgressBarFill", sceneName: "ProgressBar.cs" ).GetComponent< Image >( ).color = Color.white;
    }
}
