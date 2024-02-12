using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
   File: PlayerController.cs
   Description: Represents a progress bar in game.
   Last Modified: February 12, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Handles the progress bar in game.
/// </summary>
public class ProgressBar : MonoBehaviour
{
    /// <summary>
    /// Slider Component on gameObject.
    /// </summary>
    private Slider slider;

    /// <summary>
    /// Fill Image Component of Slider.
    /// </summary>
    private Image fillImage;

    /// <summary>
    /// Start is called before the first frame update. Grabs Slider and Image Components and 
    /// starts a Coroutine to update the Slider value based on a target percentatge and 
    /// duration in seconds.
    /// </summary>
    void Start( )
    {
        slider = gameObject.GetComponent< Slider >( );
        fillImage = gameObject.GetComponentInChildren< Image >( );
        StartCoroutine( routine: UpdateSliderValue( targetPercentage: 100, duration: 30 ) ); // move to 100% over 30 seconds
    }

    /// <summary>
    /// Updates Slider Component value over time based on a target percentage and
    /// duration in seconds.
    /// </summary>
    /// <param name="targetPercentage">The target percentage for the Slider to move to.</param>
    /// <param name="duration">The duration that it should take for the Slider to move.</param>
    /// <returns></returns>
    IEnumerator UpdateSliderValue( float targetPercentage, float duration )
    {
        float elapsedTime = 0;
        float startValue = slider.value;
        float endValue = targetPercentage * slider.maxValue / 100f;
        
        while ( elapsedTime < duration )
        {
            slider.value = Mathf.Lerp( a: startValue, b: endValue, t: elapsedTime / duration);
            ChangeSliderColorBasedOnValue( );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        slider.value = endValue;
        SaveSystem.SetBool( name: "Stage0Finish", val: true );
    }

    /// <summary>
    /// Changes the Slider color based on what value the Slider is at.
    /// </summary>
    private void ChangeSliderColorBasedOnValue( )
    {
        if ( slider.value <= slider.maxValue * 0.75f )
            fillImage.color = Color.green;
        else if ( slider.value <= slider.maxValue * 0.90f )
            fillImage.color = Color.yellow;
        else
            fillImage.color = Color.red;
    }
}
