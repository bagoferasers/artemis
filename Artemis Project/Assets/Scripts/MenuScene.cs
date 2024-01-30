using UnityEngine;
using TMPro;
/*
   File: MenuScene.cs
   Description: Script to handle the Main Menu Scene.
   Last Modified: January 30, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Script to handle the Main Menu.
/// </summary>
public class MenuScene : MonoBehaviour
{
    /// <summary>
    /// Represents the GameObject that will hold the top player score.
    /// </summary>
    private GameObject topPlayerScoreText;

    /// <summary>
    /// Holds the top player score.
    /// </summary>
    private TextMeshProUGUI topPlayerScore;

    /// <summary>
    /// Start is called before the first frame update. Initializes the Scene objects for the Play, Settings, and
    /// Credit Buttons with the proper paths. Initializes top player score to UI.
    /// </summary>    
    void Start( )
    {
        //Grab the GameObject that will hold the top player score on the UI and check for null. Then gets the
        //component for the text.
        topPlayerScoreText = GameObject.Find( name: "TopPlayerScore" );
        if( topPlayerScoreText == null )
        {
            Debug.LogWarning( message: "topPlayerScoreText variable in MenuScene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        topPlayerScore = topPlayerScoreText.GetComponent< TextMeshProUGUI >( );

        //Sets the text on the UI to the top player's score.
        topPlayerScore.text = PlayerPrefs.GetInt( key: "TopPlayerScore" ).ToString( );
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
        Debug.Log( "Exiting Game!" );
        Application.Quit( );
    }
}
