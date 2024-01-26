using System.Collections.Generic;
using UnityEngine;

/*
   File: PlayerController.cs
   Description: Represents the player (Spacecraft and launch vehicle ).
   Last Modified: January 26, 2024
   Last Modified By: Colby Bailey
*/

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// List of GameObjects from the SLS and Orion spacecraft.
    /// </summary>
    [ SerializeField ] private List< GameObject > orionObjectsList = new List< GameObject >( );

    /// <summary>
    /// Start is called before the first frame update. Adds SLS and Orion GameObjects into List.
    /// </summary>
    void Start( )
    {
        foreach ( Transform child in transform )
        {
            if( 
                child.gameObject.name != "Border" &&
                child.gameObject.name != "Main Camera" &&
                child.gameObject.name != "TriviaHolder" &&
                child.gameObject.name != "TimelineHolder"
                )
            {
                orionObjectsList.Add( item: child.gameObject );
            }
        }
    }
}
