using UnityEngine;
using System.Collections;

public class PlayWhenOtherEnds : MonoBehaviour {

    [SerializeField] 
    AudioClip playAfterThisClip;
    [SerializeField]
    bool soundFading = false;

    void OnEnable () {
        VoManager.OnVoiceEnds += StartPlaybackAfterSound;
    }


    void OnDisable () {
        VoManager.OnVoiceEnds -= StartPlaybackAfterSound;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void StartPlaybackAfterSound (AudioClip otherSoundClip){
        if (otherSoundClip == playAfterThisClip) {
            if (soundFading) {
                SendMessage("FadeInPlayback");
            } else {
                SendMessage("StartPlayback");
            }
        };
    }

    void SendStopPlayback()
    {
        if (soundFading) {
            SendMessage ("FadeOutPlayback");
        } else {
            SendMessage("StopPlayback");
        }
    }
}
