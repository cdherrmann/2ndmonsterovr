using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ShowSplashUIThenDestroy : MonoBehaviour {

    // until a certain scene has been loaded shows the splashscreen with fading logo and background. then deletes itself.
    
    public float displayTime;

    // Scene to load while Splashscreen is showing.
    public string sceneName;

    // Background Image Fading Component
    public FadeInFadeOut backgroundFader;
    // Logo Image Fading Component
    public FadeInFadeOut logoFader;

    //Basically how long the background will last, before and after the logo has faded in and out.
    public float fadePaddingTimeBefore;
    public float fadePaddingTimeAfter;

    private void Awake()
    {
        SceneManager.sceneLoaded += StartFadeSquence;
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= StartFadeSquence;
    }

    private void Start()
    {
        backgroundFader.makeOpaque();
        logoFader.makeInvisible();
    }

    void StartFadeSquence(Scene _scene, LoadSceneMode _mode)
    {
        if (_scene.name == sceneName)
        {
            StartCoroutine(FadeInSquence());
        }
    }

    IEnumerator FadeInSquence ()
    {
        yield return new WaitForSeconds(fadePaddingTimeBefore);
        // Fade in Logo (Background is allready visible)
        logoFader.FadeIn();
        // Wait for as long as the Splashscreen should show
        yield return new WaitForSeconds(displayTime);
        // Fade out Logo
        StartCoroutine(logoFader.FadeOutOverTime());
        // Wait for the Logo to Fade out
        yield return new WaitForSeconds(logoFader.fadeOutTime + fadePaddingTimeAfter);
        // Fade out Background, and when done destroy this
        StartCoroutine(backgroundFader.FadeOutAndCallBack(DestroyThisObject));
        yield return null;
    }

    void DestroyThisObject(string message)
    {
        Destroy(transform.root.gameObject);
    }



}
