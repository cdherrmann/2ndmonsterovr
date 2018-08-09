using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {


    void Start()
    {

    }

    public void OnStartButtonPressed()
    {
        if (OptionsTracker.instance.cardboardMode)
        {
            SceneChanger.instance.LoadSceneByName("00_Cardboard_Hub");
        } else
        {
            SceneChanger.instance.LoadSceneByName("01_See");
        }
    }
}
