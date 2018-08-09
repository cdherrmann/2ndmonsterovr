using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndIfWrongScene : MonoBehaviour {

    [TextArea]
    public string MyTextArea = "On Sceneload stops sound, unless the new scene is on the 'allow'-list.";

    [SerializeField]
    int[] allowedScenes;
    [SerializeField]
    bool soundFading = false;

    private int currentSceneNumber;

    private SceneIdentifier currentSceneIdentifier;


    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;

    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    // Check if this sound should be played in the current scene. If not, stop playback.
    void OnSceneLoad (Scene _level, LoadSceneMode _mode)
    {
        StopAllCoroutines();
        StartCoroutine(CheckNewScene());
    }

    IEnumerator CheckNewScene ()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        currentSceneIdentifier = FindObjectOfType<SceneIdentifier>().GetComponent<SceneIdentifier>();
        currentSceneNumber = currentSceneIdentifier.currentSceneNumber;

        //Debug.Log(currentSceneNumber);

        int levelIndexCheck = System.Array.IndexOf(allowedScenes, currentSceneNumber);

        if (levelIndexCheck == -1)
        {
            //            Debug.Log("StoppedPlayback");
            SendStopPlayback();
        }
        yield return null;
    }

    void SendStopPlayback()
    {   
        if (soundFading) {
            SendMessage("FadeOutPlayback");
        } else {
            SendMessage("StopPlayback");
        }
    }
}
