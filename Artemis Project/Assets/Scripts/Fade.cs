using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/*
   File: Fade.cs
   Description: Represents the fading in and out of a transition.
   Last Modified: February 18, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// The class that fades in and out of a transition.
/// </summary>
public class Fade : MonoBehaviour
{
    /// <summary>
    /// The Light2D that will be faded in and out.
    /// </summary>
    public static Light2D fade;

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
        fade = GetComponent< Light2D >( );
        if( SaveSystem.GetBool( name: "FirstLaunch" ) == false || !SaveSystem.GetBool( name: "FirstLaunch" ) )
        {
            StartCoroutine( routine: FadeIntoScene( ) );
            SaveSystem.SetBool( name: "FirstLaunch", val: true );
        }
        else
        {
            fade.intensity = 1f;
        }
    }

    /// <summary>
    /// Fades from black to scene.
    /// </summary>
    private IEnumerator FadeIntoScene( )
    {
        fade.intensity = 0f;
        float elapsedTime = 0f;
        while( elapsedTime < fadeTime )
        {
            fade.intensity = Mathf.Lerp( a: 0f, b: 1f, t: elapsedTime / fadeTime );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fade.intensity = 1f;
    }
}
