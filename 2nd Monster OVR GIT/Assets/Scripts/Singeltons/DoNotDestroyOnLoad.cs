using UnityEngine;
using System.Collections;

public class DoNotDestroyOnLoad : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
