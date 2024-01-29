using TMPro;
using UnityEngine;

/*
   File: LostGame.cs
   Description: Script to handle the LostGame Scene.
   Last Modified: January 29, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Handles the LostGame Scene.
/// </summary>
public class LostGame : MonoBehaviour
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
    /// Represents the GameObject that will hold the final player score.
    /// </summary>
    private GameObject finalScoreGO;

    /// <summary>
    /// The component that will hold the final player score.
    /// </summary>
    private TextMeshProUGUI finalScoreText;

    /// <summary>
    /// Start is called before the first frame update. Initializes the Scene object for the Back Button 
    /// and sets the name of the Scene object to "Main" to return to the main menu. Sets the final
    /// player score.
    /// </summary>
    void Start( )
    {
        finalScoreGO = GameObject.Find( name: "FinalScore" );
        finalScoreText = finalScoreGO.GetComponent< TextMeshProUGUI >( );
        back = new SceneTransitions.Scene( nameOfScene: "Main" );
        finalScoreText.text = PlayerPrefs.GetInt( key: "LastPlayerScore" ).ToString( );
    }

    /// <summary>
    /// The method that transitions the Scene to the main menu. Saves last player score.
    /// </summary>
    public void Back( )
    {
        PlayerPrefs.SetInt( key: "LastPlayerScore", value: 0 );
        back.ChangeScene( );
    }
}
