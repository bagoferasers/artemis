/*
   File: Player.cs
   Last Modified: February 10, 2024
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
        SaveSystem.SetInt( name: "LastPlayerScore", val: score );
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
        SaveSystem.SetString( name: "PlayerName", val: playerName );
    }

    /// <summary>
    /// Get's the player's name.
    /// </summary>
    public string GetPlayerName( )
    {
        return playerName;
    }
}