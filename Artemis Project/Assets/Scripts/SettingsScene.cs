using System.Collections;
using UnityEngine;

/*
   File: SettingsScene.cs
   Description: Script to handle the Settings Scene.
   Last Modified: February 20, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Handles the Settings Scene.
/// </summary>
public class SettingsScene : MonoBehaviour
{
    /// <summary>
    /// The successfulResetOverlay GameObject overlay that will be toggled.
    /// </summary>
    private GameObject successfulResetOverlay;

    /// <summary>
    /// The SettingsButtons GameObject that will be toggled.
    /// </summary>
    private GameObject settingsButtons;

    /// <summary>
    /// The areYouSureOverlay GameObject overlay that will be toggled.
    /// </summary>
    private GameObject areYouSureOverlay;

    /// <summary>
    /// Start is called before the first frame update. Initializes the Scene, GameObjects, and Buttons.
    /// </summary>
    void Start( )
    {
        successfulResetOverlay = FindAndInit.InitializeGameObject( gameObjectName: "SuccessfulReset", scriptName: "SettingsScene.cs" );
        areYouSureOverlay = FindAndInit.InitializeGameObject( gameObjectName: "AreYouSure", scriptName: "SettingsScene.cs" );
        settingsButtons = FindAndInit.InitializeGameObject( gameObjectName: "SettingsButtons", scriptName: "SettingsScene.cs" );

        areYouSureOverlay.GetComponent< CanvasGroup >( ).alpha = 0f;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).alpha = 0f;
        areYouSureOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        areYouSureOverlay.GetComponent< CanvasGroup >( ).interactable = false;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).interactable = false;
    }

    /// <summary>
    /// Resets all game data and player information then notifies of successful deletion.
    /// </summary>
    public void ResetAllGameData( )
    {
        StartCoroutine( routine: ResetWaitForAudio( ) );
    }

    private IEnumerator ResetWaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
        SaveSystem.ResetAllData( );
        ShowsuccessfulResetOverlay( );
    }

    /// <summary>
    /// Shows the AreYouSure? overlay.
    /// </summary>
    public void ShowAreYouSureOverlay( )
    {
        StartCoroutine( routine: ShowAreYouSureWaitForAudio( ) );
    }

    /// <summary>
    /// Hides the AreYouSure? overlay.
    /// </summary>
    public void HideAreYouSureOverlay( )
    {
        StartCoroutine( routine: HideAreYouSureWaitForAudio( ) );
    }

    /// <summary>
    /// Shows the successful reset overlay.
    /// </summary>
    private void ShowsuccessfulResetOverlay( )
    {
        StartCoroutine( routine: ShowSuccessfulResetWaitForAudio( ) );
    }

    /// <summary>
    /// Hides the successful reset overlay.
    /// </summary>
    public void HidesuccessfulResetOverlay( )
    {
        StartCoroutine( routine: HideSuccessfulResetWaitForAudio( ) );
    }

    private IEnumerator ShowAreYouSureWaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
        areYouSureOverlay.GetComponent< CanvasGroup >( ).alpha = 1f;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).alpha = 0f;
        settingsButtons.GetComponent< CanvasGroup >( ).alpha = 0f;

        areYouSureOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        settingsButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        
        areYouSureOverlay.GetComponent< CanvasGroup >( ).interactable = true;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).interactable = false;
        settingsButtons.GetComponent< CanvasGroup >( ).interactable = false;
    }

    private IEnumerator HideAreYouSureWaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
        areYouSureOverlay.GetComponent< CanvasGroup >( ).alpha = 0f;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).alpha = 0f;
        settingsButtons.GetComponent< CanvasGroup >( ).alpha = 1f;

        areYouSureOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        settingsButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = true;

        areYouSureOverlay.GetComponent< CanvasGroup >( ).interactable = false;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).interactable = false;
        settingsButtons.GetComponent< CanvasGroup >( ).interactable = true;
    }

    private IEnumerator ShowSuccessfulResetWaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
        successfulResetOverlay.GetComponent< CanvasGroup >( ).alpha = 1f;
        areYouSureOverlay.GetComponent< CanvasGroup >( ).alpha = 0f;
        settingsButtons.GetComponent< CanvasGroup >( ).alpha = 0f;

        successfulResetOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        areYouSureOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        settingsButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = false;

        successfulResetOverlay.GetComponent< CanvasGroup >( ).interactable = true;
        areYouSureOverlay.GetComponent< CanvasGroup >( ).interactable = false;
        settingsButtons.GetComponent< CanvasGroup >( ).interactable = false;
    }

    private IEnumerator HideSuccessfulResetWaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
        successfulResetOverlay.GetComponent< CanvasGroup >( ).alpha = 0f;
        areYouSureOverlay.GetComponent< CanvasGroup >( ).alpha = 0f;
        settingsButtons.GetComponent< CanvasGroup >( ).alpha = 1f;

        successfulResetOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        areYouSureOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        settingsButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = true;

        successfulResetOverlay.GetComponent< CanvasGroup >( ).interactable = false;
        areYouSureOverlay.GetComponent< CanvasGroup >( ).interactable = false;
        settingsButtons.GetComponent< CanvasGroup >( ).interactable = true;
    }
}