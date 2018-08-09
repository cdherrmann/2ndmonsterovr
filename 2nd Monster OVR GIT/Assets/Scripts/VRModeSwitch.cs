using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
//using UnityStandardAssets.ImageEffects;


public class VRModeSwitch : MonoBehaviour
{

    public static VRModeSwitch instance;

    [TextArea]
    public string Notes = "This component performs camera between the cardboard, magic window and simple 2D views.";


    [SerializeField]
    public GameObject menuCamera;

    [SerializeField]
    public GameObject gameCamera;

    public MagicWindowInput magWinInput;

    // Use this for initialization
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void EnterGameMode()
    {
        SwitchToGameCamera();
        EnableStereoscopy();
    }

    public void EnterMagicWindowMode()
    {
        DisableStereoscopy();
        magWinInput.enabled = true;
        //Debug.Log("StereoCamActive");
    }

    public void EnableStereoscopy()
    {
        magWinInput.enabled = false;
        StartCoroutine(LoadDevice("cardboard", true));

    }

    public void DisableStereoscopy()
    {
        
        magWinInput.enabled = true;
        StartCoroutine(LoadDevice("", false));
    }

    IEnumerator LoadDevice(string newDevice, bool enable)
    {
        if (XRSettings.loadedDeviceName != newDevice)
        {
            XRSettings.LoadDeviceByName(newDevice);
            yield return null;
            XRSettings.enabled = true;
        }
        yield return null;
        
        //Debug.Log("loaded new Device: " + newDevice);
        //Debug.Log("XRSettubgs.enabled = " + XRSettings.enabled);
    }

    public void SwitchToMenuCamera()
    {
        gameCamera.SetActive(false);
        menuCamera.SetActive(true);
        magWinInput.enabled = false;
    }

    public void SwitchToGameCamera()
    {
        gameCamera.SetActive(true);
        menuCamera.SetActive(false);
    }

}
