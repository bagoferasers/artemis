using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fade : MonoBehaviour
{
    private CanvasGroup fade;
    public bool fadeIn = true;
    private float fadeTime = 0.1f;
    void Start( )
    {
        try 
        {
            fade = GetComponent< CanvasGroup >( );
        }
        catch( Exception e )
        {
            Debug.LogException( e );
            Debug.Log( "Exiting application." );
            Application.Quit( );
        }
    }

    void Update( )
    {
        // Debug.Log( "fading" );
        if( fadeIn )
        {
            fade.alpha = Mathf.MoveTowards( fade.alpha, 0f, fadeTime * Time.fixedDeltaTime );
        }
        else
        {
            fade.alpha = Mathf.MoveTowards( fade.alpha, 1f, fadeTime * Time.fixedDeltaTime );
        }
        // Debug.Log( fade.alpha );
    }
}
