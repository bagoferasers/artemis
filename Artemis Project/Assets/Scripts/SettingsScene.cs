using UnityEngine;

/*
   File: SettingsScene.cs
   Description: Script to handle the Settings Scene.
   Last Modified: January 27, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Handles the Settings Scene.
/// </summary>
public class SettingsScene : MonoBehaviour
{
    /// <summary>
    /// SceneTransitions class that will help change the Scene.
    /// </summary>
    private SceneTransitions sceneTransitions = new SceneTransitions( );
    
    /// <summary>
    /// Represents a Scene object for the Back Button.
    /// </summary>
    private SceneTransitions.Scene back;

    /// <summary>
    /// Start is called before the first frame update. Initializes the Scene object for the Back Button 
    /// and sets the name of the Scene object to "Main" to return to the main menu.
    /// </summary>
    void Start( )
    {
        back = new SceneTransitions.Scene( nameOfScene: "Main" );
    }

    /// <summary>
    /// The method that transitions the Scene to the main menu.
    /// </summary>
    public void Back( )
    {
        back.ChangeScene( );
    }
}
