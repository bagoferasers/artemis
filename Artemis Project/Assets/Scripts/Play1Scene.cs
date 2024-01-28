using UnityEngine;
using UnityEngine.UI;

/*
   File: Play1Scene.cs
   Description: Script to handle the Play1 Scene.
   Last Modified: January 28, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// The script to handle the Play1 Scene.
/// </summary>
public class Play1Scene : MonoBehaviour
{
    /// <summary>
    /// SceneTransitions class that will help change the Scene.
    /// </summary>
    private SceneTransitions sceneTransitions = new SceneTransitions( );
    
    /// <summary>
    /// Represents Scene objects for the Buttons.
    /// </summary>
    private SceneTransitions.Scene mainMenu;

    /// <summary>
    /// GameObject that will be toggled for the Settings menu.
    /// </summary>
    private GameObject backButton, haze, b1, b2, masterVolume, musicVolume; 

    /// <summary>
    /// GameObject that will hold interactable Buttons.
    /// </summary>
    private GameObject a1GO, a2GO, a3GO, a4GO, exitMissionGO;

    /// <summary>
    /// A Button that will be necessary to toggle as interactable during Settings menu overlay.
    /// </summary>
    private Button a1, a2, a3, a4, exitMission;

    /// <summary>
    /// A BackGroundMove object that will be used to toggle as paused.
    /// </summary>
    public BackgroundMove backgroundMove;
    
    /// <summary>
    /// Start is called before the first frame update. Initializes the Scene, GameObjects, and Buttons.
    /// </summary>    
    void Start( )
    {
        //Grab SceneTransitions.Scene and proper path to Main Scene.
        mainMenu = new SceneTransitions.Scene( nameOfScene: "Main" );

        //Grab the GameObjects that will hold the interactable buttons and check for null. Then gets the
        //component for the Button.
        a1GO = GameObject.Find( name: "Answer1" );
        if( a1GO == null )
        {
            Debug.LogWarning( message: "a1 variable in Play1Scene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        a1 = a1GO.GetComponent< Button >( );

        a2GO = GameObject.Find( name: "Answer2" );
        if( a2GO == null )
        {
            Debug.LogWarning( message: "a2 variable in Play1Scene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        a2 = a2GO.GetComponent< Button >( );

        a3GO = GameObject.Find( name: "Answer3" );
        if( a3GO == null )
        {
            Debug.LogWarning( message: "a3 variable in Play1Scene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        a3 = a3GO.GetComponent< Button >( );

        a4GO = GameObject.Find( name: "Answer4" );
        if( a4GO == null )
        {
            Debug.LogWarning( message: "a4 variable in Play1Scene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        a4 = a4GO.GetComponent< Button >( );

        exitMissionGO = GameObject.Find( name: "ExitMission" );
        if( exitMissionGO == null )
        {
            Debug.LogWarning( message: "exitMission variable in Play1Scene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        exitMission = exitMissionGO.GetComponent< Button >( );

        //Grabs the GameObjects that will hold the Settings menu Buttons and checks for null. Then sets them
        //as inactive.
        backButton = GameObject.Find( name: "Back" );
        if( backButton == null )
        {
            Debug.LogWarning( message: "backButton variable in Play1Scene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        else
        {
            backButton.SetActive( value: false );
        }
        
        haze = GameObject.Find( name: "Haze" );
        if( haze == null )
        {
            Debug.LogWarning( message: "haze variable in Play1Scene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        else
        {
            haze.SetActive( value: false );
        }

        b1 = GameObject.Find( name: "Button1" );
        if( b1 == null )
        {
            Debug.LogWarning( message: "b1 variable in Play1Scene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        else
        {
            b1.SetActive( value: false );
        }

        b2 = GameObject.Find( name: "Button2" );
        if( b2 == null )
        {
            Debug.LogWarning( message: "b2 variable in Play1Scene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        else
        {
            b2.SetActive( value: false );
        }        

        masterVolume = GameObject.Find( name: "MasterVolume" );
        if( masterVolume == null )
        {
            Debug.LogWarning( message: "masterVolume variable in Play1Scene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        else
        {
            masterVolume.SetActive( value: false );
        }    

        musicVolume = GameObject.Find( name: "MusicVolume" );
        if( musicVolume == null )
        {
            Debug.LogWarning( message: "musicVolume variable in Play1Scene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        else
        {
            musicVolume.SetActive( value: false );
        }    
    }

    /// <summary>
    /// The method that transitions the Scene to the main menu.
    /// </summary>
    public void MainMenu( )
    {
        mainMenu.ChangeScene( );
    }

    /// <summary>
    /// Shows the settings menu. Toggles Settings Buttons on and game buttons interactable to false.
    /// </summary>
    public void ShowSettings( )
    {
        backgroundMove.paused = true;
        backButton.SetActive( value: true );
        haze.SetActive( value: true );
        b1.SetActive( value: true );
        b2.SetActive( value: true );
        masterVolume.SetActive( value: true );
        musicVolume.SetActive( value: true );
        a1.interactable = false;
        a2.interactable = false;
        a3.interactable = false;
        a4.interactable = false;
        exitMission.interactable = false;
    }

    /// <summary>
    /// Hides the Settings menu. Toggles Settings Buttons off and game buttons interactable to true.
    /// </summary>
    public void HideSettings( )
    {
        backgroundMove.paused = false;
        backButton.SetActive( value: false );
        haze.SetActive( value: false );
        b1.SetActive( value: false );
        b2.SetActive( value: false );
        masterVolume.SetActive( value: false );
        musicVolume.SetActive( value: false );
        a1.interactable = true;
        a2.interactable = true;
        a3.interactable = true;
        a4.interactable = true;
        exitMission.interactable = true;
    }
}
