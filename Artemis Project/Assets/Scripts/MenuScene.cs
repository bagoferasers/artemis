using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
/*
   File: MenuScene.cs
   Last Modified: February 23, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey 
*/

/// <summary>
/// Script to handle the Main Menu.
/// </summary>
public class MenuScene : MonoBehaviour
{
    /// <summary>
    /// The TextMeshProUGUI component that will hold the top player's score.
    /// </summary>
    private TextMeshProUGUI topPlayerScoreText;

    /// <summary>
    /// The TextMeshProUGUI component that will hold the top player's name.
    /// </summary>
    private TextMeshProUGUI topPlayerName;

    /// <summary>
    /// The GameObject to be toggled for the Enter your name... overlay.
    /// </summary>
    public static GameObject enterName;

    /// <summary>
    /// The GameObject to be toggled for the MenuButtons overlay.
    /// </summary>
    public static GameObject menuButtons;

    /// <summary>
    /// The InputField on the UI that the player will use to input their name.
    /// </summary>
    private TMP_InputField inputField;

    /// <summary>
    /// The Button that the Player will use to submit their name.
    /// </summary>
    private Button submitButton;

    /// <summary>
    /// The AudioClip to play upon game exit.
    /// </summary>
    [SerializeField] private AudioClip shutDowncomputer;

    /// <summary>
    /// The AudioClip to play upon game enter.
    /// </summary>    
    [SerializeField] private AudioClip startUpcomputer;

    /// <summary>
    /// The duration to fade the audio out.
    /// </summary>
    [SerializeField] private float fadeOutDuration = 7.0f;

    /// <summary>
    /// The AudioSource that will play AudioClips.
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Initializes top player score to UI.
    /// </summary>    
    void Start( )
    {
        audioSource = GetComponent< AudioSource >( );
        if (SaveSystem.GetBool(name: "FirstLaunch") == false || !SaveSystem.GetBool(name: "FirstLaunch"))
        {
            StartCoroutine(routine: FadeAudioSourceStartToEnd(startVolume: 1f, endVolume: 0f, duration: fadeOutDuration, clip: startUpcomputer ) );
        }

        //Find and initialize 
        submitButton = FindAndInit.InitializeGameObject( gameObjectName: "Submit", scriptName: "MenuScene.cs" ).GetComponent< Button >( );
        inputField = FindAndInit.InitializeGameObject( gameObjectName: "Field", scriptName: "MenuScene.cs" ).GetComponent< TMP_InputField >( );
        enterName = FindAndInit.InitializeGameObject( gameObjectName: "EnterName", scriptName: "MenuScene.cs" );
        menuButtons = FindAndInit.InitializeGameObject( gameObjectName: "MenuButtons", scriptName: "MenuScene.cs" );
        topPlayerScoreText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "TopPlayerScore", scriptName: "MenuScene.cs" );
        topPlayerName = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "TopPlayerName", scriptName: "MenuScene.cs" );
    
        //set score and Player name
        topPlayerScoreText.text = SaveSystem.GetInt( name: "TopPlayerScore" ).ToString( );
        topPlayerName.text = SaveSystem.GetString( name: "TopPlayerName" ).ToString( );

        //turn off/on unnecessary objects
        enterName.GetComponent< CanvasGroup >( ).alpha = 0f;
        enterName.GetComponent< CanvasGroup >( ).interactable = false;
        enterName.GetComponent< CanvasGroup >( ).blocksRaycasts = false;

        //listen for Player input
        inputField.onEndEdit.AddListener
        ( 
            call: ( string value ) => 
            {
                if( Input.GetKeyDown( key: KeyCode.Return ) || Input.GetKeyDown( key: KeyCode.KeypadEnter ) )
                {
                    if (submitButton.GetComponent< ButtonAudioEffects >( ).clickClip && ButtonAudioEffects.audioSource )
                        ButtonAudioEffects.audioSource.PlayOneShot( clip: submitButton.GetComponent< ButtonAudioEffects >( ).clickClip );
                    HandleNameInput( inputField: inputField.text );
                }
            }
        );
        submitButton.onClick.AddListener( call: ( ) => HandleNameInput( inputField: inputField.text ) );
    }

    /// <summary>
    /// Shows the Enter your name... overlay.
    /// </summary>
    public void ShowAskName( )
    {
        enterName.GetComponent< CanvasGroup >( ).alpha = 1f;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 0f;
        enterName.GetComponent< CanvasGroup >( ).blocksRaycasts = true;
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        enterName.GetComponent< CanvasGroup >( ).interactable = true;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = false;
    }

    /// <summary>
    /// Handles the input of a Player name from an InputField.
    /// </summary>
    /// <param name="string">The InputField on the UI that the user will put their name into.</param>
    public void HandleNameInput( string inputField )
    {
        if( inputField != "Enter your name..." )
        {
            SaveSystem.SetString( name: "PlayerName", val: inputField );
            enterName.GetComponent< CanvasGroup >( ).alpha = 0f;
            menuButtons.GetComponent< CanvasGroup >( ).alpha = 0f;
            enterName.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
            menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
            enterName.GetComponent< CanvasGroup >( ).interactable = false;
            menuButtons.GetComponent< CanvasGroup >( ).interactable = false;
            SceneManager.LoadScene( sceneName: "Play1" );
        }
    }

    /// <summary>
    /// Starts the audio fade out and disables buttons upon game exit.
    /// </summary>
    public void ExitGameFromMainMenu( )
    {
        StartCoroutine(routine: FadeAudioSourceStartToEnd(startVolume: 1f, endVolume: 0f, duration: fadeOutDuration, clip: shutDowncomputer ) );
        enterName.GetComponent< CanvasGroup >( ).alpha = 0f;
        menuButtons.GetComponent< CanvasGroup >( ).alpha = 0f;
        enterName.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        menuButtons.GetComponent< CanvasGroup >( ).blocksRaycasts = false;
        enterName.GetComponent< CanvasGroup >( ).interactable = false;
        menuButtons.GetComponent< CanvasGroup >( ).interactable = false;
    }

    /// <summary>
    /// Coroutine to fade audio volume
    /// </summary>
    /// <param name="startVolume">The volume to start the audio at.</param>
    /// <param name="endVolume">The volume to end the audio at.</param>
    /// <param name="duration">The duration to fade the audio.</param>
    /// <param name="clip">The AudioClip to fade.</param>
    /// <returns></returns>
    private IEnumerator FadeAudioSourceStartToEnd(float startVolume, float endVolume, float duration, AudioClip clip )
    {
        audioSource.PlayOneShot( clip );
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(a: startVolume, b: endVolume, t: currentTime / duration);
            yield return null;
        }
    }
}
