using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CardboardModeSceneFork : MonoBehaviour {

    OptionsTracker myOptionsTracker;

	// Use this for initialization
	void Start () {
	    
        if (OptionsTracker.instance.cardboardMode)
        {
            SceneManager.LoadScene("Cardboard_Zwischenmenu");
        } else {
            SceneManager.LoadScene("01_See");
        }
        
	}
	
}
