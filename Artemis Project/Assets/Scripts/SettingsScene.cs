using UnityEngine;

/*
   File: SettingsScene.cs
   Description: Script to handle the Settings Scene.
   Last Modified: February 19, 2024
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
        successfulResetOverlay = FindAndInit.FindAndDeactivate( gameObjectName: "SuccessfulReset", scriptName: "SettingsScene.cs" );
        areYouSureOverlay = FindAndInit.FindAndDeactivate( gameObjectName: "AreYouSure", scriptName: "SettingsScene.cs" );
        settingsButtons = FindAndInit.InitializeGameObject( gameObjectName: "SettingsButtons", scriptName: "SettingsScene.cs" );
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
    /// Shows the AreYouSure? overlay.
    /// </summary>
    public void ShowAreYouSureOverlay( )
    {
        areYouSureOverlay.SetActive( value: true );
        successfulResetOverlay.SetActive( value: false );
        settingsButtons.SetActive( value: false );
    }

    /// <summary>
    /// Hides the AreYouSure? overlay.
    /// </summary>
    public void HideAreYouSureOverlay( )
    {
        areYouSureOverlay.SetActive( value: false );
        successfulResetOverlay.SetActive( value: false );
        settingsButtons.SetActive( value: true );
    }

    /// <summary>
    /// Shows the successful reset overlay.
    /// </summary>
    private void ShowsuccessfulResetOverlay( )
    {
        successfulResetOverlay.SetActive( value: true );
        areYouSureOverlay.SetActive( value: false );
        settingsButtons.SetActive( value: false );
    }

    /// <summary>
    /// Hides the successful reset overlay.
    /// </summary>
    public void HidesuccessfulResetOverlay( )
    {
        successfulResetOverlay.SetActive( value: false );
        areYouSureOverlay.SetActive( value: false );
        settingsButtons.SetActive( value: true );
    }
}