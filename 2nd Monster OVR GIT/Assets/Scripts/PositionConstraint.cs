using UnityEngine;
using System.Collections;

public class PositionConstraint : MonoBehaviour {

    [SerializeField]
    public GameObject targetObject;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        CopyPosition();
    }

    void CopyPosition ()
    {
        transform.position = targetObject.transform.position;
        //Debug.Log(targetObject.transform.position);
    }
}
