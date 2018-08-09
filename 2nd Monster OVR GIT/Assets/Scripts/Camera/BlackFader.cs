/*
	This allows you to easily fade a UI Image. 
	If an object is already partially faded it will continue from there. 
	If you choose a different speed, it will use the new speed. 
 
	NOTE: It is a copy of the Material based script, adapted to an UI-image.  
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlackFader : MonoBehaviour {

    // publically editable speed
    public float fadeDelay = 0.0f;
    public float setFadeTime = 0.5f;
    public bool fadeInOnStart = false;
    public bool fadeOutOnStart = false;
    public Image blackImage;

    private bool logInitialFadeSequence = false;
    private float fadeTime;

    public delegate void FaderAction();
    public static event FaderAction FadeOutDone;
    public static event FaderAction FadeInDone;


    // store colours
    private Color baseColor;


    // allow automatic fading on the start of the scene
    IEnumerator Start()
    {
        //yield return null; 

        yield return new WaitForSeconds(fadeDelay);


        if (fadeInOnStart)
        {
            logInitialFadeSequence = true;
            FadeIn();
        }

        if (fadeOutOnStart)
        {
            FadeOut(fadeTime);
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

    void FadeIn()
    {
        FadeIn(fadeTime);
    }

    void FadeOut()
    {
        FadeOut(fadeTime);
    }

    void FadeIn(float newFadeTime)
    {
        StopAllCoroutines();
        StartCoroutine("FadeSequence", newFadeTime);
    }

    void FadeOut(float newFadeTime)
    {
        StopAllCoroutines();
        StartCoroutine("FadeSequence", -newFadeTime);
    }

    // fade sequence
    IEnumerator FadeSequence(float fadingOutTime)
    {

        // log fading direction, then precalculate fading speed as a multiplier
        bool fadingOut = (fadingOutTime < 0.0f);
        float fadingOutSpeed = 1.0f / fadingOutTime;


        // store the original basecolor of the image
        baseColor = blackImage.color;

        // make the image visable
        blackImage.enabled = true;

        // get currentalpha
        float alphaValue = blackImage.color.a;

        // This is a special case for objects that are set to fade in on start. 
        // it will treat them as alpha 0, despite them not being so. 
        if (logInitialFadeSequence && !fadingOut)
        {
            alphaValue = 0.0f;
            logInitialFadeSequence = false;
        }

        // iterate to change alpha value 
        while ((alphaValue >= 0.0f && fadingOut) || (alphaValue <= 1.0f && !fadingOut))
        {
            alphaValue += Time.deltaTime * fadingOutSpeed;


            Color newColor = blackImage.color;
            //newColor.a = Mathf.Min(newColor.a, alphaValue);
            newColor.a = alphaValue;
            newColor.a = Mathf.Clamp(newColor.a, 0.0f, 1.0f);
            blackImage.color = newColor;

            yield return null;
        }


        // turn objects off after fading out
        if (fadingOut)
        {
            blackImage.enabled = false;

            // Broadcast FadeOutDone Event
            if (FadeOutDone != null)
            {
                FadeOutDone();
            }
        }
        else
        {
            // Broadcast FadeInDone Event
            if (FadeInDone != null)
            {
                FadeInDone();
            }
        }



    }


}
