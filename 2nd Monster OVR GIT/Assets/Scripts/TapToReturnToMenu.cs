using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToReturnToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0))
        {
            SceneChanger.instance.LoadSceneByName("00_Options_Screen");
        }
	}
}
