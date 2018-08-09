using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndInProgression : MonoBehaviour {

    // Listen for the Start of another Soundclip and stop this clip after a given period of time

    [SerializeField]
    AudioClip dependingOnThisClip;
    [SerializeField]
    float endAfterTime = 0f;
    [SerializeField]
    bool soundFading = false;

    private AthmoAudioSourceLink mySourceLink;

    private int sceneCheck = 0;

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
        if (mySourceLink == null) Debug.LogError("NO AudioSourceComponent attached!");
    }


    void CompareAudioClips(AudioClip compareAudioClip)
    {
        if (dependingOnThisClip == compareAudioClip)
        {
            //sceneCheck = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine("StopAfterDelay", endAfterTime);
        }

    }

    IEnumerator StopAfterDelay (float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //if (SceneManager.GetActiveScene().buildIndex == sceneCheck)
        SendStopPlayback();
    }

    void SendStopPlayback()
    {
        StopAllCoroutines();
        if (soundFading) {
            SendMessage ("FadeOutPlayback");
        } else {
            SendMessage("StopPlayback");
        }
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
