using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class FadeInFadeOut : MonoBehaviour {

    public AnimationCurve FadeInCurve;
    public AnimationCurve FadeOutCurve;

    public float fadeInTime;
    public float fadeOutTime;
    
    public Image fadeImage;

    // Use this for initialization
    void Awake () {
    }

    public void makeInvisible ()
    {
        Color tempColor = fadeImage.color;
        tempColor.a = 0f;
        fadeImage.color = tempColor;
    }

    public void makeOpaque ()
    {
        Color tempColor = fadeImage.color;
        tempColor.a = 1f;
        fadeImage.color = tempColor;
    }
	
    public void FadeIn ()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInOverTime());
    }

    public IEnumerator FadeOutAndCallBack (Action<string> onFadeComplete)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutOverTime());
        yield return new WaitForSeconds(fadeOutTime);
        if (onFadeComplete != null)
        {
            onFadeComplete("random string");
        }
    }

    public IEnumerator FadeInOverTime ()
    {
        float timer = 0;
        float percent = 0;

        while (timer <= fadeInTime)
        {
            percent = timer / fadeInTime;
            timer += Time.deltaTime;
            Color tempColor = fadeImage.color;
            tempColor.a = FadeInCurve.Evaluate(percent);
            fadeImage.color = tempColor;
            yield return null;
        }

        yield return null;
    }

    public IEnumerator FadeOutOverTime()
    {
        yield return fadeOutTime;
        float timer = 0;
        float percent = 0;

        while (timer <= fadeOutTime)
        {
            percent = timer / fadeOutTime;
            timer += Time.deltaTime;
            Color tempColor = fadeImage.color;
            tempColor.a = 1 - FadeOutCurve.Evaluate(percent);
            fadeImage.color = tempColor;
            yield return null;
        }
        yield return null;
    }
}
