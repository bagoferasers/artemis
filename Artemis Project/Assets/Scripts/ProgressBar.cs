using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

/*
   File: PlayerController.cs
   Last Modified: March 9, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Handles the progress bar in game.
/// </summary>
public class ProgressBar : MonoBehaviour
{
    /// <summary>
    /// Gets the singleton instance of the <see cref="ProgressBar"/> class.
    /// </summary>
    /// <value>
    /// The singleton instance of the <see cref="ProgressBar"/> class. Once an instance is created, it remains available throughout the application's lifecycle.
    /// </value>
    public static ProgressBar Instance { get; private set; }

    /// <summary>
    /// Slider Component on gameObject.
    /// </summary>
    public static Slider slider;

    /// <summary>
    /// Fill Image Component of Slider.
    /// </summary>
    public static Image fillImage;

    /// <summary>
    /// Controls when to move slider.
    /// </summary>
    public static bool paused = false;

    /// <summary>
    /// Holds a Coroutine. This will be used to stop coroutines that are active.
    /// </summary>
    public Coroutine coroutine;

    /// <summary>
    /// Initializes the <see cref="ProgressBar"/> component, ensuring a single instance (singleton pattern),
    /// and sets up the initial UI elements for the progress bar. Also configures the progress bar for the current game stage.
    /// </summary>
    /// <remarks>
    /// <para>If no other instance of <see cref="ProgressBar"/> exists, this instance becomes the singleton instance and is not destroyed on loading a new scene.</para>
    /// <para>This method finds and initializes the slider and fill image components, sets their initial values,
    /// and calls <see cref="SetupStage"/> with the current stage number from <see cref="GameManager"/>.</para>
    /// </remarks>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(target: gameObject);
        }

        slider = FindAndInit.InitializeGameObject(gameObjectName: "ProgressBar", scriptName: "ProgressBar.cs").GetComponent<Slider>();
        fillImage = FindAndInit.InitializeGameObject(gameObjectName: "ProgressBar", scriptName: "ProgressBar.cs").GetComponentInChildren<Image>();
        
        slider.value = 0f;
        fillImage.color = Color.green;
        SetupStage(stageNumber: GameManager.currentStageNumber);
    }

    /// <summary>
    /// Updates Slider Component value over time based on a target percentage and
    /// duration in seconds.
    /// </summary>
    /// <param name="duration">The duration that it should take for the Slider to move.</param>
    public IEnumerator UpdateSliderValue(float duration)
    {
        float startValue = GameManager.sliderPercentageFrom / 100f * slider.maxValue;
        float endValue = GameManager.sliderPercentageTo / 100f * slider.maxValue;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            if (!paused)
            {
                slider.value = Mathf.Lerp(a: startValue, b: endValue, t: elapsedTime / duration);
                ChangeSliderColorBasedOnValue();
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        slider.value = endValue;
        SaveSystem.SetBool(name: "StageFinish", val: true);
    }

    /// <summary>
    /// Changes the Slider color based on what value the Slider is at.
    /// </summary>
    public void ChangeSliderColorBasedOnValue()
    {
        // Ensure these values are accurately representing the slider's range for the current stage.
        float normalizedStartValue = GameManager.sliderPercentageFrom / 100f * slider.maxValue;
        float normalizedEndValue = GameManager.sliderPercentageTo / 100f * slider.maxValue;

        // Calculate the progress within the stage as a fraction of the stage's total range.
        float currentStageValue = slider.value - normalizedStartValue;
        float stageTotalRange = normalizedEndValue - normalizedStartValue;
        float stageProgress = currentStageValue / stageTotalRange;

        if (stageProgress <= 0.5)
            fillImage.color = Color.green;
        else if (stageProgress <= 0.75)
            fillImage.color = Color.yellow;
        else
            fillImage.color = Color.red;
    }

    /// <summary>
    /// Resets the slider for the current stage, setting its value according to the stage's starting percentage,
    /// changing the fill color to green, and restarting the slider update coroutine with the current stage's duration.
    /// </summary>
    public void ResetSliderForStage()
    {
        slider.value = GameManager.sliderPercentageFrom / 100f * slider.maxValue;
        fillImage.color = Color.green;
        StopAllCoroutines( );
        coroutine = StartCoroutine(routine: UpdateSliderValue( duration: GameManager.sliderDuration ));
    }

    /// <summary>
    /// Configures the slider settings for a specified game stage and resets the slider to reflect these new settings.
    /// </summary>
    /// <param name="stageNumber">The number of the stage to set up. Each stage has predefined slider start and end percentages, and a duration.</param>
    public void SetupStage(int stageNumber) {
        switch(stageNumber) {
            case 0: 
                GameManager.sliderPercentageFrom = 0;
                GameManager.sliderPercentageTo = 25;
                GameManager.sliderDuration = 60;
                ResetSliderForStage();
                break;
            case 1:
                GameManager.sliderPercentageFrom = 25;
                GameManager.sliderPercentageTo = 100;
                GameManager.sliderDuration = 60;
                ResetSliderForStage();
                break;
        }
    }
}
