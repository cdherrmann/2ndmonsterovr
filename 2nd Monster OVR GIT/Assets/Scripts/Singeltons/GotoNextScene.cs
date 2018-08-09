using UnityEngine;
using System.Collections;

public class GotoNextScene : MonoBehaviour {

    private void Awake()
    {
        if (Debug.isDebugBuild) { 
            InputManager.OnTrippleClick += LoadNextScene;
        }

    }

    

    void LoadNextScene ()
    {
        SceneChanger.instance.LoadNextScene();
    }

}
