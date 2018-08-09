using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowCanvasThenDestroy : MonoBehaviour {

    public float displayTime;

    public string sceneName;

    public FadeInFadeOut[] faderComponents;


    private void Awake()
    {
        SceneManager.sceneLoaded += startFadeIn;
        SceneManager.sceneLoaded += startFadeOutAndDestroy;


    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= startFadeIn;
        SceneManager.sceneLoaded -= startFadeOutAndDestroy;

    }

    void startFadeIn (Scene _scene, LoadSceneMode _mode)
    {
        if (_scene.name == sceneName)
        {
            faderComponents[0].FadeIn();
        }
    }

    void startFadeOutAndDestroy (Scene _scene, LoadSceneMode _mode)
    {
        if (_scene.name == sceneName)
        {
            Invoke("CallFadOutWithCallBack", displayTime);
        }
    }

    void CallFadOutWithCallBack()
    {
        StartCoroutine(faderComponents[0].FadeOutAndCallBack(DestroyThisObject));
    }

    void DestroyThisObject(string message)
    {
        Destroy(transform.root.gameObject);
    }



}
