using UnityEngine;
using System.Collections;

public class DeactivatePerMessage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.active = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DeactivateMe()
    {
        gameObject.active = false;
    }
}
