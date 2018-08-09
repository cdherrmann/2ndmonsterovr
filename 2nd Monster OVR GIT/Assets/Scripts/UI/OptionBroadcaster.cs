using UnityEngine;
using System.Collections;

public class OptionBroadcaster : MonoBehaviour {

    [SerializeField]
    private bool cardboardMode = false;

    [SerializeField]
    private bool touchToMove = false;

    public delegate void OptionBroadcast(bool myBool);
    public static event OptionBroadcast onToggleCardboard;
    public static event OptionBroadcast onToggleTouchMove;



    void sendToggleCardboard(bool myBool)
    {
        if (onToggleCardboard != null)
        {
            onToggleCardboard(myBool);
        }
    }

    void sendToggleTouchMove(bool myBool)
    {
        if (onToggleTouchMove != null)
        {
            onToggleTouchMove(myBool);
        }
    }

    public void switchCardboardMode(OptionBroadcaster script)
    {
        script.cardboardMode = !script.cardboardMode;
        sendToggleCardboard(script.cardboardMode);
    }

    public void switchTouchToMove(OptionBroadcaster script)
    {
        script.touchToMove = !script.touchToMove;
        sendToggleTouchMove(script.touchToMove);
    }

}
