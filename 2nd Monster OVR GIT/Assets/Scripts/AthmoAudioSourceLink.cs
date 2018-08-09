using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AthmoAudioSourceLink : MonoBehaviour {
    
    // gets Messages from Sound Event Components, checks if the sound is allready playing, and starts or stops it accordingly

    public AudioSource athmoSoundSource;

    [SerializeField] float maxVolume = 1f;
    [SerializeField] float fadeInTime = 2f;
    [SerializeField] float fadeOutTime = 2f;

    public string[] optionScreenName;
    private string currentSceneName = "";

    private float fastFadeOutTime = 4f;

    public delegate void AudioSourceAction(AudioClip actionAudioClip);
    public static event AudioSourceAction OnPlaybackStart;
    public static event AudioSourceAction OnPlaybackStop;

    public AnimationCurve fadeInCurve;
    public AnimationCurve fadeOutCurve;

    private bool isPlaying = false;


    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSceneLoaded(Scene _sceneName, LoadSceneMode _loadMode)
    {
        currentSceneName = _sceneName.name;
    }

    void OnDisable()
    {
        StopPlayback();
    }

    void PlaySounds()
    {
        if (OnPlaybackStart != null)
        {
            OnPlaybackStart(athmoSoundSource.clip);
        }
        athmoSoundSource.Play();
        //Debug.Log("Athmo is playing " + athmoSoundSource.clip);
        //Debug.Log("Startvolume at " + athmoSoundSource.volume);
    }

    void StopSounds()
    {
        if (OnPlaybackStop != null)
        {
            OnPlaybackStop(athmoSoundSource.clip);
        }
        athmoSoundSource.Stop();
    }

    // The following Functions are used by others as Messaging Targets
    // Instantly Start Playback
    public void StartPlayback()
    {
        if (!isPlaying)
        {

            isPlaying = true;

            StopAllCoroutines();

            athmoSoundSource.volume = maxVolume;

            PlaySounds();
        }
    }

    // Instantly Stop Playback
    public void StopPlayback()
    {
        if (isPlaying)
        {

            StopAllCoroutines();

            athmoSoundSource.volume = 0f;

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
        athmoSoundSource.volume = 0f;

        PlaySounds();

        float timer = fadeInTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            athmoSoundSource.volume = maxVolume * fadeInCurve.Evaluate(1 - (timer / fadeInTime));
            //Debug.Log("Athmo Volume of " + athmoSoundSource.clip.name + " set to " + athmoSoundSource.volume + " and ist playing, right?:" + athmoSoundSource.isPlaying);
            yield return null;
        }
        athmoSoundSource.volume = maxVolume;

        yield return null;
    }

    IEnumerator FadeOutAndStop()
    {
        athmoSoundSource.volume = maxVolume;

        // set timer to FadeOutTime or fastfade if going to the optionsSceen
        float timer = fadeOutTime;
        if (System.Array.IndexOf (optionScreenName, currentSceneName) != -1)
        {
            timer = fastFadeOutTime;
        }
        
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            athmoSoundSource.volume = fadeOutCurve.Evaluate(timer / fadeOutTime);

            yield return null;
        }
        athmoSoundSource.volume = 0;

        StopSounds();

        yield return null;
    }
}
