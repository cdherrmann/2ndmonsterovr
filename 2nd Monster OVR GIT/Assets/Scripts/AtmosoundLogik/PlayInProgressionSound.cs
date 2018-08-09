using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayInProgressionSound : MonoBehaviour {

    // Listen for the Start of another Soundclip and play this clip after a given period of time

    [SerializeField]
    AudioClip dependingOnThisClip;
    [SerializeField]
    float playAfterTime = 0f;
    [SerializeField]
    bool soundFading = false;

    private AthmoAudioSourceLink mySourceLink;

    private int sceneCheck=0;

    void OnEnable ()
    {
        AthmoAudioSourceLink.OnPlaybackStart += CompareAudioClips;
        VoManager.OnVoiceStarts += CompareAudioClips;
    }

    void OnDisable ()
    {
        AthmoAudioSourceLink.OnPlaybackStart -= CompareAudioClips;
        VoManager.OnVoiceStarts -= CompareAudioClips;
    }

    // Use this for initialization
    void Start () {
        // find the AudioSourceLink Component
        mySourceLink = gameObject.GetComponent<AthmoAudioSourceLink>();
        if (mySourceLink == null) Debug.Log("NO AudioSourceComponent attached!");
    }

    // Update is called once per frame
    void Update () {
	
	}

    void CompareAudioClips(AudioClip compareAudioClip)
    {
        //Debug.Log("Compare Clips " + compareAudioClip + " and " + dependingOnThisClip);
        if (dependingOnThisClip == compareAudioClip)
        {
            sceneCheck = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine("PlayAfterDelay", playAfterTime);
        }

    }

    IEnumerator PlayAfterDelay (float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (SceneManager.GetActiveScene().buildIndex == sceneCheck) SendStartPlayback();
    }


    void SendStartPlayback()
    {   
        if (soundFading) {
            SendMessage ("FadeInPlayback");
        } else {
            SendMessage("StartPlayback");
        }
    }

}
