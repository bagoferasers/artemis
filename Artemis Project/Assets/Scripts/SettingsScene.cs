using UnityEngine;

/*
   File: SettingsScene.cs
   Description: Script to handle the Settings Scene.
   Last Modified: January 30, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Handles the Settings Scene.
/// </summary>
public class SettingsScene : MonoBehaviour
{
    /// <summary>
    /// The method that transitions the Scene to the main menu.
    /// </summary>
    public void Back( )
    {
        new SceneTransitions.Scene( nameOfScene: "Main" ).ChangeScene( );
    }
}