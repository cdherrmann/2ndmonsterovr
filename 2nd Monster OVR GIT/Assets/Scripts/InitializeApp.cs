using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeApp : MonoBehaviour {

    public string[] scenesToLoad;
    public Scene[] sceneList;

    private void Awake()
    {
 
    }

    // Use this for initialization
    void Start () {
        Coroutine loadScenesRoutine = StartCoroutine(MergeScenesInList(scenesToLoad));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator MergeScenesInList (string[] sceneList)
    {
        LoadScenes(scenesToLoad);
        yield return new WaitForEndOfFrame();

        foreach (string scene in sceneList)
        {
            GameObject[] sceneRootObjects = SceneManager.GetSceneByName(scene).GetRootGameObjects();
            ParentToThis(sceneRootObjects);
        }

        UnLoadScenes(scenesToLoad);

        yield return null;
    }

    private void LoadScenes (string[] scenes) {
        foreach (string scene in scenes) { 
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }

    private void ParentToThis (GameObject[] gameObjects)
    {
        foreach (GameObject go in gameObjects)
        {
            go.transform.SetParent(gameObject.transform);
        }
    }

    private void UnLoadScenes (string[] scenes)
    {
        foreach (string scene in scenes)
        {
            SceneManager.UnloadSceneAsync(scene);
        }
    }

}
