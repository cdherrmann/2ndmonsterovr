using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GeneralDebugInformation : MonoBehaviour {

    private void Awake()
    {
        Debug.Log("FYI: Awake");

    }

    private void OnEnable()
    {
        Debug.Log("FYI: OnEnable");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    

	// Use this for initialization
	void Start () {
        Debug.Log("FYI: Start");

    }

    // Update is called once per frame
    private void OnDisable()
    {
        Debug.Log("FYI: OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void OnDestroy()
    {
        Debug.Log("FYI: OnDestroy");
    }

    void OnSceneLoaded (Scene _scene, LoadSceneMode _mode)
    {
        Debug.Log("FYI: Scene of buildindex " + _scene.buildIndex + " was Loaded");
    }

}

