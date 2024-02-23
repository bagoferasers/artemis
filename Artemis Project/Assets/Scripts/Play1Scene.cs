using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
   File: Play1Scene.cs
   Description: Script to handle the Play1 Scene.
   Last Modified: February 22, 2024
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
        ShowMenuButtonsAndTrivia( );
    }

    /// <summary>
    /// Shows the AreYouSure? menu.
    /// </summary>
    public void ShowAreYouSure( )
    {
        StartCoroutine( routine: WaitForAudio( ) );
        ProgressBar.paused = true;
        areYouSure.GetComponent< CanvasGroup >( ).alpha = 1f;
        areYouSure.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        areYouSure.GetComponent< CanvasGroup >( ).interactable = true;
        HideSettingsOverlay( );
        HideAreYouSure1Overlay( );
        HideMenuButtonsAndTrivia( );
        HidesuccessfulResetOverlay( );
    }

    /// <summary>
    /// Hides the AreYouSure? menu.
    /// </summary>
    public void HideAreYouSure( )
    {
        StartCoroutine( routine: WaitForAudio( ) );
        ProgressBar.paused = false;
        ShowMenuButtonsAndTrivia( );
    }

    /// <summary>
    /// Shows the AreYouSure1? menu.
    /// </summary>
    public void ShowAreYouSure1( )
    {
        StartCoroutine( routine: WaitForAudio( ) );
        ProgressBar.paused = true;
        HideSettingsOverlay( );
        HideAreYouSureOverlay( );
        HideMenuButtonsAndTrivia( );
        HidesuccessfulResetOverlay( );
        areYouSure1.GetComponent< CanvasGroup >( ).alpha = 1f;
        areYouSure1.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        areYouSure1.GetComponent< CanvasGroup >( ).interactable = true;
    }

    /// <summary>
    /// Hides the AreYouSure1? menu.
    /// </summary>
    public void HideAreYouSure1( )
    {
        StartCoroutine( routine: WaitForAudio( ) );
        ProgressBar.paused = false;
        ShowMenuButtonsAndTrivia( );
    }

    /// <summary>
    /// Shows the settings menu. Toggles Settings Buttons on and game buttons interactable to false.
    /// </summary>
    public void ShowSettings( )
    {
        StartCoroutine( routine: WaitForAudio( ) );
        ProgressBar.paused = true;
        HideAreYouSureOverlay( );
        settings.GetComponent< CanvasGroup >( ).alpha = 1f;
        settings.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        settings.GetComponent< CanvasGroup >( ).interactable = true;
        HidesuccessfulResetOverlay( );
        HideAreYouSure1Overlay( );
        HideMenuButtonsAndTrivia( );
    }

    /// <summary>
    /// Hides the Settings menu. Toggles Settings Buttons off and game buttons interactable to true.
    /// </summary>
    public void HideSettings( )
    {
        StartCoroutine( routine: WaitForAudio( ) );
        ProgressBar.paused = false;
        ShowMenuButtonsAndTrivia( );
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
        successfulResetOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        areYouSure.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
    }

    private void HideSettingsOverlay( )
    {
        settings.GetComponent< CanvasGroup >( ).alpha = 0f;
        settings.GetComponent< CanvasGroup >( ).interactable = false;
        settings.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
    }

    /// <summary>
    /// Hides the successful reset overlay.
    /// </summary>
    private void HidesuccessfulResetOverlay( )
    {
        successfulResetOverlay.GetComponent< CanvasGroup >( ).alpha = 0f;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).interactable = false;
        successfulResetOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
    }

    private void HideAreYouSureOverlay( )
    {
        areYouSure.GetComponent< CanvasGroup >( ).alpha = 0f;
        areYouSure.GetComponent< CanvasGroup >( ).interactable = false;
        areYouSure.GetComponent< CanvasGroup >( ).blocksRaycasts = false;

    }

    private void HideAreYouSure1Overlay( )
    {
        areYouSure1.GetComponent< CanvasGroup >( ).alpha = 0f;
        areYouSure1.GetComponent< CanvasGroup >( ).interactable = false;
        areYouSure1.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
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

    private IEnumerator WaitForAudio( )
    {
        while( ButtonAudioEffects.audioSource.isPlaying )
        {
            yield return null;
        }
    }

    private void ShowMenuButtonsAndTrivia( )
    {
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        triviaHolder.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = true;
        triviaHolder.GetComponent< CanvasGroup >( ).interactable = true;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 1f;
        triviaHolder.GetComponent< CanvasGroup >( ).alpha = 1f;
        HideAreYouSureOverlay( );
        HideSettingsOverlay( );
        HideAreYouSure1Overlay( );
        HidesuccessfulResetOverlay( );
    }

    private void HideMenuButtonsAndTrivia( )
    {
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        triviaHolder.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = false;
        triviaHolder.GetComponent< CanvasGroup >( ).interactable = false;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 0f;
        triviaHolder.GetComponent< CanvasGroup >( ).alpha = 0f;
    }
}
