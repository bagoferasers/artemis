using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   File: BackgroundMove.cs
   Description: Represents the class that moves the backdrop of the game.
   Last Modified: January 25, 2024
   Last Modified By: Colby Bailey
*/

public class BackgroundMove : MonoBehaviour
{
    [ SerializeField ] private float speed = 1f; 

    [ SerializeField ] private bool paused = true;

    void Update( )
    {
        if( paused == false )
        {
            Vector3 target = new Vector3( x: transform.position.x, y: -5000f, z: transform.position.z );
            transform.position = Vector3.MoveTowards( current: transform.position, target, maxDistanceDelta: speed * Time.fixedDeltaTime );
            speed += 0.00001f;            
        }
    }
}
