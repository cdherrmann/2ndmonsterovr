using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetInScene : MonoBehaviour {
    public string creditsSceneName = "09_Jump";
    private string currentSceneName = "";
    
    public Animator myAnimator;
    
    public ResetInScene creditsPrefab;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        //SetStartValues();
    }

    private void Update()
    {
    }

 

    private void OnSceneLoaded(Scene _sceneName, LoadSceneMode _loadMode)
    {

        currentSceneName = _sceneName.name;
        bool creditSceneBool = (currentSceneName == creditsSceneName);
        //Debug.Log("In Credits Scene? " + creditSceneBool);

        if (creditSceneBool)
        {
            myAnimator.SetBool("Playable", true);
        } else
        {
            myAnimator.SetBool("Playable", false);
            myAnimator.SetBool("CreditsRoll", false);
        }

    }





}
