using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundAfterSeconds : MonoBehaviour {

    public AudioSource[] audioSources;

    public float fireSoundAtTime = 0f;

    bool fired = false;

    float timer = 0f;

	// Use this for initialization
	void Start () {
        StopAllCoroutines();
        StartCoroutine(PlayAfterTime(fireSoundAtTime));
	}
	
    IEnumerator PlayAfterTime(float _time)
    {
        yield return new WaitForSeconds(_time);
        foreach (AudioSource source in audioSources)
        {
            source.Play();
        }
        yield return null;
    }

}
