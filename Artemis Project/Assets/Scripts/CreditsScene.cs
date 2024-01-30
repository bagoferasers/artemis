using UnityEngine;

/*
   File: CreditsScene.cs
   Description: Script to handle the Credits Scene.
   Last Modified: January 30, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Handles the Credits Scene.
/// </summary>
public class CreditsScene : MonoBehaviour
{
    /// <summary>
    /// The method that transitions the Scene to the main menu.
    /// </summary>
    public void Back( )
    {
        new SceneTransitions.Scene( nameOfScene: "Main" ).ChangeScene( );
    }
}
