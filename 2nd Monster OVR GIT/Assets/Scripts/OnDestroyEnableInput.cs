using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDestroyEnableInput : MonoBehaviour {

    public string sceneName = "00_Options_Screen";

    private void Awake()
    {
        SceneManager.sceneLoaded += DisableInput;
    }

    // Use this for initialization
    void Start () {

    }

    void DisableInput (Scene _scene, LoadSceneMode _mode)
    {
        if(_scene.name == sceneName)
        {
            InputManager.instance.enabled = false;

        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        InputManager.instance.enabled = true;
        SceneManager.sceneLoaded -= DisableInput;
        
    }
}
