using System.Collections;
using TMPro;
using UnityEngine;

/*
   File: LostGame.cs
   Last Modified: February 17, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Handles the EndGame Scene.
/// </summary>
public class EndGame : MonoBehaviour
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
    /// The TextMeshProUGUI component that will hold the player's final score.
    /// </summary>
    private TextMeshProUGUI WonLostText;

    /// <summary>
    /// Sets the final player score to UI and waits a certain amount of time before switching to the Credits Scene.
    /// </summary>
    void Start( )
    {
        finalScoreText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "FinalScore", scriptName: "EndGame.cs" );
        finalScoreText.text = SaveSystem.GetInt( name: "LastPlayerScore" ).ToString( );
        WonLostText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "WonLostText", scriptName: "EndGame.cs" );
        if( SaveSystem.GetBool( name: "Won" ) == true )
        {
            WonLostText.text = "Mission Success!";
        }
        else
        {
            WonLostText.text = "Mission Failure.";
        }
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