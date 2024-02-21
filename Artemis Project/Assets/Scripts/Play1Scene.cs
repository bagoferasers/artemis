using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
   File: Play1Scene.cs
   Description: Script to handle the Play1 Scene.
   Last Modified: February 20, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// The script to handle the Play1 Scene.
/// </summary>
public class Play1Scene : MonoBehaviour
{
    /// <summary>
    /// GameObject that will be toggled.
    /// </summary>
    private GameObject settings, areYouSure, menuButtons, areYouSure1, triviaHolder, successfulResetOverlay;

    /// <summary>
    /// A BackGroundMove object that will be used to toggle as paused.
    /// </summary>
    public BackgroundMove backgroundMove;

    /// <summary>
    /// Start is called before the first frame update. Initializes the Scene, GameObjects, and Buttons.
    /// </summary>    
    void Start( )
    {
        settings = FindAndInit.InitializeGameObject( gameObjectName: "SettingsButtons", scriptName: "Play1Scene.cs" );
        areYouSure = FindAndInit.InitializeGameObject( gameObjectName: "AreYouSure", scriptName: "Play1Scene.cs" );
        menuButtons = FindAndInit.InitializeGameObject( gameObjectName: "MenuButtons", scriptName: "Play1Scene.cs" );
        areYouSure1 = FindAndInit.InitializeGameObject( gameObjectName: "AreYouSure1", scriptName: "Play1Scene.cs" );
        triviaHolder = FindAndInit.InitializeGameObject( gameObjectName: "TriviaHolder", scriptName: "Play1Scene.cs" );
        successfulResetOverlay = FindAndInit.InitializeGameObject( gameObjectName: "SuccessfulReset", scriptName: "SettingsScene.cs" );
        ProgressBar.paused = false;
    }

    /// <summary>
    /// Shows the AreYouSure? menu.
    /// </summary>
    public void ShowAreYouSure( )
    {
        StartCoroutine( routine: ShowAreYouSureWaitForAudio( ) );
    }

    /// <summary>
    /// Hides the AreYouSure? menu.
    /// </summary>
    public void HideAreYouSure( )
    {
        StartCoroutine( routine: HideAreYouSureWaitForAudio( ) );
    }

    /// <summary>
    /// Shows the AreYouSure1? menu.
    /// </summary>
    public void ShowAreYouSure1( )
    {
        StartCoroutine( routine: ShowAreYouSure1WaitForAudio( ) );
    }

    /// <summary>
    /// Hides the AreYouSure1? menu.
    /// </summary>
    public void HideAreYouSure1( )
    {
        StartCoroutine( routine: HideAreYouSure1WaitForAudio( ) );
    }

    /// <summary>
    /// Shows the settings menu. Toggles Settings Buttons on and game buttons interactable to false.
    /// </summary>
    public void ShowSettings( )
    {
        StartCoroutine( routine: ShowSettingsWaitForAudio( ) );
    }

    /// <summary>
    /// Hides the Settings menu. Toggles Settings Buttons off and game buttons interactable to true.
    /// </summary>
    public void HideSettings( )
    {
        StartCoroutine( routine: HideSettingsWaitForAudio( ) );
    }

    /// <summary>
    /// Resets all game data and player information then notifies of successful deletion.
    /// </summary>
    public void ResetAllGameData( )
    {
        StartCoroutine( routine: ResetWaitForAudio( ) );
    }

    /// <summary>
    /// Shows the successful reset overlay.
    /// </summary>
    private void ShowsuccessfulResetOverlay( )
    {
        successfulResetOverlay.GetComponent< CanvasGroup >( ).alpha = 1f;
        areYouSure.GetComponent< CanvasGroup >( ).alpha = 0f;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).interactable = true;
        areYouSure.GetComponent< CanvasGroup >( ).interactable = false;
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

    private IEnumerator ShowAreYouSureWaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
        ProgressBar.paused = true;
        BackgroundMove.paused = true;
        areYouSure.GetComponent< CanvasGroup >( ).alpha = 1f;
        settings.GetComponent< CanvasGroup >( ).alpha = 0f;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 0f;
        areYouSure1.GetComponent< CanvasGroup >( ).alpha = 0f;
        triviaHolder.GetComponent< CanvasGroup >( ).alpha = 0f;

        areYouSure.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        settings.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        areYouSure1.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        triviaHolder.GetComponent< CanvasGroup >( ).blocksRaycasts = false;

