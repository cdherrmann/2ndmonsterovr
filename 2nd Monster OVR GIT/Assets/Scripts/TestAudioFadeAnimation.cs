using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudioFadeAnimation : MonoBehaviour {

    public AudioSource audioSource;

    public AnimationCurve animationCurve;

    public float waitTime;
    float percentage = 1f;
    public float fadeOutTime = 10f;
    float maxVolume = 1f;

    // Use this for initialization
    void Start () {

        StartCoroutine(FadeSound());
	}

    IEnumerator FadeSound()
    {
        yield return new WaitForSeconds(waitTime);

        float currentTimer = fadeOutTime;

        while (currentTimer > 0)
        {
            percentage = currentTimer / fadeOutTime;
            currentTimer -= Time.deltaTime;
            audioSource.volume = animationCurve.Evaluate(percentage) * maxVolume;
            yield return null;
        }

        
        yield return null;
    }

}
