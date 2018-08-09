using UnityEngine;
using System.Collections;

public class DisableMovementOnTrigger : MonoBehaviour {

    PlayerMovement myPlayerMovement;

    //public GameObject PlayerObject;

	// Use this for initialization
	void OnEnable () {
        //if (PlayerObject != null)
        //{
        //    PlayerObject = GameObject.Find("Player");
        //}
        //myPlayerMovement = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
    }
	
	void OnTriggerEnter()
    {
        myPlayerMovement = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        myPlayerMovement.SetInputProcessing(false);
    }
}
