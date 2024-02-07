using UnityEngine;

/*
   File: Player.cs
   Description: Represents a player.
   Last Modified: January 29, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Represents a player.
/// </summary>
public class Player
{
    /// <summary>
    /// Represents the player's current score.
    /// </summary>
    private int score;

    /// <summary>
    /// Represents the current player's name.
    /// </summary>
    private string playerName;

    /// <summary>
    /// A constructor that represents a Player object.
    /// </summary>
    /// <param name="score">The player's score.</param>
    /// <param name="playerName">The player's name.</param>
    public Player( int score, string playerName )
    {
        this.score = score;
        this.playerName = playerName;
    }

    /// <summary>
    /// Sets the player's score.
    /// </summary>
    /// <param name="score">The score to set as the player's score.</param>
    public void SetScore( int score )
    {
        this.score = score;
        // PlayerPrefs.SetInt( key: "LastPlayerScore", value: score );
        // PlayerPrefs.Save( );
        SaveSystem.SetInt( name: "LastPlayerScore", val: score );
        SaveSystem.SaveToDisk( );
    }
    
    /// <summary>
    /// Get's the player's score.
    /// </summary>
    public int GetScore( )
    {
        return score;
    }

    /// <summary>
    /// Set's the player's name.
    /// </summary>
    /// <param name="playerName">The player's name to be set.</param>
    public void SetPlayerName( string playerName )
    {
        this.playerName = playerName;
        // PlayerPrefs.SetString( key: "PlayerName", value: playerName );
        // PlayerPrefs.Save( );
        SaveSystem.SetString( name: "PlayerName", val: playerName );
        SaveSystem.SaveToDisk( );
    }

    /// <summary>
    /// Get's the player's name.
    /// </summary>
    public string GetPlayerName( )
    {
        return playerName;
    }
}