using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapToRecenter : MonoBehaviour {

    public RecenterCardboardView recenterComponent;

    private void Awake()
    {
        InputManager.OnDoubleClick += RecenterView;
    }

    void RecenterView()
    {
        RecenterCardboardView recenterObject = null;
        recenterObject = FindObjectOfType<RecenterCardboardView>();
        if (recenterObject != null) { 
            recenterObject.Recenter();
        }
    }
}
