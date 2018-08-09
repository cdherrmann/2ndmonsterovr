using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static InputManager instance;

    public delegate void TouchAction();
    public static event TouchAction OnTouchBegin;
    public static event TouchAction OnTouchEnd;
    public static event TouchAction OnDoubleClick;
    public static event TouchAction OnTrippleClick;

    private bool checkForTrippleClicks = true;
    private bool trippleClick = false;

    [SerializeField]
    float doubleTapTime = 0.5f;

    private Coroutine trippleClickRoutine;

	void Start () {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update () {
        if (PrimaryInputDown())
        {
            if (OnTouchBegin != null)
            {
                OnTouchBegin();
            }
        }

        if (PrimaryInputUp())
        {
            if (OnTouchEnd != null)
            {
                OnTouchEnd();
            }
            if (checkForTrippleClicks) {
                checkForTrippleClicks = false;
                //Debug.Log("First Click");
                trippleClickRoutine = StartCoroutine("listenForSecondClick");
            }
        }
    }

    IEnumerator listenForSecondClick ()
    {
        yield return null;
        float timer = doubleTapTime;
        bool registeredAClick = false;
        // lausche für die Zeit "timer" nach einem click
        
        while (timer >= 0f)
        {
            timer -= Time.deltaTime;
            
            if (PrimaryInputUp())
            {
                //Debug.Log("Second Click");
                registeredAClick = true;
                break;
            }

            yield return null;
        }

        if (registeredAClick) {
            StartCoroutine("listenForThirdClick");
            if (OnDoubleClick != null)
            {
                OnDoubleClick();
                
            }
        } else
        {
            checkForTrippleClicks = true;
        }
        
    }

    IEnumerator listenForThirdClick()
    {
        yield return null;
        float timer = doubleTapTime;
        bool registeredAClick = false;

        // lausche für die Zeit "timer" nach einem click
        while (timer >= 0f)
        {
            timer -= Time.deltaTime;

            if (PrimaryInputUp())
            {
                //Debug.Log("Third Click");
                registeredAClick = true;
                break;
            }

            yield return null;
        }

        if (registeredAClick)
        {
            if (OnTrippleClick != null)
            {
                OnTrippleClick();
                //Debug.Log("TrippleClick!");
            }
        }
        checkForTrippleClicks = true;
    }

    bool PrimaryInputDown ()
    {
        return Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.One);
    }

    bool PrimaryInputUp ()
    {
        return Input.GetMouseButtonUp(0) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetUp(OVRInput.Button.One);
    }

}
