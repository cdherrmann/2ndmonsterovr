using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuSingelton : MonoBehaviour {

    static MainMenuSingelton instance;

    private Canvas myCanvas;

    // Use this for initialization
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        myCanvas = gameObject.GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;

    }

    void OnSceneLoad (Scene _level, LoadSceneMode _mode)
    {
        if (_level.name == "00_Options_Screen")
        {
            myCanvas = gameObject.GetComponent<Canvas>();
            myCanvas.enabled = true;
        }
        else
        {
            myCanvas = gameObject.GetComponent<Canvas>();
            myCanvas.enabled = false;
        }
    }
}
