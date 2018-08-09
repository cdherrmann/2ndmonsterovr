using UnityEngine;
using System.Collections;

public class GoBackToMainMenu : MonoBehaviour {

    [SerializeField]
    string optionsSceneName = "00_Options_Screen";

    [SerializeField]
    string cardboardHubSceneName = "10_Cardboard_Hub_Exit";

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MarkerEventLoadStartMenu()
    {
        if (OptionsTracker.instance.cardboardMode) { 
            SceneChanger.instance.LoadSceneByName("cardboardHubSceneName");
        } else
        {
            SceneChanger.instance.LoadSceneByName(optionsSceneName);
        }
    }
}
