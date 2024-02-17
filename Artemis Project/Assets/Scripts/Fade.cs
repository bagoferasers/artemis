using System.Collections;
using UnityEngine;

/*
   File: Fade.cs
   Description: Represents the fading in and out of a transition.
   Last Modified: February 17, 2024
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
    public static CanvasGroup fade;

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
        if( !SaveSystem.GetBool( name: "FirstLaunch" ) )
        {
            StartCoroutine( routine: FadeIntoScene( ) );
            SaveSystem.SetBool( name: "FirstLaunch", val: true );
        }
        else
        {
            fade.alpha = 0f;
        }
    }

    /// <summary>
    /// Fades from black to scene.
    /// </summary>
    private IEnumerator FadeIntoScene( )
    {
        fade.alpha = 1f;
        float elapsedTime = 0f;
        while( elapsedTime < fadeTime )
        {
            fade.alpha = 1f - ( elapsedTime / fadeTime );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fade.alpha = 0f;
    }
}
