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
        settings = FindAndInit.FindAndDeactivate( gameObjectName: "SettingsButtons", scriptName: "Play1Scene.cs" );
        areYouSure = FindAndInit.FindAndDeactivate( gameObjectName: "AreYouSure", scriptName: "Play1Scene.cs" );
        menuButtons = FindAndInit.InitializeGameObject( gameObjectName: "MenuButtons", scriptName: "Play1Scene.cs" );
        areYouSure1 = FindAndInit.FindAndDeactivate( gameObjectName: "AreYouSure1", scriptName: "Play1Scene.cs" );
        triviaHolder = FindAndInit.InitializeGameObject( gameObjectName: "TriviaHolder", scriptName: "Play1Scene.cs" );
        successfulResetOverlay = FindAndInit.FindAndDeactivate( gameObjectName: "SuccessfulReset", scriptName: "SettingsScene.cs" );
        ProgressBar.paused = false;
    }

    /// <summary>
    /// Shows the AreYouSure? menu.
    /// </summary>
    public void ShowAreYouSure( )
    {
        ProgressBar.paused = true;
        BackgroundMove.paused = true;
        areYouSure.SetActive( value: true );
        settings.SetActive( value: false );
        menuButtons.SetActive( value: false );
        areYouSure1.SetActive( value: false );
        triviaHolder.SetActive( value: false );
    }

    /// <summary>
    /// Shows the AreYouSure? menu.
    /// </summary>
    public void HideAreYouSure( )
    {
        ProgressBar.paused = false;
        BackgroundMove.paused = false;
        areYouSure.SetActive( value: false );
        settings.SetActive( value: false );
        menuButtons.SetActive( value: true );
        areYouSure1.SetActive( value: false );
        triviaHolder.SetActive( value: true );
    }

    /// <summary>
    /// Shows the AreYouSure1? menu.
    /// </summary>
    public void ShowAreYouSure1( )
    {
        ProgressBar.paused = true;
        BackgroundMove.paused = true;
        areYouSure.SetActive( value: false );
        settings.SetActive( value: false );
        menuButtons.SetActive( value: false );
        areYouSure1.SetActive( value: true );
        triviaHolder.SetActive( value: false );
    }

    /// <summary>
    /// Shows the AreYouSure1? menu.
    /// </summary>
    public void HideAreYouSure1( )
    {
        ProgressBar.paused = false;
        BackgroundMove.paused = false;
        areYouSure.SetActive( value: false );
        settings.SetActive( value: false );
        menuButtons.SetActive( value: true );
        areYouSure1.SetActive( value: false );
        triviaHolder.SetActive( value: true );
    }

    /// <summary>
    /// Shows the settings menu. Toggles Settings Buttons on and game buttons interactable to false.
    /// </summary>
    public void ShowSettings( )
    {
        ProgressBar.paused = true;
        BackgroundMove.paused = true;
        areYouSure.SetActive( value: false );
        settings.SetActive( value: true );
        menuButtons.SetActive( value: false );
        areYouSure1.SetActive( value: false );
        triviaHolder.SetActive( value: false );
    }

    /// <summary>
    /// Hides the Settings menu. Toggles Settings Buttons off and game buttons interactable to true.
    /// </summary>
    public void HideSettings( )
    {
        ProgressBar.paused = false;
        BackgroundMove.paused = false;
        areYouSure.SetActive( value: false );
        settings.SetActive( value: false );
        menuButtons.SetActive( value: true );
        areYouSure1.SetActive( value: false );
        triviaHolder.SetActive( value: true );
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
    /// Shows the successful reset overlay.
    /// </summary>
    private void ShowsuccessfulResetOverlay( )
    {
        successfulResetOverlay.SetActive( value: true );
        areYouSure.SetActive( value: false );
    }
}
