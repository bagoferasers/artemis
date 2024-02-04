using System.Collections;
using TMPro;
using UnityEngine;

/*
   File: LostGame.cs
   Description: Script to handle the LostGame Scene.
   Last Modified: February 4, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Handles the LostGame Scene.
/// </summary>
public class LostGame : MonoBehaviour
{
    /// <summary>
    /// The time to wait till moving to the Credits Scene.
    /// </summary>
    [ SerializeField ] private float waitTime = 10;

    /// <summary>
    /// The TextMeshProUGUI component that will hold the player's final score.
    /// </summary>
    private TextMeshProUGUI finalScoreText;

    /// <summary>
    /// Sets the final player score to UI and waits a certain amount of time before switching to the Credits Scene.
    /// </summary>
    void Start( )
    {
        finalScoreText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "FinalScore", sceneName: "LostGameScene.cs" );
        // finalScoreText.text = PlayerPrefs.GetInt( key: "LastPlayerScore" ).ToString( );
        finalScoreText.text = SaveSystem.GetInt( name: "LastPlayerScore" ).ToString( );
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