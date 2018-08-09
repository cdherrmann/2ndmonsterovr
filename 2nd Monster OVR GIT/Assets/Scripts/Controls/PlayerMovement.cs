using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private bool canMove = false;

    public float manualMovingSpeed = 1f;
    public float autoMovingSpeed = 0.2f;
    private float movingSpeed = 1f;

    public GameObject vrHead;

    private bool autoMovement;

    private bool processInput = true;
    private bool moving = false;
    private Vector3 forwardVector = new Vector3(0, 0, 1);
    private Vector3 newDirection = new Vector3(0, 0, 1);

    private Rigidbody myRigidbody;

    private Coroutine moveSpeedSetup = null;

    void OnEnable ()
    {
        ResetPlayerPosition.OnReset += SetPosition;
        DefinePlayerMobility.OnSceneLoad += SetCanMove;
        DefinePlayerMobility.OnBroadcastAutoMoveSpeed += SetAutoMoveSpeed;
        InputManager.OnTouchBegin += StartMoving;
        InputManager.OnTouchEnd += StopMoving;
        AutoMovement.onAutoMoveStart += StartAutoMovement;
        AutoMovement.onAutoMoveStop += StopAutoMovement;
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnDisable ()
    {
        ResetPlayerPosition.OnReset -= SetPosition;
        DefinePlayerMobility.OnSceneLoad -= SetCanMove;
        DefinePlayerMobility.OnBroadcastAutoMoveSpeed -= SetAutoMoveSpeed;
        InputManager.OnTouchBegin -= StartMoving;
        InputManager.OnTouchEnd -= StopMoving;
        AutoMovement.onAutoMoveStart -= StartAutoMovement;
        AutoMovement.onAutoMoveStop -= StopAutoMovement;
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    // Use this for initialization
    void Start () {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        ChooseMovingSpeed();
        autoMovement = !OptionsTracker.instance.touchToMove;
    }

    // Update is called once per frame
    void Update () {
        if (moving && processInput)
        {
            //Debug.Log("Moving forward!!!!");
            MoveForward();
        }
    }

    public void SetInputProcessing (bool myBool)
    {
        processInput = myBool;
    }

    void OnSceneLoad (Scene _scene, LoadSceneMode _mode)
    {
        processInput = true;
        if (moveSpeedSetup == null) { StartCoroutine(ChooseMovingSpeed()); }
        //ChooseMovingSpeed();
    }

    void SetAutoMoveSpeed (float myFloat)
    {
        autoMovingSpeed = myFloat;
        if (moveSpeedSetup == null) { StartCoroutine(ChooseMovingSpeed()); }
        //ChooseMovingSpeed();
    }

    IEnumerator ChooseMovingSpeed ()
    {
        yield return new WaitForEndOfFrame();
        if (OptionsTracker.instance.touchToMove)
        {
            movingSpeed = manualMovingSpeed;
        }
        else
        {
            movingSpeed = autoMovingSpeed;
        }
        autoMovement = !OptionsTracker.instance.touchToMove;

        yield return null;
    }


    void SetCanMove (bool myBool)
    {
        canMove = myBool;
        Rigidbody myRigidBody = gameObject.GetComponent<Rigidbody>();
        myRigidBody.isKinematic = !myBool;
    }

    void SetPosition (Vector3 myPosition)
    {
        transform.position = myPosition;
    }

    void StartMoving()
    {
        if (!autoMovement)
        { 
            moving = true;
        }
    }

    void StopMoving()
    {
        if (!autoMovement)
        {
            moving = false;
        }        
    }

    void StartAutoMovement ()
    {
        if (autoMovement)
        {
            moving = true;
        }
    }

    void StopAutoMovement ()
    {
        if (autoMovement)
        {
            moving = false;
        }
    }

    void MoveForward()
    {
        float speedByDirection = 0;
        forwardVector = vrHead.transform.forward;
        newDirection = Vector3.Normalize(new Vector3(forwardVector.x, 0, forwardVector.z));

        speedByDirection = Mathf.Clamp01(forwardVector.z);

        myRigidbody.AddForce(speedByDirection*movingSpeed*newDirection);
    }

}
