using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Needed for UI event handling

/*
   File: ButtonAudioEffects.cs
   Description: Script to handle the audio effects of Buttons.
   Last Modified: February 20, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

[RequireComponent(requiredComponent: typeof(AudioSource))]
public class ButtonAudioEffects : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler // Implement IPointerDownHandler
{
    public AudioClip hoverClip; // Assign in the Inspector
    public AudioClip clickClip; // Assign in the Inspector

    public static AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Handle pointer enter (hover)
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

    // Handle pointer down
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
