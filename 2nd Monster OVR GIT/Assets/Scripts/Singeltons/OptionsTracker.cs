using UnityEngine;
using System.Collections;

public class OptionsTracker : MonoBehaviour {

    public static OptionsTracker instance;

    public bool cardboardMode = false;

    public bool touchToMove = false;

    // since this is a singelton never unregister the delegates
    private void Awake()
    {
        OptionBroadcaster.onToggleCardboard += RecieveCardboardBool;
        OptionBroadcaster.onToggleTouchMove += RecieveMoveBool;
    }

    void Start()
    {

       if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }

    void RecieveCardboardBool (bool myBool)
    {
        cardboardMode = myBool;
    }

    void RecieveMoveBool (bool myBool)
    {
        touchToMove = myBool;
    }

}

