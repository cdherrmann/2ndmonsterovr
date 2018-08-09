using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVR_StartButton : MonoBehaviour {

    [SerializeField]
    string gotoScene = "01_See";

    public void OnStartButtonPressed()
    {
        
        SceneChanger.instance.LoadSceneByName(gotoScene);
        
    }
}
