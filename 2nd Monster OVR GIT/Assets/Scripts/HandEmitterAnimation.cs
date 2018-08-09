using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEmitterAnimation : MonoBehaviour {

    public float startRate;
    public float endRate;
    public float delayTime;
    public float fadeTime;

    public ParticleSystem[] particleSystems;

	// Use this for initialization
	void Start () {
        StopAllCoroutines();
        foreach (ParticleSystem p in particleSystems) {
            
            StartCoroutine(FadeInParticles(p));
        }        
	}
	
    IEnumerator FadeInParticles(ParticleSystem _p) {
        var emission = _p.emission;
        emission.rateOverTimeMultiplier = startRate;

        yield return new WaitForSeconds(delayTime);

        float percent = 0f;
        for (float timer = 0f; timer < fadeTime; timer += Time.deltaTime)
        {
            percent = timer / fadeTime;
            emission.rateOverTimeMultiplier = Mathf.Lerp(startRate, endRate, percent);
            yield return null;
        }

        yield return null;
    }
    	
}
