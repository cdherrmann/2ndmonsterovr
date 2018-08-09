using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndAtScenestart : MonoBehaviour {
    [Header("This is deprecated. Use 'EndIfWrongScene' Component instead!")]

    [SerializeField]
    int endAtScene = 1;
    [SerializeField]
    bool soundFading = false;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    // Check if this is the Scene, where this sound should end. If so stop playback.
    void OnSceneLoad(Scene _scene, LoadSceneMode _mode)
    {
        if (_scene.buildIndex == endAtScene)
        {
            SendStopPlayback();
        }
    }


    void SendStopPlayback ()
    {   
        if (soundFading) {
            SendMessage("FadeOutPlayback");
        } else {
            SendMessage("StopPlayback");
        }
    }
}
