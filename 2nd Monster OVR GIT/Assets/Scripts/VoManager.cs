using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VoManager : MonoBehaviour {

    public static VoManager instance;

    public VoTimeTable voTimeTable;

    [Range(0f,1f)]
    public float maxVolume = 1f;

    //private VOAudioClipHolder myVOClipHolder;
    private SceneIdentifier sceneIdentifier;



    AudioSource audioSource;
    float delayTime = 2f;
    float fadeOutTime = 1f;

    
    private IEnumerator callBackCoroutine;
    private VoTimeTable voTable = null;


    public delegate void VoiceOverEvents(AudioClip voiceOverClip);
    public static event VoiceOverEvents OnVoiceStarts;
    public static event VoiceOverEvents OnVoiceEnds;

   

    // Use this for initialization
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
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
        StopAllCoroutines();
    }

    void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        //Debug.Log("Voice Jukebox knows, a new Scene was loaded!");
        sceneIdentifier = null;
        sceneIdentifier = FindObjectOfType<SceneIdentifier>().GetComponent<SceneIdentifier>();
        delayTime = 2f;
       

        if (callBackCoroutine != null)
        {
            StopCoroutine(callBackCoroutine);
        }

        // Check if there is Component in the VoTable for the current scene. If there is, switch to that voiceover.
        // If not. Stop playing voiceover.
        VoTimeTable[] timeTableComponents = gameObject.GetComponentsInChildren<VoTimeTable>();
        voTable = null;

        foreach (VoTimeTable table in timeTableComponents)
        {
            if (table.sceneNumber == sceneIdentifier.currentSceneNumber)
            {
                voTable = table;
            }
        }

        if (voTable != null)
        {   
            float VOholderDelay = voTable.VoStartTime;
            delayTime = (VOholderDelay > delayTime) ? VOholderDelay : 2f;
            StartCoroutine("SwitchVoiceover");
        }
        else StopVoiceover();
    }

    IEnumerator SwitchVoiceover()
    {
        float currentTimer = fadeOutTime;
        float percentage = 1f;

        while (currentTimer > 0)
        {
            percentage = currentTimer / fadeOutTime;
            currentTimer -= Time.deltaTime;
            audioSource.volume = maxVolume * percentage;
            yield return null;
        }

        StopVoiceover();
        PlayVoiceover();
        yield return null;
    }


    void StopVoiceover()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.volume = maxVolume;
        }
    }

    void PlayVoiceover()
    {
        StopAllCoroutines();
        float callBackTime = 0f;

        callBackTime = SetAudioClip();
        callBackTime += delayTime;
        callBackCoroutine = CallbackAtEnd(callBackTime);
        StartCoroutine(callBackCoroutine);
        //Debug.Log("Play VO with delay of " + delayTime + " seconds.");
        audioSource.PlayDelayed(delayTime);

        StartCoroutine(BroadcastPlayWithDelay());

    } 

    float SetAudioClip()
    {
        audioSource.clip = voTable.audioClip;
        //Debug.Log("Set VO-Audioclip to " + audioSource.clip.name);
        return voTable.audioClip.length;
    }

    IEnumerator BroadcastPlayWithDelay ()
    {
        // broadcast Event when starting Playback
        if (OnVoiceStarts != null)
        {
            yield return new WaitForSeconds(delayTime);
            OnVoiceStarts(audioSource.clip);
        }
        else yield return null;
        
    }

    IEnumerator CallbackAtEnd(float time)
    {
        AudioClip callBackClip = audioSource.clip;
        yield return new WaitForSeconds(time);
        if (OnVoiceEnds != null)
        {
            OnVoiceEnds(callBackClip);
        }
        //Debug.Log(callBackClip.name + " has ended playback.");

    }
}
