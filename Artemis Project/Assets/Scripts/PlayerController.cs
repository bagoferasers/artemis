using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private List< GameObject > orionObjectsList = new List< GameObject >( );

    void Start( )
    {
        try 
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
        catch( Exception e )
        {
            Debug.LogException( e );
            Debug.Log( "Exiting application." );
            Application.Quit( );
        }
    }
}
