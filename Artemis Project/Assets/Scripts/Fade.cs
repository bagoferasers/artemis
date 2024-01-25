using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fade : MonoBehaviour
{
    private CanvasGroup fade;
    [ SerializeField ] private bool fadeIn = true;
    [ SerializeField ] private float fadeTime = 0.1f;
    void Start( )
    {
        fade = GetComponent< CanvasGroup >( );
    }

    void Update( )
    {
        // Debug.Log( "fading" );
        if( fadeIn )
        {
            fade.alpha = Mathf.MoveTowards( current: fade.alpha, target: 0f, maxDelta: fadeTime * Time.fixedDeltaTime );
        }
        else
        {
            fade.alpha = Mathf.MoveTowards( current: fade.alpha, target: 1f, maxDelta: fadeTime * Time.fixedDeltaTime );
        }
        // Debug.Log( fade.alpha );
    }
}
