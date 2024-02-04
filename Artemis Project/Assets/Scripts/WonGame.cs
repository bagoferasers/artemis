using System.Collections;
using TMPro;
using UnityEngine;

/*
   File: WonGame.cs
   Description: Script to handle the WonGame Scene.
   Last Modified: February 4, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Handles the WonGame Scene.
/// </summary>
public class WonGame : MonoBehaviour
{
    /// <summary>
    /// The time to wait till moving to the Credits Scene.
    /// </summary>
    [ SerializeField ] private float waitTime = 10;

    /// <summary>
    /// The TextMeshProUGUI component to hold the final score.
    /// </summary>
    private TextMeshProUGUI lastPlayerScoreText;

    /// <summary>
    /// Start is called before the first frame update. Sets the final player score and starts timer to wait
    /// until moving to the credits scene.
    /// </summary>
    void Start( )
    {
        lastPlayerScoreText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "FinalScore", sceneName: "WonGame.cs" );
        // lastPlayerScoreText.text = PlayerPrefs.GetInt( key: "LastPlayerScore" ).ToString( );
        lastPlayerScoreText.text = SaveSystem.GetInt( name: "LastPlayerScore" ).ToString( );
        StartCoroutine( routine: WaitForCredits( ) );
    }

    /// <summary>
    /// The method that transitions the Scene to the main menu. Saves last player score.
    /// </summary>
    public void Next( )
    {
        // PlayerPrefs.SetInt( key: "LastPlayerScore", value: 0 );
        SaveSystem.SetInt( name: "LastPlayerScore", val: 0 );
        SaveSystem.SaveToDisk( );
        new SceneTransitions.Scene( nameOfScene: "Credits" ).ChangeScene( );
    }

    /// <summary>
    /// Waits for a set time before moving to the Credits Scene.
    /// </summary>
    private IEnumerator WaitForCredits( )
    {
        yield return new WaitForSeconds( seconds: waitTime );
        new SceneTransitions.Scene( nameOfScene: "Credits" ).ChangeScene( );
    }
}