/*
This allows you to easily fade an object and its children.
If an object is already partially faded it will continue from there. 

If you choose a different speed, it will use the new speed.

NOTE: Requires an canvas image.
*/
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// this class inherits from FadeInAndOut
public class FadeInAndOutUiImage : MonoBehaviour

{
    // store UI Image
    public Image uiImage;

    public AnimationCurve fadeInCurve;
    public AnimationCurve fadeOutCurve;

    // publically editable speed
    public float fadeDelay = 0.0f;
    public float setFadeTime = 0.5f;
    public bool fadeInOnStart = false;
    public bool fadeOutOnStart = false;
    private bool logInitialFadeSequence = false;


    // allow automatic fading on the start of the scene
    IEnumerator Start()
    {
        //yield return null; 

        yield return new WaitForSeconds(fadeDelay);


        if (fadeInOnStart)
        {
            logInitialFadeSequence = true;
            FadeIn(setFadeTime);
        }

        if (fadeOutOnStart)
        {
            logInitialFadeSequence = true;
            FadeOut(setFadeTime);
        }
    }

    void OnEnable()
    {
        SceneChanger.OnSceneStart += FadeOut;
        SceneChanger.OnSceneChangeIssued += FadeIn;
    }

    void OnDisable()
    {
        SceneChanger.OnSceneStart -= FadeOut;
        SceneChanger.OnSceneChangeIssued -= FadeIn;
    }


    void FadeIn(float newFadeTime)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInSequence (newFadeTime));

    }

    void FadeOut(float newFadeTime)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutSequence(newFadeTime));

    }

    // fade sequences
    IEnumerator FadeInSequence(float _fadeTime)
    {
        // make image visable
        uiImage.enabled = true;
        
        // get current alpha
        float startAlphaValue = uiImage.color.a;
        
        
        // This is a special case for objects that are set to fade in on start. 
        // it will treat them as alpha 0, despite them not being so. 
        if (logInitialFadeSequence)
        {
            startAlphaValue = 0.0f;
            logInitialFadeSequence = false;
        }


        float timer = 0f;
        float percentage = 0f;
        float newAlpha = 0f;
        Color newColor = uiImage.color;

        // iterate to change alpha value 
        while (timer < _fadeTime)
        {
            percentage = fadeInCurve.Evaluate(timer / _fadeTime);
            newAlpha = Mathf.Lerp(startAlphaValue, 1f, percentage);
            newColor.a = Mathf.Clamp(newAlpha, 0.0f, 1.0f);
            uiImage.color = newColor;

            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOutSequence(float _fadeTime)
    {
        // get current alpha
        float startAlphaValue = uiImage.color.a;

        // This is a special case for objects that are set to fade in on start. 
        // it will treat them as alpha 0, despite them not being so. 
        if (logInitialFadeSequence)
        {
            startAlphaValue = 1.0f;
            logInitialFadeSequence = false;
        }


        // This is a special case for objects that are set to fade in on start. 
        // it will treat them as alpha 0, despite them not being so. 

        float timer = 0f;
        float percentage = 0f;
        float newAlpha = 0f;
        Color newColor = uiImage.color;

        // iterate to change alpha value 
        while (timer < _fadeTime)
        {
            percentage = 1 - fadeOutCurve.Evaluate(timer / _fadeTime);
            
            newAlpha = Mathf.Lerp(0f, startAlphaValue, percentage);            
            newColor.a = Mathf.Clamp(newAlpha, 0.0f, 1.0f);
            uiImage.color = newColor;

            timer += Time.deltaTime;
            yield return null;
        }

        // turn objects off after fading out
        uiImage.enabled = false;
    }


}
