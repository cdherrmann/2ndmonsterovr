using UnityEngine;
using System.Collections;

public class RotateIntoView : MonoBehaviour {

    private GameObject cardboardHead;

	// Use this for initialization
	void Start () {
        //cardboardHead = GameObject.Find("Head");
	}
	
    public void rotateToFrontOfView ()
    {
        //Vector3 headDirection = cardboardHead.transform.forward;
        Vector3 headDirection = GvrVRHelpers.GetHeadForward();
        Vector3 newDirection = new Vector3(headDirection.x, 0, headDirection.z).normalized;
        transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
    }
}
