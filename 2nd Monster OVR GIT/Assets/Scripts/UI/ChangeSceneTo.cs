using UnityEngine;
using System.Collections;

public class ChangeSceneTo : MonoBehaviour {

    // Connect to a slider to load given scene, when the bar is full

    [SerializeField]
    private string sceneName = "01_See";                         // Scene to Jump to, when Bar is full


    //public delegate void ChangeSceneAfterInput(int sceneNr);
    //public static event ChangeSceneAfterInput OnSceneChangeInput;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void ReactToGuiElement()
    {   
        InvokeSceneChange (sceneName);
    }

    void InvokeSceneChange (string myString) {
        SceneChanger.instance.LoadSceneByName(myString);
    }
}
