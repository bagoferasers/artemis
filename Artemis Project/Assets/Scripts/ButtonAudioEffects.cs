using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*
   File: ButtonAudioEffects.cs
   Last Modified: February 20, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

// Requires an AudioSource on gameObject.
[RequireComponent(requiredComponent: typeof(AudioSource))]

/// <summary>
/// Script to handle the audio effects of Buttons.
/// </summary>
public class ButtonAudioEffects : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    /// <summary>
    /// AudioClip to play on button hover.
    /// </summary>
    public AudioClip hoverClip;

    /// <summary>
    /// AudioClip to play on button click.
    /// </summary>
    public AudioClip clickClip;

    /// <summary>
    /// AudioSource to play AudioClips.
    /// </summary>
    public static AudioSource audioSource;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Handle pointer enter (hover).
    /// </summary>
    /// <param name="eventData">Data from Mouse pointer.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverClip && audioSource && SceneManager.GetActiveScene().name != "Main")
        {
            PlayAudioClip(clip: hoverClip);
        }
        else if 
        (
            SceneManager.GetActiveScene().name == "Main" && 
            (MenuScene.menuButtons.GetComponent<CanvasGroup>().alpha == 1f || MenuScene.enterName.GetComponent< CanvasGroup >( ).alpha == 1f)
        )
        {
            PlayAudioClip(clip: hoverClip);
        }
    }

    /// <summary>
    /// Handle Mouse pointer down.
    /// </summary>
    /// <param name="eventData">Data from Mouse pointer.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickClip && audioSource && SceneManager.GetActiveScene().name != "Main")
        {
            PlayAudioClip(clip: clickClip);
        }
        else if 
        (
            SceneManager.GetActiveScene().name == "Main" && 
            (MenuScene.menuButtons.GetComponent<CanvasGroup>().alpha == 1f || MenuScene.enterName.GetComponent< CanvasGroup >( ).alpha == 1f)
        )
        {
            PlayAudioClip(clip: clickClip);
        }
    }

    /// <summary>
    /// Plays an AudioClip once.
    /// </summary>
    /// <param name="clip">The AudioClip to play.</param>
    private void PlayAudioClip(AudioClip clip)
    {
        if (gameObject.GetComponentInParent<CanvasGroup>())
        {
            if (gameObject.GetComponentInParent<CanvasGroup>().interactable == true)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
