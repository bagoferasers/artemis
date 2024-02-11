using UnityEngine;

/*
   File: Stages.cs
   Description: Handles Stage collisions.
   Last Modified: February 10, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// This class will handle all collisions for each stage.
/// </summary>
public class Stage : MonoBehaviour
{
    /// <summary>
    /// Handles collision with Stage0 Finish Line.
    /// </summary>
    /// <param name="collision">The Collider2D that ... collides with Stage0 Finish Line.</param>
    private void OnTriggerEnter2D( Collider2D collision )
    {
        //future switch statement?
        if ( gameObject.tag == "Stage0" && collision.gameObject.tag == "Player" )
        {
            Debug.Log( message: "Collision detected in Stage0" );
            SaveSystem.SetBool( name: "Stage0Finish", val: true );
            SaveSystem.SaveToDisk( );
        }
    }
}
