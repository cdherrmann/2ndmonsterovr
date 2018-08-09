using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VoiceOverJukebox : MonoBehaviour {

    [SerializeField] AudioSource audioSource;
    [SerializeField] float delayTime = 2f;  
    [SerializeField] float fadeOutTime = 1f;

    private float startVolume = 1f;
    private IEnumerator callBackCoroutine;
    private VOAudioClipHolder myVOClipHolder;

    public delegate void VoJukeBoxOverEvents(AudioClip voiceOverClip);
    public static event VoJukeBoxOverEvents OnVoiceStarts;
    public static event VoJukeBoxOverEvents OnVoiceEnds;


	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        //SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded (Scene _scene, LoadSceneMode _mode) {
        Debug.Log("Voice Jukebox knows, a new Scene was loaded!");
        myVOClipHolder = null;
        delayTime = 2f;
        GameObject VOClipHolderObject = GameObject.Find("VOAudioClip");
       
        if (VOClipHolderObject != null) {
            Debug.Log("Found for a VOClipHolderObject!");

            myVOClipHolder = VOClipHolderObject.GetComponent<VOAudioClipHolder>();
        }
        if (callBackCoroutine != null) {
            StopCoroutine(callBackCoroutine);
        }
        if (myVOClipHolder != null) {
            float VOholderDelay = VOClipHolderObject.GetComponent<VOAudioClipHolder>().delayVoiceOver;
            delayTime = (VOholderDelay > delayTime) ? VOholderDelay : 2f;
            StartCoroutine("SwitchVoiceover");
        } else StopVoiceover();
    }

    IEnumerator SwitchVoiceover () {
        float currentTimer = fadeOutTime;
        float percentage = 1f;

        while (currentTimer > 0) {
            percentage = currentTimer / fadeOutTime;
            currentTimer -= Time.deltaTime;
            audioSource.volume = startVolume * percentage;
            yield return null;
        }

        StopVoiceover();
        PlayVoiceover();
        yield return null;
    }


    void StopVoiceover () {
        audioSource.Stop();
        audioSource.volume = startVolume;

    }

    void PlayVoiceover () {
        StopAllCoroutines();
        float callBackTime = 0f;

        callBackTime = SetAudioClip();
        callBackTime += delayTime;
        callBackCoroutine = CallbackAtEnd(callBackTime);
        StartCoroutine(callBackCoroutine);
        audioSource.PlayDelayed(delayTime);

        // broadcast Event when starting Playback
        if (OnVoiceStarts != null) {
            OnVoiceStarts(audioSource.clip);
        }

    }

    float SetAudioClip () {
        audioSource.clip = myVOClipHolder.voiceOverClip;
        Debug.Log("Set VO-Audioclip to " + audioSource.clip.name);
        return myVOClipHolder.voiceOverClip.length;
    }


    IEnumerator CallbackAtEnd (float time) {
        AudioClip callBackClip = audioSource.clip;
        yield return new WaitForSeconds(time);
        if (OnVoiceEnds != null) {
            OnVoiceEnds(callBackClip);
        }
        Debug.Log(callBackClip.name + " has ended playback.");

    }

}
