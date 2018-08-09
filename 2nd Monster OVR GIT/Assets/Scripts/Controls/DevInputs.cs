using UnityEngine;
using System.Collections;

public class DevInputs : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Debug.isDebugBuild)
        {
            ChangeSceneOnKeyInput();
        }
	}

    void ChangeSceneOnKeyInput ()
    {           
       
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SceneChanger.instance.LoadSceneByName("01_See");
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SceneChanger.instance.LoadSceneByName("02_Hand");
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SceneChanger.instance.LoadSceneByName("03_Neurons");
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SceneChanger.instance.LoadSceneByName("04_Brain");
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SceneChanger.instance.LoadSceneByName("05_Particles");
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            SceneChanger.instance.LoadSceneByName("06_Eclipse");
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            SceneChanger.instance.LoadSceneByName("07_Baby");
        }
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            SceneChanger.instance.LoadSceneByName("08_Monster");
        }
        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            SceneChanger.instance.LoadSceneByName("09_Jump");
        }
      


        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            SceneChanger.instance.LoadNextScene();
        }

    }

}
