using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/*
   File: BlinkingLight.cs
   Last Modified: February 19, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Will handle blinking a computer light on and off.
/// </summary>
public class BlinkingLight : MonoBehaviour
{
    /// <summary>
    /// The Light2D to be blinked.
    /// </summary>
    private Light2D light2D;

    /// <summary>
    /// The minimum amount of time in-betweeen blinks.
    /// </summary>
    [ SerializeField ] private float minTime = 0f;

    /// <summary>
    /// The maximum amount of time in-between blinks.
    /// </summary>
    [ SerializeField ] private float maxTime = 1f;

    /// <summary>
    /// The time to wait before blinking light.
    /// </summary>
    [SerializeField] private float waitTime = 0f;

    /// <summary>
    /// Start is called before the first frame update. Grabs the Light2D Component and then starts
    /// blinking the Light2D.
    /// </summary>
    void Start( )
    {
        light2D = GetComponent< Light2D >( );
        StartCoroutine( routine: BlinkLight( ) );
    }

    /// <summary>
    /// Blinks the light and changes its color based on a minimum and maximum amount of time.
    /// </summary>
    IEnumerator BlinkLight( )
    {
        light2D.enabled = false;
        yield return new WaitForSeconds( seconds: waitTime );
        while ( true )
        {
            // Wait for a random time
            yield return new WaitForSeconds( seconds: Random.Range( minInclusive: minTime, maxInclusive: maxTime ) );

            // Toggle the light's enabled state
            light2D.enabled = !light2D.enabled;

            // Randomly change the light's color between green and red
            if ( light2D.enabled )
            {
                light2D.color = Random.Range( minInclusive: 0, maxExclusive: 2 ) == 0 ? Color.green : Color.red;
            }
        }
    }
}