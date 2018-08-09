using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

//using Gvr.Internal;

public class RecenterCardboardView : MonoBehaviour {

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnSceneLoaded (Scene _scene, LoadSceneMode _loadSceneMode)
    {
        //Debug.Log("Checking for Cardboard.");
        // if cardboard isn't active, do nothing
        if ((XRSettings.loadedDeviceName != "cardboard") && (OVRPlugin.productName != "Oculus Go"))
        {
            //Debug.Log("There is none.");
            return;
        }
 
        Recenter();
    }

    public void Recenter()
    {
        #if !UNITY_EDITOR
            GvrCardboardHelpers.Recenter();
        #else
            GvrEditorEmulator emulator = FindObjectOfType<GvrEditorEmulator>();
            if (emulator == null)
            {
                return;
            }
            emulator.Recenter();
        #endif  // !UNITY_EDITOR
    }

}
