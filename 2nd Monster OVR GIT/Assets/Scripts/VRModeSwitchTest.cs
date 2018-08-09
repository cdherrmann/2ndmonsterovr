using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;



public class VRModeSwitchTest : MonoBehaviour {

    public GameObject menuCamera;
    public GameObject gameCamera;

    // Use this for initialization
    void Start () {
        gameCamera.SetActive(false);
        menuCamera.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
    }
    
    public void EnterGameMode()
    {
        SwitchToGameCamera();
        EnableStereoscopy();
    }

    public void EnterMagicWindowMode()
    {
        DisableStereoscopy();
        Debug.Log("StereoCamActive");
    }

    public void EnableStereoscopy()
    {
        StartCoroutine(LoadDevice("cardboard", true));
    }

    public void DisableStereoscopy()
    {
        StartCoroutine(LoadDevice("", false));
    }

    IEnumerator LoadDevice(string newDevice, bool enable)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = enable;
        Debug.Log(XRSettings.enabled);
    }

    public void SwitchToMenuCamera() {
        gameCamera.SetActive(false);
        menuCamera.SetActive(true);
    }

    public void SwitchToGameCamera()
    {
        gameCamera.SetActive(true);
        menuCamera.SetActive(false);
    }

}
