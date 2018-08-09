using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour {

    public string sceneName;
   

 // Use this for initialization
    void Start () {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    
}
