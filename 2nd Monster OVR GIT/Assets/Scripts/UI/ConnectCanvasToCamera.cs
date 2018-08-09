using UnityEngine;
using System.Collections;

public class ConnectCanvasToCamera : MonoBehaviour {

    [SerializeField]
    GameObject twoDeeCamera;

    Canvas myCanvas;

	// Use this for initialization
	void Start () {
        myCanvas = GetComponent<Canvas>();
        twoDeeCamera = GameObject.Find("GvrCamera");
        myCanvas.worldCamera = twoDeeCamera.GetComponent<Camera>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
