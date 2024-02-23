using System.Collections;
using UnityEngine;

/*
   File: BackgroundNoise.cs
   Last Modified: February 23, 2024
   Last Modified By: Colby Bailey
   Authors: Colby Bailey
*/

/// <summary>
/// Represents the class that carries the background noise throughout the game.
/// </summary>
public class BackgroundNoise : MonoBehaviour
{
    /// <summary>
    /// The audio clip to play in the background.
    /// </summary>
    [SerializeField] private AudioClip backgroundClip;

    /// <summary>
    /// The duration to fade the audio in.
    /// </summary>
    [SerializeField] private float fadeInDuration = 2.0f;

    /// <summary>
    /// The AudioSource to play the AudioClip from.
    /// </summary>
    public static AudioSource audioSource;

    /// <summary>
    /// Ensure this object persists and doesn't get destroyed when loading a new scene.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(target: gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundClip;
        audioSource.playOnAwake = false;
    }

    /// <summary>
    /// Start is called before the first frame update. Starts routine to fade in audio.
    /// </summary>
    void Start()
    {
        StartCoroutine(routine: FadeAudioSourceStartToEnd(startVolume: 0f, endVolume: 1f, duration: fadeInDuration, source: audioSource, play: true));
    }

    /// <summary>
    /// Coroutine to fade audio volume
    /// </summary>
    /// <param name="startVolume">The volume to start the fade at.</param>
    /// <param name="endVolume">The volume to end the fade at.</param>
    /// <param name="duration">The duration to fade the audio in.</param>
    /// <param name="source">The AudioSource that will play the AudioClip.</param>
    /// <param name="play">Controls whether source should be playing or not.</param>
    private IEnumerator FadeAudioSourceStartToEnd(float startVolume, float endVolume, float duration, AudioSource source, bool play)
    {
        if (play)
        {
            source.Play();
        }
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(a: startVolume, b: endVolume, t: currentTime / duration);
            yield return null;
        }

        // If fading out, stop the audio after fade
        if (!play)
        {
            source.Stop();
        }
    }
}
