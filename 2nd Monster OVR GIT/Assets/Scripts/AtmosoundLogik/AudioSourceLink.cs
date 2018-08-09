using UnityEngine;
using System.Collections;

public class AudioSourceLink : MonoBehaviour {

    // gets Messages from Sound Event Components, checks if the sound is allready playing, and starts or stops it accordingly

    public AudioSource audioSourceStereo;
    public AudioSource audioSourceAmbisonic;

    [SerializeField] float maxVolume = 1f;
    [SerializeField] float fadeTime = 2f;
    
    public delegate void AudioSourceAction(AudioClip actionAudioClip);
    public static event AudioSourceAction OnPlaybackStart;
    public static event AudioSourceAction OnPlaybackStop;

    private bool isPlaying = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDisable()
    {
        StopPlayback();
    }

    void PlaySounds () {
        if (OnPlaybackStart != null) {
            OnPlaybackStart(audioSourceStereo.clip);
        }
        audioSourceStereo.Play();
        audioSourceAmbisonic.Play();
        Debug.Log("Athmo is playing " + audioSourceStereo.clip);
        Debug.Log("Startvolume at " + audioSourceStereo.volume);
    }

    void StopSounds () {
        if (OnPlaybackStop != null) {
            OnPlaybackStop(audioSourceStereo.clip);
        }
        audioSourceStereo.Stop();
        audioSourceAmbisonic.Stop();
    }

    // The following Functions are used by others as Messaging Targets
    // Instantly Start Playback
    public void StartPlayback()
    {   
        if (!isPlaying) {

            isPlaying = true;

            StopAllCoroutines();

            audioSourceStereo.volume = maxVolume;
            audioSourceAmbisonic.volume = maxVolume;

            PlaySounds();
        }
    }

    // Instantly Stop Playback
    public void StopPlayback()
    {   
        if (isPlaying) {

            StopAllCoroutines();

            audioSourceStereo.volume = 0f;
            audioSourceAmbisonic.volume = 0f;

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
    public void FadeInPlayback() {

        if (!isPlaying) {

            isPlaying = true;

            StopAllCoroutines();
            StartCoroutine("StartAndFadeIn");
        }
    }

    // Fade out sound then stop placback
    public void FadeOutPlayback() {

        if (isPlaying) {
            
            StopAllCoroutines();
            StartCoroutine("FadeOutAndStop");

            isPlaying = false;
        }
    }

    IEnumerator StartAndFadeIn () {
        audioSourceStereo.volume = 0f;
        audioSourceAmbisonic.volume = 0f;

        PlaySounds ();

        float timer = fadeTime;
        while (timer > 0) {
            timer -= Time.deltaTime;
            audioSourceStereo.volume = maxVolume - (timer/fadeTime);
            audioSourceAmbisonic.volume = maxVolume - (timer/fadeTime);
            //Debug.Log("Athmo Volume of " + audioSourceStereo.clip.name + " set to " + audioSourceStereo.volume + " and ist playing, right?:" + audioSourceStereo.isPlaying);
            yield return null;
        }
        audioSourceStereo.volume = maxVolume;
        audioSourceAmbisonic.volume = maxVolume;

        yield return null;
    }

    IEnumerator FadeOutAndStop () {
        audioSourceStereo.volume = maxVolume;
        audioSourceAmbisonic.volume = maxVolume;

        float timer = fadeTime;
        while (timer > 0) {
            timer -= Time.deltaTime;
            audioSourceStereo.volume = timer/fadeTime;
            audioSourceAmbisonic.volume = timer/fadeTime;

            yield return null;
        }
        audioSourceStereo.volume = 0;
        audioSourceAmbisonic.volume = 0;

        StopSounds();

        yield return null;
    }
}
