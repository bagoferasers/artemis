using UnityEngine;
using UnityEngine.UI;
using TMPro;
/*
   File: MenuScene.cs
   Description: Script to handle the Main Menu Scene.
   Last Modified: February 17, 2024
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
    private GameObject enterName;

    /// <summary>
    /// The InputField on the UI that the player will use to input their name.
    /// </summary>
    private TMP_InputField inputField;

    /// <summary>
    /// The Button that the Player will use to submit their name.
    /// </summary>
    private Button submitButton;

    /// <summary>
    /// Initializes top player score to UI.
    /// </summary>    
    void Start( )
    {
        //Find and initialize 
        submitButton = FindAndInit.InitializeGameObject( gameObjectName: "Submit", scriptName: "MenuScene.cs" ).GetComponent< Button >( );
        inputField = FindAndInit.InitializeGameObject( gameObjectName: "Field", scriptName: "MenuScene.cs" ).GetComponent< TMP_InputField >( );
        enterName = FindAndInit.InitializeGameObject( gameObjectName: "EnterName", scriptName: "MenuScene.cs" );
        topPlayerScoreText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "TopPlayerScore", scriptName: "MenuScene.cs" );
        topPlayerName = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "TopPlayerName", scriptName: "MenuScene.cs" );
    
        //set score and Player name
        topPlayerScoreText.text = SaveSystem.GetInt( name: "TopPlayerScore" ).ToString( );
        topPlayerName.text = SaveSystem.GetString( name: "TopPlayerName" ).ToString( );

        //turn off unnecessary objects
        enterName.SetActive( value: false );

        //listen for Player input
        inputField.onEndEdit.AddListener( call: delegate { HandleNameInput( inputField ); } );
        submitButton.onClick.AddListener( call: ( ) => HandleNameInput( inputField ) );
    }

    /// <summary>
    /// Shows the Enter your name... overlay.
    /// </summary>
    public void ShowAskName( )
    {
        enterName.SetActive( value: true );
    }

    /// <summary>
    /// Handles the input of a Player name from an InputField.
    /// </summary>
    /// <param name="inputField">The InputField on the UI that the user will put their name into.</param>
    public void HandleNameInput( TMP_InputField inputField )
    {
        if ( Input.GetKeyDown( key: KeyCode.Return ) || Input.GetKeyDown( key: KeyCode.KeypadEnter ) || submitButton.onClick != null )
        {
            string userInput = inputField.text;
            SaveSystem.SetString( name: "PlayerName", val: userInput );
            inputField.text = "Enter your name...";
        }
        SceneTransitions.Play1Scene( );
    }
}
