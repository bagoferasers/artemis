using System.Collections;
using UnityEngine;

/*
   File: Play1Scene.cs
   Last Modified: March 9, 2024
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
    private GameObject settings, areYouSure, menuButtons, areYouSure1, triviaHolder, successfulResetOverlay, beatStageOverlay;

    /// <summary>
    /// The amount of time to wait after completing a stage before continuing.
    /// </summary>
    [SerializeField] private float waitTime = 4f;

    /// <summary>
    /// Tracks whether it's done waiting or not before continuing to the next stage.
    /// </summary>
    public static bool doneWaiting = false;

    /// <summary>
    /// Start is called before the first frame update. Initializes the Scene, GameObjects, and Buttons.
    /// </summary>    
    void Awake( )
    {
        settings = FindAndInit.InitializeGameObject( gameObjectName: "SettingsButtons", scriptName: "Play1Scene.cs" );
        areYouSure = FindAndInit.InitializeGameObject( gameObjectName: "AreYouSure", scriptName: "Play1Scene.cs" );
        menuButtons = FindAndInit.InitializeGameObject( gameObjectName: "MenuButtons", scriptName: "Play1Scene.cs" );
        areYouSure1 = FindAndInit.InitializeGameObject( gameObjectName: "AreYouSure1", scriptName: "Play1Scene.cs" );
        triviaHolder = FindAndInit.InitializeGameObject( gameObjectName: "TriviaHolder", scriptName: "Play1Scene.cs" );
        successfulResetOverlay = FindAndInit.InitializeGameObject( gameObjectName: "SuccessfulReset", scriptName: "SettingsScene.cs" );
        beatStageOverlay = FindAndInit.InitializeGameObject( gameObjectName: "BeatStage", scriptName: "SettingsScene.cs" );
        ProgressBar.paused = false;
        ShowMenuButtonsAndTrivia( );
    }

    /// <summary>
    /// Checks if conditions are met for continuing to next stage.
    /// </summary>
    void Update( )
    {
        // Check if the current stage's objectives are met
        bool objectivesMet = ( GameManager.numberOfQuestionsRight >= GameManager.numberOfQuestionsNeeded ) && (ProgressBar.slider.value < GameManager.sliderPercentageTo) && ( Play1Scene.doneWaiting == false );
        if ( objectivesMet )
        {
            doneWaiting = false;
            ShowBeatStage( );
        }
    }

    /// <summary>
    /// Hides the BeatStage overlay.
    /// </summary>
    public void HideBeatStage( )
    {
        ProgressBar.paused = false;
        HideBeatStageOverlay( );
        ShowMenuButtonsAndTrivia( );
    }

    /// <summary>
    /// Shows the BeatStage overlay and waits a certain amount of time before continuing to the next question set/stage.
    /// </summary>
    public void ShowBeatStage( )
    {
        ProgressBar.paused = true;
        StartCoroutine( routine: WaitForSuccessfulStageCompletion( ) );
        ShowBeatStageOverlay( );
        HideSettingsOverlay( );
        HideAreYouSureOverlay( );
        HideAreYouSure1Overlay( );
        HideMenuButtonsAndTrivia( );
        HidesuccessfulResetOverlay( );
    }

    /// <summary>
    /// Shows the AreYouSure? menu.
    /// </summary>
    public void ShowAreYouSure( )
    {
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
        ProgressBar.paused = false;
        ShowMenuButtonsAndTrivia( );
    }

    /// <summary>
    /// Shows the AreYouSure1? menu.
    /// </summary>
    public void ShowAreYouSure1( )
    {
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
        ProgressBar.paused = false;
        ShowMenuButtonsAndTrivia( );
    }

    /// <summary>
    /// Shows the settings menu. Toggles Settings Buttons on and game buttons interactable to false.
    /// </summary>
    public void ShowSettings( )
    {
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
        ProgressBar.paused = false;
        ShowMenuButtonsAndTrivia( );
    }

    /// <summary>
    /// Resets all game data and player information then notifies of successful deletion.
    /// </summary>
    public void ResetAllGameData( )
    {
        SaveSystem.ResetAllData( );
        ShowsuccessfulResetOverlay( );
    }

    /// <summary>
    /// Hides the BeatStage overlay.
    /// </summary>
    private void HideBeatStageOverlay( )
    {
        beatStageOverlay.GetComponent< CanvasGroup >( ).alpha = 0f;
        beatStageOverlay.GetComponent< CanvasGroup >( ).interactable = false;
        beatStageOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
    }

    /// <summary>
    /// Shows the BeatStage overlay.
    /// </summary>
    private void ShowBeatStageOverlay( )
    {
        beatStageOverlay.GetComponent< CanvasGroup >( ).alpha = 1f;
        beatStageOverlay.GetComponent< CanvasGroup >( ).interactable = true;
        beatStageOverlay.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
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

    /// <summary>
    /// Hides the settings overlay.
    /// </summary>
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

    /// <summary>
    /// Hides the AreYouSure overlay.
    /// </summary>
    private void HideAreYouSureOverlay( )
    {
        areYouSure.GetComponent< CanvasGroup >( ).alpha = 0f;
        areYouSure.GetComponent< CanvasGroup >( ).interactable = false;
        areYouSure.GetComponent< CanvasGroup >( ).blocksRaycasts = false;

    }

    /// <summary>
    /// Hides the AreYouSure1 overlay.
    /// </summary>
    private void HideAreYouSure1Overlay( )
    {
        areYouSure1.GetComponent< CanvasGroup >( ).alpha = 0f;
        areYouSure1.GetComponent< CanvasGroup >( ).interactable = false;
        areYouSure1.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
    }

    /// <summary>
    /// Shows the menu buttons and trivia buttons.
    /// </summary>
    private void ShowMenuButtonsAndTrivia( )
    {
        doneWaiting = false;
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        triviaHolder.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = true;
        triviaHolder.GetComponent< CanvasGroup >( ).interactable = true;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 1f;
        triviaHolder.GetComponent< CanvasGroup >( ).alpha = 1f;
        HideBeatStageOverlay( );
        HideAreYouSureOverlay( );
        HideSettingsOverlay( );
        HideAreYouSure1Overlay( );
        HidesuccessfulResetOverlay( );
    }

    /// <summary>
    /// Hides the menu buttons and trivia buttons.
    /// </summary>
    private void HideMenuButtonsAndTrivia( )
    {
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        triviaHolder.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = false;
        triviaHolder.GetComponent< CanvasGroup >( ).interactable = false;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 0f;
        triviaHolder.GetComponent< CanvasGroup >( ).alpha = 0f;
    }

    /// <summary>
    /// Waits an amount of time before continuing to next stage.
    /// </summary>
    private IEnumerator WaitForSuccessfulStageCompletion( )
    {
        yield return new WaitForSeconds( seconds: waitTime );
        ShowMenuButtonsAndTrivia( );
        ProgressBar.paused = false;
        doneWaiting = true;
    }
}
