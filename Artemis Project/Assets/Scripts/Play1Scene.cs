using UnityEngine;
using UnityEngine.UI;

/*
   File: Play1Scene.cs
   Description: Script to handle the Play1 Scene.
   Last Modified: February 7, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// The script to handle the Play1 Scene.
/// </summary>
public class Play1Scene : MonoBehaviour
{
    /// <summary>
    /// GameObject that will be toggled.
    /// </summary>
    private GameObject backButton, haze, b1, b2, masterVolume, musicVolume, effectsVolume;

    /// <summary>
    /// GameObject that will hold interactable Buttons.
    /// </summary>
    private GameObject yes, no, areYouSureText;

    /// <summary>
    /// A Button that will be necessary to toggle as interactable during Settings menu overlay.
    /// </summary>
    private Button a1, a2, a3, a4, exitMission, settings;

    /// <summary>
    /// A BackGroundMove object that will be used to toggle as paused.
    /// </summary>
    public BackgroundMove backgroundMove;

    /// <summary>
    /// Start is called before the first frame update. Initializes the Scene, GameObjects, and Buttons.
    /// </summary>    
    void Start( )
    {
        //Initialize Buttons.
        a1 = FindAndInit.InitializeButton( gameObjectName: "Answer1", sceneName: "Play1Scene.cs" );
        a2 = FindAndInit.InitializeButton( gameObjectName: "Answer2", sceneName: "Play1Scene.cs" );
        a3 = FindAndInit.InitializeButton( gameObjectName: "Answer3", sceneName: "Play1Scene.cs" );
        a4 = FindAndInit.InitializeButton( gameObjectName: "Answer4", sceneName: "Play1Scene.cs" );
        exitMission = FindAndInit.InitializeButton( gameObjectName: "ExitMission", sceneName: "Play1Scene.cs" );
        settings = FindAndInit.InitializeButton( gameObjectName: "Settings", sceneName: "Play1Scene.cs" );

        //Initialize and deactivate GameObjects
        backButton = FindAndInit.FindAndDeactivate( gameObjectName: "Back", sceneName: "Play1Scene.cs" );
        haze = FindAndInit.FindAndDeactivate( gameObjectName: "Haze", sceneName: "Play1Scene.cs" );
        b1 = FindAndInit.FindAndDeactivate( gameObjectName: "Button1", sceneName: "Play1Scene.cs" );
        b2 = FindAndInit.FindAndDeactivate( gameObjectName: "Button2", sceneName: "Play1Scene.cs" );
        masterVolume = FindAndInit.FindAndDeactivate( gameObjectName: "MasterVolume", sceneName: "Play1Scene.cs" );
        musicVolume = FindAndInit.FindAndDeactivate( gameObjectName: "MusicVolume", sceneName: "Play1Scene.cs" );
        effectsVolume = FindAndInit.FindAndDeactivate( gameObjectName: "EffectsVolume", sceneName: "Play1Scene.cs" );
        yes = FindAndInit.FindAndDeactivate( gameObjectName: "Yes", sceneName: "Play1Scene.cs" );
        no = FindAndInit.FindAndDeactivate( gameObjectName: "No", sceneName: "Play1Scene.cs" );
        areYouSureText = FindAndInit.FindAndDeactivate( gameObjectName: "AreYouSureText", sceneName: "Play1Scene.cs" );
    }

    /// <summary>
    /// Shows the AreYouSure? menu. Toggles Buttons on and game buttons interactable to false.
    /// </summary>
    public void ShowAreYouSure( )
    {
        areYouSureText.SetActive( value: true );
        yes.SetActive( value: true );
        no.SetActive( value: true );
        Show( );
    }

    /// <summary>
    /// Shows the AreYouSure? menu. Toggles Buttons on and game buttons interactable to false.
    /// </summary>
    public void HideAreYouSure( )
    {
        areYouSureText.SetActive( value: false );
        yes.SetActive( value: false );
        no.SetActive( value: false );
        Hide( );
    }

    /// <summary>
    /// Shows the settings menu. Toggles Settings Buttons on and game buttons interactable to false.
    /// </summary>
    public void ShowSettings( )
    {
        backButton.SetActive( value: true );
        b1.SetActive( value: true );
        b2.SetActive( value: true );
        masterVolume.SetActive( value: true );
        musicVolume.SetActive( value: true );
        effectsVolume.SetActive( value: true );
        Show( );
    }

    /// <summary>
    /// Hides the Settings menu. Toggles Settings Buttons off and game buttons interactable to true.
    /// </summary>
    public void HideSettings( )
    {
        backButton.SetActive( value: false );
        b1.SetActive( value: false );
        b2.SetActive( value: false );
        masterVolume.SetActive( value: false );
        effectsVolume.SetActive( value: false );
        musicVolume.SetActive( value: false );
        Hide( );
    }

    /// <summary>
    /// Sets certain objects active, some interactables to false, and pauses the background. To be
    /// used for showing settings and the AreYouSure? overlays.
    /// </summary>
    public void Show( )
    {
        backgroundMove.paused = true;
        haze.SetActive( value: true );
        exitMission.interactable = false;
        a1.interactable = false;
        a2.interactable = false;
        a3.interactable = false;
        a4.interactable = false;
        settings.interactable = false;
    }

    /// <summary>
    /// Sets certain objects inactive, some interactables to true, and un-pauses the background. To be
    /// used for hiding settings and the AreYouSure? overlays.
    /// </summary>
    public void Hide( )
    {
        haze.SetActive( value: false );
        backgroundMove.paused = false;
        exitMission.interactable = true;
        a1.interactable = true;
        a2.interactable = true;
        a3.interactable = true;
        a4.interactable = true;
        settings.interactable = true;
    }
}
