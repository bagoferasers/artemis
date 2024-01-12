using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List< GameObject > orionObjectsList = new List< GameObject >( );
    // public float speed = 1f; 
    // public bool paused = false;

    void Start( )
    {
        // int count = 0;
        foreach ( Transform child in transform )
        {
            // count++;
            orionObjectsList.Add( child.gameObject );
        }
        // Debug.Log( "Number of children = " + count );
        // Debug.Log( "List of children = " );
        // foreach( var item in orionObjectsList )
        // {
        //     Debug.Log( item.name );
        // }
    }

    // void Update( )
    // {
    //     if( paused == false )
    //     {
    //         Vector3 target = new Vector3( transform.position.x, 5000f, transform.position.z );
    //         transform.position = Vector3.MoveTowards( transform.position, target, speed * Time.fixedDeltaTime );
    //         speed += 0.00001f;            
    //     }
    // }
}
