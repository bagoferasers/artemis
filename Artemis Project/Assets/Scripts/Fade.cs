using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/*
   File: Fade.cs
   Description: Represents the fading in and out of a transition.
   Last Modified: February 19, 2024
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
    private Light2D fade;

    /// <summary>
    /// The speed at which the fading will happen.
    /// </summary>
    [SerializeField] private float fadeTime = 2f;

    /// <summary>
    /// The intensity to set the light to.
    /// </summary>
    [SerializeField] private float fadeIntensity = 1f;

    /// <summary>
    /// The time to wait before turning light on.
    /// </summary>
    [SerializeField] private float waitTime = 0f;

    /// <summary>
    /// Start is called before the first frame update. Grabs the CanvasGroup of the GameObject this script
    /// is attached to.
    /// </summary>
    void Start()
    {
        fade = GetComponent<Light2D>();
        if (SaveSystem.GetBool(name: "FirstLaunch") == false || !SaveSystem.GetBool(name: "FirstLaunch"))
        {
            StartCoroutine(routine: FadeIntoScene( ) );
        }
        else
        {
            fade.intensity = fadeIntensity;
            MenuScene.menuButtons.SetActive(value: true);
        }
    }

    /// <summary>
    /// Fades computer screen from black to scene.
    /// </summary>
    private IEnumerator FadeIntoScene()
    {
        fade.intensity = 0f;
        float elapsedTime = 0f;
        if (gameObject.name == "ComputerBacklight")
            MenuScene.menuButtons.SetActive(value: false);
        yield return new WaitForSeconds( seconds: waitTime );
        while (elapsedTime < fadeTime)
        {
            fade.intensity = Mathf.Lerp(a: 0f, b: fadeIntensity, t: elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fade.intensity = fadeIntensity;
        if (gameObject.name == "ComputerBacklight")
            MenuScene.menuButtons.SetActive(value: true);
        SaveSystem.SetBool(name: "FirstLaunch", val: true);
    }
}
