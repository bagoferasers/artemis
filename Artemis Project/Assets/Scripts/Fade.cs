using UnityEngine;

/*
   File: Fade.cs
   Description: Represents the fading in and out of a transition.
   Last Modified: January 26, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// The class that fades in and out of a transition.
/// </summary>
public class Fade : MonoBehaviour
{
    /// <summary>
    /// The CanvasGroup of the GameObject this script is attached to.
    /// </summary>
    private CanvasGroup fade;

    /// <summary>
    /// Will control if script will fade the CanvasGroup in or out.
    /// </summary>
    [ SerializeField ] private bool fadeIn = true;

    /// <summary>
    /// The speed at which the fading will happen.
    /// </summary>
    [ SerializeField ] private float fadeTime = 0.1f;

    /// <summary>
    /// Start is called before the first frame update. Grabs the CanvasGroup of the GameObject this script
    /// is attached to.
    /// </summary>
    void Start( )
    {
        fade = GetComponent< CanvasGroup >( );
    }

    /// <summary>
    /// Update is called once per frame. Fades in or out of the CanvasGroup.
    /// </summary>
    void Update( )
    {
        if( fadeIn )
        {
            fade.alpha = Mathf.MoveTowards( current: fade.alpha, target: 0f, maxDelta: fadeTime * Time.fixedDeltaTime );
        }
        else
        {
            fade.alpha = Mathf.MoveTowards( current: fade.alpha, target: 1f, maxDelta: fadeTime * Time.fixedDeltaTime );
        }
    }
}
