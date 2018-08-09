using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
    private GameObject vrCamera;

    void Start()
    {
        vrCamera = GameObject.Find("Player");
    }

    void Update()
    {
        if (vrCamera != null) { 
            transform.LookAt(vrCamera.transform.position);
        }
    }
}