using UnityEngine;
using System;

using System.Collections;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class CameraSwitcher : MonoBehaviour {
    [TextArea]
    public string Notes = "This component controls which camera is used in any given situation. It also sticks the fadescoop to the current camera. It needs the VRModeSwitch to actually perform the switch.";

    public static CameraSwitcher instance;

    //private OptionsTracker myOptionsTracker;

    [SerializeField]
    GameObject gameCamera;

    [SerializeField]
    GameObject menuCamera;

    [SerializeField]
    PositionConstraint fadeScoopConstraint;

    // Use this for initialization

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }


    void Start () {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        StartCoroutine(InitializeObject());

    }

    IEnumerator InitializeObject ()
    {
        yield return new WaitForEndOfFrame();
        VRModeSwitch.instance.SwitchToMenuCamera();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        StartCoroutine(CheckCameraAndSwitch(_scene));
    }

    void OnSceneUnloaded(Scene _scene)
    {
    }

    IEnumerator CheckCameraAndSwitch (Scene _scene)
    {
        yield return new WaitForEndOfFrame();
        if (_scene.name == "00_Options_Screen")
        {
           //AttachScoopToMenuCam();
            // Mit einem kleinen Delay zur Menu-Kamera wechseln, damit die Cardboardcam das so kurz nach dem Levelload auch checkt
            StartCoroutine(SwitchToMenuCamDelayed());
        } else
        {
            VRModeSwitch.instance.SwitchToGameCamera();
            AttachScoopToGameCam();
            CheckCardboardVRMode();
        }
        yield return null;
    }


    void AttachScoopToGameCam()
    {
        //fadeScoopConstraint.targetObject = gameCamera.gameObject;
    }

    void AttachScoopToMenuCam()
    {
        //fadeScoopConstraint.targetObject = menuCamera.gameObject;
    }

    void CheckCardboardVRMode()
    {
            if (OptionsTracker.instance.cardboardMode)
            {
                VRModeSwitch.instance.EnableStereoscopy();
                //Debug.Log("Stereoscopy Enabled");
            }
            else
            {
                VRModeSwitch.instance.DisableStereoscopy();
                //Debug.Log("Stereoscopy Disabled");
            }

    }

    IEnumerator SwitchToMenuCamDelayed ()
    {
        //yield return new WaitForEndOfFrame();
        VRModeSwitch.instance.DisableStereoscopy();
        VRModeSwitch.instance.SwitchToMenuCamera();
        //AttachScoopToMenuCam();
        yield return null;
    }
}
