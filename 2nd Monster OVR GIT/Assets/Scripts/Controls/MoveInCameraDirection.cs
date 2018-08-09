using UnityEngine;
using System.Collections;

public class MoveInCameraDirection : MonoBehaviour {

    public float movingSpeed = 1f;
    public GameObject vrHead;

    private bool moving = false;
    private Vector3 direction = new Vector3(0, 0, 1);
    private float distance = 0f;

    void OnEnable()
    {
        InputManager.OnTouchBegin += StartMoving;
        InputManager.OnTouchEnd += StopMoving;
    }
    
    void OnDisable ()
    {
        InputManager.OnTouchBegin -= StartMoving;
        InputManager.OnTouchEnd -= StopMoving;
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (moving)
        {
            MoveForward();
        }
	}

    void StartMoving()
    {
        moving = true;
    }

    void StopMoving()
    {
        moving = false;
    }

    void MoveForward()
    {
        direction = vrHead.transform.forward;
        distance = movingSpeed * Time.deltaTime;
        transform.position += distance * direction;
    }

}
