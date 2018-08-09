using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnKeyframe : MonoBehaviour {

    public AudioSource goalAudioSource;

	void activateAudioSource ()
    {
        goalAudioSource.gameObject.SetActive(true);
        goalAudioSource.Play();
    }
}
