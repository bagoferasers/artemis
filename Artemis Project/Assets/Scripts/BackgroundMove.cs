using System;
using JetBrains.Annotations;
using UnityEngine;

/*
   File: BackgroundMove.cs
   Description: Represents the class that moves the backdrop of the game.
   Last Modified: February 12, 2024
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
    public bool paused;

    /// <summary>
    /// Will be used to signify if GameObject this Script is attached to is in the Credits
    /// Scene or not.
    /// </summary>
    public bool credits;

    /// <summary>
    /// Used to grab the player's current position relative to the background moving.
    /// </summary>
    public Vector2 playerPosition;

    /// <summary>
    /// Update is called once per frame. Updates player position and checks for color of progress bar.
    /// </summary>
    void Update( )
    {
        playerPosition = transform.position;
        if( !credits )
            CheckColor( );
    }

    /// <summary>
    /// Checks for color of progress bar and changes it based on position.
    /// </summary>
    private void CheckColor( )
    {
        switch( GameManager.currentStageNumber )
        {
            case 0:
                switch( playerPosition.y )
                {
                    case >-50f:
                        ProgressBar.DisplayGreen( );
                        break;
                    case >-80f:
                        ProgressBar.DisplayYellow( );
                        break;
                    case >-150f:
                        ProgressBar.DisplayRed( );
                        break;
                    default:
                        ProgressBar.DisplayWhite( );
                        break;
                }
                break;
            case 1: 
                //is green
                break;
            default:
                //is white
                break;
        }
    }

    /// <summary>
    /// Will be used to control the increase of speed over time.
    /// </summary>
    // [ SerializeField ] private float speedIncrement = 0.00001f;

    /// <summary>
    /// Update is called once per frame. Moves the background and increases speed over time.
    /// </summary>
    void FixedUpdate( )
    {
        if( paused == false )
        {
            Vector3 target = new Vector3( x: transform.position.x, y: -5000f, z: transform.position.z );
            transform.position = Vector3.MoveTowards( current: transform.position, target, maxDistanceDelta: speed * Time.fixedDeltaTime );
            // speed += speedIncrement;            
        }
    }
}
