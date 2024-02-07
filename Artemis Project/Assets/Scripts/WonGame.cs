using System.Collections;
using TMPro;
using UnityEngine;

/*
   File: WonGame.cs
   Description: Script to handle the WonGame Scene.
   Last Modified: February 7, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
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
        lastPlayerScoreText.text = SaveSystem.GetInt( name: "LastPlayerScore" ).ToString( );
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