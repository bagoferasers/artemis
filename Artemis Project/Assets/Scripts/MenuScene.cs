using UnityEngine;
using TMPro;
/*
   File: MenuScene.cs
   Description: Script to handle the Main Menu Scene.
   Last Modified: February 4, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Script to handle the Main Menu.
/// </summary>
public class MenuScene : MonoBehaviour
{
    /// <summary>
    /// The TextMeshProUGUI component that will hold the top player's score.
    /// </summary>
    private TextMeshProUGUI topPlayerScoreText;

    /// <summary>
    /// Initializes top player score to UI.
    /// </summary>    
    void Start( )
    {
        topPlayerScoreText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "TopPlayerScore", sceneName: "MenuScene.cs" );
        if( PlayerPrefs.GetString( key: "FirstTime" ) != "yes" )
        {
            SaveSystem.SetString( name: "FirstTime", val: "yes" );
            SaveSystem.SaveToDisk( );
        }
        topPlayerScoreText.text = SaveSystem.GetInt( "TopPlayerScore" ).ToString( );
    }

    /// <summary>
    /// The method that transitions the Scene to the main game.
    /// </summary>
    public void Play( )
    {
        new SceneTransitions.Scene( nameOfScene: "Play1" ).ChangeScene( );
    }

    /// <summary>
    /// The method that transitions the Scene to settings.
    /// </summary>
    public void Settings( )
    {
        new SceneTransitions.Scene( nameOfScene: "Settings" ).ChangeScene( );
    }

    /// <summary>
    /// The method that transitions the Scene to credits.
    /// </summary>
    public void Credits( )
    {
        new SceneTransitions.Scene( nameOfScene: "Credits" ).ChangeScene( );
    }    

    /// <summary>
    /// The method that exits the game and quits the application.
    /// </summary>
    public void ExitGame( )
    {
        Debug.Log( message: "Exiting Game!" );
        Application.Quit( );
    }
}
