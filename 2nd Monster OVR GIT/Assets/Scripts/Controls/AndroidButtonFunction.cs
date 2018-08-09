using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidButtonFunction : MonoBehaviour
{
    public string backButtonSceneName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || OVRInput.GetUp(OVRInput.Button.Two))
        {
            SceneChanger.instance.LoadSceneByName(backButtonSceneName);
        }
    }
}
