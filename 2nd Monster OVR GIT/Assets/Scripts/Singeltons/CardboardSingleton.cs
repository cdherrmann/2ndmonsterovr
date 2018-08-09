using UnityEngine;
using System.Collections;

public class CardboardSingleton : MonoBehaviour {

    static CardboardSingleton instance;

    // Use this for initialization
    void Start()
    {
        if (instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
