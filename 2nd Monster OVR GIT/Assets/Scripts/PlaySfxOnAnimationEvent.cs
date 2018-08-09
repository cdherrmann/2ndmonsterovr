using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySfxOnAnimationEvent : MonoBehaviour {

    public GameObject soundObject;

	public void PlaySounds ()
    {
        AudioSource[] audioSources = soundObject.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            source.Play();
        }
    }
}
