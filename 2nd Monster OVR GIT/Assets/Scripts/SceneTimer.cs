using UnityEngine;
using System.Collections;

public class SceneTimer : MonoBehaviour {

    [TextArea]
    public string timingsHint = "This component delays scenechanges after the VoiceOver is over. The timings are stored in the VOManager Table.";

    private float minDelayAfterVO = 0f;
    private float maxDelayAfterVO = 0f;

    [TextArea]
    public string preponeHint = "When the Athmo of the next Scene needs to start before the scene changes, propone by x seconds.";
    
    [SerializeField] float preponeFadeIn = 0f;

    
    //    [SerializeField] AudioClip dependingOnThisClip;

    private bool earlySceneEnd = false;

    public delegate void SceneTimerAction();
    public static event SceneTimerAction earlyFadeIn;
    
    private void Awake()
    {
        VoManager.OnVoiceEnds += CompareAudioClips;
    }

    private void OnDestroy()
    {
        VoManager.OnVoiceEnds -= CompareAudioClips;
    }

    //void OnEnable ()
    //{
    //    VoiceOverJukebox.OnVoiceEnds += CompareAudioClips;
    //}

    void OnDisable ()
    {   
        StopAllCoroutines();
        //VoiceOverJukebox.OnVoiceEnds -= CompareAudioClips;
    }

	// Use this for initialization
	void Start () {
        StopAllCoroutines();
        //VoManager voManager = FindObjectOfType<VoManager>();
        //VoTimeTable[] voTables = voManager.GetComponentsInChildren<VoTimeTable>();
        VoTimeTable[] voTables = VoManager.instance.transform.GetComponentsInChildren<VoTimeTable>();

        SceneIdentifier sceneIdentifier = FindObjectOfType<SceneIdentifier>();
        VoTimeTable voTable = null;
        foreach (VoTimeTable table in voTables)
        {
            if (table.sceneNumber == sceneIdentifier.currentSceneNumber)
            {
                voTable = table;
            }
        }
        minDelayAfterVO = voTable.TimeAfterVoMin;
        maxDelayAfterVO = voTable.TimeAfterVoMax;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter () {
        SwitchToEarlySceneEnd();
    }


    void CompareAudioClips(AudioClip compareAudioClip)
    {
//        if (dependingOnThisClip == compareAudioClip)
//        {
            StartCoroutine("CountDownAndEndLevel");
        StartCoroutine(IssueEarlyAthmoFadeIn());
//        }

    }

    public void SwitchToEarlySceneEnd () {
        earlySceneEnd = true;
    } 

    IEnumerator IssueEarlyAthmoFadeIn ()
    {
        yield return new WaitForSeconds(minDelayAfterVO - preponeFadeIn);
        if (earlyFadeIn != null)
        {
            earlyFadeIn();
        }
    }

    IEnumerator CountDownAndEndLevel () {
        yield return new WaitForSeconds(minDelayAfterVO);
        Debug.Log("EarlyExitNowAvailable");
        float timer = maxDelayAfterVO - minDelayAfterVO;
        while (timer >= 0f) {
            timer -= Time.deltaTime;
            if (earlySceneEnd) break;
        }
        InvokeNextScene();
    }

    void InvokeNextScene() {
        SceneChanger.instance.LoadNextScene();
    }
        
}
