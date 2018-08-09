using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    [TextArea]
    public string Notes = "This component manages Scenechanges. It first broadcasts that a scenechange is going to happen, and then after a delay, actually loads the new scene. It also keeps track, of which scene to change to.";

    public static SceneChanger instance;

    public delegate void SceneChangerAction(float delayTime);
    public static event SceneChangerAction OnSceneStart;
    public static event SceneChangerAction OnSceneChangeIssued;


    public float fadeBeforeSceneChange = 2f;
    public float fadeAfterSceneChange = 2f;


    // since this object is a singleton and is never disabled, it can only register its delegate 
    // and never unregister it. Therefore it does it in Awake(), not in OnEnable / OnDisable
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;

        // make sure, this is the only instance
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }

    // Use this for initialization
    void Start () {
        
        //// Make sure this is the only instance of the SceneChanger
        //if (instance != null)
        //{
        //    GameObject.Destroy(gameObject);
        //}
        //else
        //{
        //    GameObject.DontDestroyOnLoad(gameObject);
        //    instance = this;
        //}
        BroadCastLevelLoad(0.01f);
    }
	
	void Update () {
    
    }

    // this is the main function of this component and gets called by other components like the dev input component
    public void LoadNextScene () {       

        string currentSceneName = SceneManager.GetActiveScene().name;
        //int currentSceneCount = SceneManager.sceneCountInBuildSettings;

        if (currentSceneName == "09_Jump") {
            LoadSceneByName("00_Options_Screen");
        } else {
            LoadSceneNr(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    // Initiate the Loading of the Next Scene after a timed delay
    public void LoadSceneNr (int sceneNr)
    {
        BroadcastSceneChange(fadeBeforeSceneChange);        
        StartCoroutine ("LoadSceneAfterDelay", sceneNr);
    }

    // This is used by the dev Input, where you can jump to a certain scene by putting in a number on the keyboard
    public void LoadSceneByName (string sceneName)
    {
        BroadcastSceneChange(fadeBeforeSceneChange);
        StartCoroutine("LoadSceneByNameAfterDelay", sceneName);
    }

    // Broadcast the Event of Scenechange
    void BroadcastSceneChange(float timer)
    {        
        if (OnSceneChangeIssued != null)
        {
            OnSceneChangeIssued(timer);
        }
    }

    // Broadcast New Scene was Loaded
    void BroadCastLevelLoad (float timer)
    {
        if (OnSceneStart != null) {
            OnSceneStart(timer);
        }
    }
     
    // Couroutine to delay the scene loading
    IEnumerator LoadSceneAfterDelay (int sceneNr) {
        yield return new WaitForSeconds(fadeBeforeSceneChange + 0.1f);
        LoadScene(sceneNr);
    }

    // Couroutine to delay the scene loading
    IEnumerator LoadSceneByNameAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(fadeBeforeSceneChange + 0.1f);
        LoadScene(sceneName);
    }

    // Wenn ein Level geladen wurde, kommuniziere den Event und den Fade-Timer
    void OnSceneLoad (Scene _level, LoadSceneMode _mode) {
        //Debug.Log ("Level " + _level.name + " was loaded");
        if (_level.buildIndex != 0)
        {
            BroadCastLevelLoad(fadeAfterSceneChange);
        }
        //BroadCastLevelLoad(fadeAfterSceneChange + 0.1f);
    }

    // actually load the new scene
    void LoadScene (int index)
    {
        SceneManager.LoadScene(index);
    }

    void LoadScene (string name)
    {
        SceneManager.LoadScene(name);
    }

}