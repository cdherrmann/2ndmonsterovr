using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusTriggerTest : MonoBehaviour {

    [SerializeField]
    bool triggerPressed;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update () {
        //OVRInput.Update();
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            triggerPressed = true;
            Debug.Log("Trigger Pressed!");
        }

    }
}
