using UnityEngine;

/*
   File: BeginningBlock.cs
   Description: Represents the class that blocks a Player from moving 
                credits up and down when colliding with the top of the credits.
   Last Modified: February 22, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Blocks Player from moving credits up endlessly.
/// </summary>
public class BeginningBlock : MonoBehaviour
{
    /// <summary>
    /// Will be used to see if Player needs to be blocked or not.
    /// </summary>
    public static bool blockedBeginning = false;

    /// <summary>
    /// When colliding with the top of the screen, block player from continuing.
    /// </summary>
    /// <param name="other">The collider that the credits ran into.</param>
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if( other.collider.name == "CreditsHandler" )
        {
            blockedBeginning = true;
        }
    }

    /// <summary>
    /// When exiting collision, Player can continue scrolling.
    /// </summary>
    /// <param name="other">The collider that the credits ran into.</param>
    private void OnCollisionExit2D( Collision2D other )
    {
        blockedBeginning = false;
    }
}
