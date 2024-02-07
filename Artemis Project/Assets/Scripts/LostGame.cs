using System.Collections;
using TMPro;
using UnityEngine;

/*
   File: LostGame.cs
   Description: Script to handle the LostGame Scene.
   Last Modified: February 7, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
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
        finalScoreText.text = SaveSystem.GetInt( name: "LastPlayerScore" ).ToString( );
        WaitForCredits( );
    }

    /// <summary>
    /// Method that starts a coroutine for the IENumerator to wait a set time before moving to the Credits Scene.
    /// </summary>
    private void WaitForCredits( )
    {
        StartCoroutine( routine: WaitForCreditsNumerator( ) );
    }

    /// <summary>
    /// Waits for a set time before moving to the Credits Scene.
    /// </summary>
    private IEnumerator WaitForCreditsNumerator( )
    {
        yield return new WaitForSeconds( seconds: waitTime );
        SceneTransitions.CreditsScene( );
    }
}