using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
   File: PlayerController.cs
   Description: Represents the player controller.
   Last Modified: January 30, 2024
   Last Modified By: Colby Bailey
*/

/// <summary>
/// Controls the Player object.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// List of GameObjects from the SLS and Orion spacecraft.
    /// </summary>
    [ SerializeField ] private List< GameObject > orionObjectsList = new List< GameObject >( );

    /// <summary>
    /// Represents the GameObjects that will hold the player name.
    /// </summary>
    private GameObject playerNameGO;

    /// <summary>
    /// Represents the GameObjects that will hold the current score.
    /// </summary>
    private GameObject playerScoreGO;

    /// <summary>
    /// Will hold the current player namne.
    /// </summary>
    private TextMeshProUGUI playerNameText;
    
    /// <summary>
    /// Will hold the current player score.
    /// </summary>
    private TextMeshProUGUI playerScoreText;

    /// <summary>
    /// Represents a Player object that will represent the current player.
    /// </summary>
    public Player player = new Player( score: 0, playerName: "Colby Bailey" );

    /// <summary>
    /// Start is called before the first frame update. Adds SLS and Orion GameObjects into List.
    /// Initializes current player name and score to UI.
    /// </summary>
    void Start( )
    {
        //Grab the GameObject that will hold the player's name and check for null. Then gets the
        //component for the text and sets it.
        playerNameGO = GameObject.Find( name: "PlayerName" );
        if( playerNameGO == null )
        {
            Debug.LogWarning( message: "playerNameGO variable in MenuScene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        playerNameText = playerNameGO.GetComponent< TextMeshProUGUI >( );
        playerNameText.text = player.GetPlayerName( );

        //Grab the GameObject that will hold the player's current score and check for null. Then gets the
        //component for the text and sets it.
        playerScoreGO = GameObject.Find( name: "PlayerScore" );
        if( playerScoreGO == null )
        {
            Debug.LogWarning( message: "playerScoreGO variable in MenuScene.cs is null!" , context: gameObject );
            Application.Quit( );
        }
        playerScoreText = playerScoreGO.GetComponent< TextMeshProUGUI >( );
        playerScoreText.text = player.GetScore( ).ToString( );
        
        //Grab all GameObjects of spacecraft.
        foreach ( Transform child in transform )
        {
            if( 
                child.gameObject.name != "Border" &&
                child.gameObject.name != "Main Camera" &&
                child.gameObject.name != "TriviaHolder" &&
                child.gameObject.name != "TimelineHolder"
                )
            {
                orionObjectsList.Add( item: child.gameObject );
            }
        }
    }

    /// <summary>
    /// Update is called once per frame. Updates the player score text on UI to current
    /// player score.
    /// </summary>
    void Update( )
    {
        playerScoreText.text = player.GetScore( ).ToString( );
    }
}
