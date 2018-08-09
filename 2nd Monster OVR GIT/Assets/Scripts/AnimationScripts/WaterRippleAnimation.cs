using UnityEngine;
using System.Collections;

public class WaterRippleAnimation : MonoBehaviour {

    [SerializeField]
    float cycleOffset = 0f;

    [SerializeField]
    float cycleLength = 10f;

    [SerializeField]
    float noiseFrequence = 5f;

    private SkinnedMeshRenderer mySkinnedMeshRenderer;

    // Use this for initialization
    void Start () {
        mySkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        StartCoroutine("delayAnimation");
    }

    IEnumerator delayAnimation ()
    {
        yield return new WaitForSeconds(cycleOffset);
        startAnimationLoop();
    }

    IEnumerator fadeIn()
    {
        float fadeTimer = 0f;
        float fadeDuration = cycleLength / 9f;
        Material mat = mySkinnedMeshRenderer.material;
        Color newColor = mySkinnedMeshRenderer.material.color;
        while (fadeTimer <= fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            newColor.a = fadeTimer / fadeDuration;
            mat.color = newColor; 
            yield return null;
        }
        yield return new WaitForSeconds(cycleLength - 2 * fadeDuration);
        StartCoroutine("fadeOut");
    }

    IEnumerator fadeOut ()
    {
        float fadeTimer = 0f;
        float fadeDuration = cycleLength / 9f;
        Material mat = mySkinnedMeshRenderer.material;
        Color newColor = mySkinnedMeshRenderer.material.color;
        while (fadeTimer <= fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            newColor.a = 1 - fadeTimer / fadeDuration;
            mat.color = newColor;
            yield return null;
        }

    }

    IEnumerator noiseUp ()
    {
        float noiseTimer = 0f;
        float noiseDuration = (cycleLength / noiseFrequence);
        while (noiseTimer <= noiseDuration)
        {
            noiseTimer += Time.deltaTime;
            mySkinnedMeshRenderer.SetBlendShapeWeight(2, 100 * (noiseTimer / noiseDuration));
            yield return null;
        }
        StartCoroutine("noiseDown");
    }

    IEnumerator noiseDown ()
    {
        float noiseTimer = 0f;
        float noiseDuration = (cycleLength / noiseFrequence);
        while (noiseTimer <= noiseDuration)
        {
            noiseTimer += Time.deltaTime;
            mySkinnedMeshRenderer.SetBlendShapeWeight(2, 100 - 100 * (noiseTimer / noiseDuration));
            yield return null;
        }
        StartCoroutine("noiseUp");
    }

    IEnumerator growingRipples ()
    {        
        float blendWeight = 1f;
        float growTimer = 0f;

        while (growTimer <= cycleLength)
        {
            yield return null;
            growTimer += Time.deltaTime;
            blendWeight = 100 - 100*(growTimer / cycleLength);
            mySkinnedMeshRenderer.SetBlendShapeWeight(0, blendWeight);
            mySkinnedMeshRenderer.SetBlendShapeWeight(1, blendWeight);
        }
        growTimer = 0f;
        yield return new WaitForSeconds(0.1f);
        startAnimationLoop();
    }

    void startAnimationLoop ()
    {
        StartCoroutine("growingRipples");
        StartCoroutine("noiseUp");
        StartCoroutine("fadeIn");
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
		
}
