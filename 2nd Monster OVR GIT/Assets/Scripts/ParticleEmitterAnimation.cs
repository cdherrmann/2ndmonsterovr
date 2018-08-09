using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitterAnimation : MonoBehaviour {

    public ParticleSystem[] particleSystems;

    public bool animateRate;

    public float startRate;
    public float endRate;
    public float rDelayTime;
    public float rFadeTime;

    public bool animateInheritVelocity;

    public float startVelocity;
    public float endVelocity;
    public float vDelayTime;
    public float vFadeTime;


    // Use this for initialization
    void Start()
    {
        StopAllCoroutines();
        if (animateRate) { AnimateRate(); }
        if (animateInheritVelocity) { AnimateInheritVelocity(); }
        
    }

    void AnimateRate ()
    {
        foreach (ParticleSystem p in particleSystems)
        {

            StartCoroutine(FadeInParticles(p));
        }
    }

    void AnimateInheritVelocity ()
    {
        foreach (ParticleSystem p in particleSystems)
        {

            StartCoroutine(FadeVelocity(p));
        }
    }

    IEnumerator FadeInParticles(ParticleSystem _p)
    {
        var emission = _p.emission;
        emission.rateOverTimeMultiplier = startRate;

        yield return new WaitForSeconds(rDelayTime);

        float percent = 0f;
        for (float timer = 0f; timer < rFadeTime; timer += Time.deltaTime)
        {
            percent = timer / rFadeTime;
            emission.rateOverTimeMultiplier = Mathf.Lerp(startRate, endRate, percent);
            yield return null;
        }

        yield return null;
    }

    IEnumerator FadeVelocity (ParticleSystem _p)
    {
        var inheritVelocity = _p.inheritVelocity;
        inheritVelocity.curveMultiplier = startVelocity;

        yield return new WaitForSeconds(vDelayTime);

        float percent = 0f;
        for (float timer = 0f; timer < vFadeTime; timer += Time.deltaTime)
        {
            percent = timer / vFadeTime;
            inheritVelocity.curveMultiplier = Mathf.Lerp(startVelocity, endVelocity, percent);
            yield return null;
        }

        yield return null;
    }

}

