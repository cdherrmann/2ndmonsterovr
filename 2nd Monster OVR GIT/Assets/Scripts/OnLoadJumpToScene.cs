using UnityEngine;
using System.Collections;

public class OnLoadJumpToScene : MonoBehaviour {

    [SerializeField]
    string sceneName = "SceneName";


    void Start () {
        SceneChanger.instance.LoadSceneByName(sceneName);     
    }
}