        areYouSure.GetComponent< CanvasGroup >( ).interactable = true;
        settings.GetComponent< CanvasGroup >( ).interactable = false;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = false;
        areYouSure1.GetComponent< CanvasGroup >( ).interactable = false;
        triviaHolder.GetComponent< CanvasGroup >( ).interactable = false;
    }

    private IEnumerator HideAreYouSureWaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
        ProgressBar.paused = false;
        BackgroundMove.paused = false;
        areYouSure.GetComponent< CanvasGroup >( ).alpha = 0f;
        settings.GetComponent< CanvasGroup >( ).alpha = 0f;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 1f;
        areYouSure1.GetComponent< CanvasGroup >( ).alpha = 0f;
        triviaHolder.GetComponent< CanvasGroup >( ).alpha = 1f;

        areYouSure.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        settings.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        areYouSure1.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        triviaHolder.GetComponent< CanvasGroup >( ).blocksRaycasts = true;

        areYouSure.GetComponent< CanvasGroup >( ).interactable = false;
        settings.GetComponent< CanvasGroup >( ).interactable = false;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = true;
        areYouSure1.GetComponent< CanvasGroup >( ).interactable = false;
        triviaHolder.GetComponent< CanvasGroup >( ).interactable = true;
    }

    private IEnumerator ShowAreYouSure1WaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
        ProgressBar.paused = true;
        BackgroundMove.paused = true;
        areYouSure.GetComponent< CanvasGroup >( ).alpha = 0f;
        settings.GetComponent< CanvasGroup >( ).alpha = 0f;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 0f;
        areYouSure1.GetComponent< CanvasGroup >( ).alpha = 1f;
        triviaHolder.GetComponent< CanvasGroup >( ).alpha = 0f;

        areYouSure.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        settings.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        areYouSure1.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        triviaHolder.GetComponent< CanvasGroup >( ).blocksRaycasts = false;

        areYouSure.GetComponent< CanvasGroup >( ).interactable = false;
        settings.GetComponent< CanvasGroup >( ).interactable = false;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = false;
        areYouSure1.GetComponent< CanvasGroup >( ).interactable = true;
        triviaHolder.GetComponent< CanvasGroup >( ).interactable = false;
    }

    private IEnumerator HideAreYouSure1WaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
        ProgressBar.paused = false;
        BackgroundMove.paused = false;
        areYouSure.GetComponent< CanvasGroup >( ).alpha = 0f;
        settings.GetComponent< CanvasGroup >( ).alpha = 0f;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 1f;
        areYouSure1.GetComponent< CanvasGroup >( ).alpha = 0f;
        triviaHolder.GetComponent< CanvasGroup >( ).alpha = 1f;

        areYouSure.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        settings.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        areYouSure1.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        triviaHolder.GetComponent< CanvasGroup >( ).blocksRaycasts = true;

        areYouSure.GetComponent< CanvasGroup >( ).interactable = false;
        settings.GetComponent< CanvasGroup >( ).interactable = false;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = true;
        areYouSure1.GetComponent< CanvasGroup >( ).interactable = false;
        triviaHolder.GetComponent< CanvasGroup >( ).interactable = true;
    }

    private IEnumerator ShowSettingsWaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
        ProgressBar.paused = true;
        BackgroundMove.paused = true;
        areYouSure.GetComponent< CanvasGroup >( ).alpha = 0f;
        settings.GetComponent< CanvasGroup >( ).alpha = 1f;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 0f;
        areYouSure1.GetComponent< CanvasGroup >( ).alpha = 0f;
        triviaHolder.GetComponent< CanvasGroup >( ).alpha = 0f;

        areYouSure.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        settings.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        areYouSure1.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        triviaHolder.GetComponent< CanvasGroup >( ).blocksRaycasts = false;

        areYouSure.GetComponent< CanvasGroup >( ).interactable = false;
        settings.GetComponent< CanvasGroup >( ).interactable = true;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = false;
        areYouSure1.GetComponent< CanvasGroup >( ).interactable = false;
        triviaHolder.GetComponent< CanvasGroup >( ).interactable = false;
    }

    private IEnumerator HideSettingsWaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
        ProgressBar.paused = false;
        BackgroundMove.paused = false;
        areYouSure.GetComponent< CanvasGroup >( ).alpha = 0f;
        settings.GetComponent< CanvasGroup >( ).alpha = 0f;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 1f;
        areYouSure1.GetComponent< CanvasGroup >( ).alpha = 0f;
        triviaHolder.GetComponent< CanvasGroup >( ).alpha = 1f;

        areYouSure.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        settings.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        areYouSure1.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        triviaHolder.GetComponent< CanvasGroup >( ).blocksRaycasts = true;

        areYouSure.GetComponent< CanvasGroup >( ).interactable = false;
        settings.GetComponent< CanvasGroup >( ).interactable = false;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = true;
        areYouSure1.GetComponent< CanvasGroup >( ).interactable = false;
        triviaHolder.GetComponent< CanvasGroup >( ).interactable = true;
    }
}
