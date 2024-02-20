using UnityEngine;

/*
   File: BackgroundMove.cs
   Description: Represents the class that moves the backdrop of the game.
   Last Modified: February 20, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// The class that moves the backdrop of the game.
/// </summary>
public class BackgroundMove : MonoBehaviour
{
    /// <summary>
    /// The speed that the background will move.
    /// </summary>
    public float speed = 1f; 

    /// <summary>
    /// Will be used to control when the background should move.
    /// </summary>
    public static bool paused;

    /// <summary>
    /// Update is called once per frame. Moves the background and increases speed over time.
    /// </summary>
    void FixedUpdate( )
    {
        if( paused == false )
        {
            Vector3 target = new Vector3( x: transform.position.x, y: -5000f, z: transform.position.z );
            transform.position = Vector3.MoveTowards( current: transform.position, target, maxDistanceDelta: speed * Time.fixedDeltaTime );
        }
    }
}
