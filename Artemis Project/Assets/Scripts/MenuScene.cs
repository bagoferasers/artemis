using UnityEngine;

/*
   File: MenuScene.cs
   Description: Script to handle the Main Menu Scene.
   Last Modified: January 28, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Script to handle the Main Menu.
/// </summary>
public class MenuScene : MonoBehaviour
{
    /// <summary>
    /// SceneTransitions class that will help change the Scene.
    /// </summary>
    private SceneTransitions sceneTransitions = new SceneTransitions( );
    
    /// <summary>
    /// Represents Scene objects for the Play, Settings, and Credit Buttons.
    /// </summary>
    private SceneTransitions.Scene play, settings, credits;

    /// <summary>
    /// Start is called before the first frame update. Initializes the Scene objects for the Play, Settings, and
    /// Credit Buttons with the proper paths.
    /// </summary>    
    void Start( )
    {
        play = new SceneTransitions.Scene( nameOfScene: "Play1" );
        settings = new SceneTransitions.Scene( nameOfScene: "Settings" );
        credits = new SceneTransitions.Scene( nameOfScene: "Credits" );
    }

    /// <summary>
    /// The method that transitions the Scene to the main game.
    /// </summary>
    public void Play( )
    {
        play.ChangeScene( );
    }

    /// <summary>
    /// The method that transitions the Scene to settings.
    /// </summary>
    public void Settings( )
    {
        settings.ChangeScene( );
    }

    /// <summary>
    /// The method that transitions the Scene to credits.
    /// </summary>
    public void Credits( )
    {
        credits.ChangeScene( );
    }    

    /// <summary>
    /// The method that exits the game and quits the application.
    /// </summary>
    public void ExitGame( )
    {
        Debug.Log( "Exiting Game!" );
        Application.Quit( );
    }
}
