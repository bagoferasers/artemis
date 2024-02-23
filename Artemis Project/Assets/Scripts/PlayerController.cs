using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
   File: PlayerController.cs
   Last Modified: February 17, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
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
    /// Will hold the current player score.
    /// </summary>
    private TextMeshProUGUI playerScoreText;

    /// <summary>
    /// Will hold the current player name.
    /// </summary>
    private TextMeshProUGUI playerNameText;

    /// <summary>
    /// Represents a Player object that will represent the current player.
    /// </summary>
    public Player player;

    /// <summary>
    /// Start is called before the first frame update. Adds SLS and Orion GameObjects into List.
    /// Initializes current player name and score to UI.
    /// </summary>
    void Start( )
    {
        player = new Player( score: SaveSystem.GetInt( name: "PlayerScore" ), playerName: SaveSystem.GetString( name: "PlayerName" ) );
        playerNameText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "PlayerName", scriptName: "PlayerController.cs" );
        playerNameText.text  = player.GetPlayerName( );
        playerScoreText = FindAndInit.InitializeTextMeshProUGUI( gameObjectName: "PlayerScore", scriptName: "PlayerController.cs" );
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
