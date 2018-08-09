using UnityEngine;
using System.Collections;

public class PlayOnTriggerEvent : MonoBehaviour {

    [SerializeField] 
    string soundTriggerName;
    [SerializeField]
    bool soundFading = false;

    void OnEnable () {
        SoundTriggerEvent.onTriggerActivation += CheckTriggerObject;
    }

    void OnDisable () {
        SoundTriggerEvent.onTriggerActivation -= CheckTriggerObject;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
    }

    void CheckTriggerObject (GameObject triggerObject) {
        if (triggerObject.name == soundTriggerName) {
            SendStartPlayback();
        }
    }

    void SendStartPlayback()
    {   
        if (soundFading) {
            SendMessage ("FadeInPlayback");
        } else {
            //SendMessage("StartPlayback");
            SendMessage("RestartPlayback");
        }
    }
}
