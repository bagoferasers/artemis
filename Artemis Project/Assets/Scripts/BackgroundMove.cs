using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
