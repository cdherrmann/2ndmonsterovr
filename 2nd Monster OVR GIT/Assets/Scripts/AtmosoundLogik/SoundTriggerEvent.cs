using UnityEngine;
using System.Collections;

public class SoundTriggerEvent : MonoBehaviour {

    public delegate void SoundTriggerAction (GameObject triggerSender);
    public static event SoundTriggerAction onTriggerActivation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider other) {
        Debug.Log("Soundtrigger Entered");
        if (onTriggerActivation != null) {
            onTriggerActivation (gameObject);
        }
    }
}
