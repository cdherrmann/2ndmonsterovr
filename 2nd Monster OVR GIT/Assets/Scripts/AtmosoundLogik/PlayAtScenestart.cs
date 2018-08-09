using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayAtScenestart : MonoBehaviour
{

    //  Start playing at Scenestart.

    [SerializeField]
    int[] startAtScenes;

    [SerializeField]
    bool soundFading = false;

	public float delayPlayback = 0f;

    private AthmoAudioSourceLink mySourceLink;

    private int currentSceneNumber;

    private SceneIdentifier currentSceneIdentifier;

    // Use this for initialization
    void Start()
    {
        // find the AudioSourceLink Component
        mySourceLink = gameObject.GetComponent<AthmoAudioSourceLink>();
        if (mySourceLink == null) Debug.Log("NO AudioSourceComponent attached!");

        //find the SceneIdetifier Object and read its currentSceneNumber attribute


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        SceneTimer.earlyFadeIn += OnEarlyFadeIn;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
        SceneTimer.earlyFadeIn -= OnEarlyFadeIn;

    }



    void OnSceneLoad(Scene _scene, LoadSceneMode _mode)
    {
        currentSceneIdentifier = FindObjectOfType<SceneIdentifier>().GetComponent<SceneIdentifier>();
        currentSceneNumber = currentSceneIdentifier.currentSceneNumber;

        //Debug.Log(currentSceneNumber);

        int levelIndexCheck = System.Array.IndexOf(startAtScenes, currentSceneNumber);
        

        
        if (levelIndexCheck != -1)
        {
            StartCoroutine(SendStartPlayback());
        }
    }
        
    void OnEarlyFadeIn ()
    {
        int levelIndexCheck = System.Array.IndexOf(startAtScenes, currentSceneNumber + 1);
    }

    IEnumerator SendStartPlayback()
    {
		yield return new WaitForSeconds(0.1f + delayPlayback);
        if (!mySourceLink.athmoSoundSource.isPlaying) {
            if (soundFading)
            {
                SendMessage("FadeInPlayback");
            }
            else
            {
                SendMessage("StartPlayback");
            }
        }
        yield return null;
    }
}
