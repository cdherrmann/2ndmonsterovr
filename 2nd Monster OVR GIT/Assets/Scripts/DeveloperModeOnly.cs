using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperModeOnly : MonoBehaviour {

    [TextArea]
    public string hint = "This component destroys its object, when not in developer mode. Developer mode can be turned on or off in the buil menu.";

	// Use this for initialization
	void Awake () {
		if (!Debug.isDebugBuild)
        {
            Destroy(gameObject);
        }
	}
}
