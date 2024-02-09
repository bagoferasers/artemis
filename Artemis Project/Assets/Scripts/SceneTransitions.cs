using UnityEngine.SceneManagement;
using UnityEngine;

/*
   File: SceneTransitions.cs
   Description: Script to handle scene transitions.
   Last Modified: February 9, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Holds methods that transition to different Scenes.
/// </summary>
public class SceneTransitions : MonoBehaviour
{   
    /// <summary>
    /// The method that transitions the Scene to the main menu.
    /// </summary>
    public static void MainMenuScene( )
    {
        SceneManager.LoadScene( sceneName: "Main" );
    }

    /// <summary>
    /// The method that transitions the Scene to EndGame.
    /// </summary>
    public static void EndGameScene( bool won )
    {
        if( won == true )
        {
            SaveSystem.SetBool( name: "Won", val: true );
        }
        else
        {
            SaveSystem.SetBool( name: "Won", val: false );
        }
        SceneManager.LoadScene( sceneName: "EndGame" );
    }

    /// <summary>
    /// The method that transitions the Scene to Credits manually via Buttons.
    /// </summary>
    public static void CreditsScene( )
    {
        SceneManager.LoadScene( sceneName: "Credits" );
    }

    /// <summary>
    /// The method that transitions the Scene to Credits at the end of a Game.
    /// </summary>
    public static void EndGameCreditsScene( )
    {
        SaveSystem.SetInt( name: "LastPlayerScore", val: 0 );
        SaveSystem.SaveToDisk( );
        SceneManager.LoadScene( sceneName: "Credits" );
    }

    /// <summary>
    /// The method that transitions the Scene to the main game.
    /// </summary>
    public static void Play1Scene( )
    {
        SceneManager.LoadScene( sceneName: "Play1" );
    }

    /// <summary>
    /// The method that transitions the Scene to settings.
    /// </summary>
    public static void SettingsScene( )
    {
        SceneManager.LoadScene( sceneName: "Settings" );
    }

    /// <summary>
    /// The method that exits the game and quits the application.
    /// </summary>
    public static void ExitGame( )
    {
        Debug.Log( message: "Exiting Game!" );
        Application.Quit( );
    }
}
