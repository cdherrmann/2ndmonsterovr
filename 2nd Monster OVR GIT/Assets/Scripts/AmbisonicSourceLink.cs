using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbisonicSourceLink : MonoBehaviour {

    // gets Messages from Sound Event Components, checks if the sound is allready playing, and starts or stops it accordingly

    public AudioSource ambisonicSource;

    [SerializeField] float maxVolume = 1f;
    [SerializeField] float fadeTime = 2f;

    public delegate void AudioSourceAction(AudioClip actionAudioClip);
    public static event AudioSourceAction OnPlaybackStart;
    public static event AudioSourceAction OnPlaybackStop;

    private bool isPlaying = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        StopPlayback();
    }

    void PlaySounds()
    {
        if (OnPlaybackStart != null)
        {
            OnPlaybackStart(ambisonicSource.clip);
        }
        ambisonicSource.Play();
        Debug.Log("Athmo is playing " + ambisonicSource.clip);
        Debug.Log("Startvolume at " + ambisonicSource.volume);
    }

    void StopSounds()
    {
        if (OnPlaybackStop != null)
        {
            OnPlaybackStop(ambisonicSource.clip);
        }
        ambisonicSource.Stop();
    }

    // The following Functions are used by others as Messaging Targets
    // Instantly Start Playback
    public void StartPlayback()
    {
        if (!isPlaying)
        {

            isPlaying = true;

            StopAllCoroutines();

            ambisonicSource.volume = maxVolume;

            PlaySounds();
        }
    }

    // Instantly Stop Playback
    public void StopPlayback()
    {
        if (isPlaying)
        {

            StopAllCoroutines();

            ambisonicSource.volume = 0f;

            StopSounds();

            isPlaying = false;
        }
    }

    public void RestartPlayback()
    {
        StopPlayback();
        StartPlayback();
    }

    // Start Playback and fade in sound
    public void FadeInPlayback()
    {

        if (!isPlaying)
        {

            isPlaying = true;

            StopAllCoroutines();
            StartCoroutine("StartAndFadeIn");
        }
    }

    // Fade out sound then stop placback
    public void FadeOutPlayback()
    {

        if (isPlaying)
        {

            StopAllCoroutines();
            StartCoroutine("FadeOutAndStop");

            isPlaying = false;
        }
    }

    IEnumerator StartAndFadeIn()
    {
        ambisonicSource.volume = 0f;

        PlaySounds();

        float timer = fadeTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            ambisonicSource.volume = maxVolume - (timer / fadeTime);
            //Debug.Log("Athmo Volume of " + ambisonicSource.clip.name + " set to " + ambisonicSource.volume + " and ist playing, right?:" + ambisonicSource.isPlaying);
            yield return null;
        }
        ambisonicSource.volume = maxVolume;

        yield return null;
    }

    IEnumerator FadeOutAndStop()
    {
        ambisonicSource.volume = maxVolume;

        float timer = fadeTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            ambisonicSource.volume = timer / fadeTime;

            yield return null;
        }
        ambisonicSource.volume = 0;

        StopSounds();

        yield return null;
    }
}
