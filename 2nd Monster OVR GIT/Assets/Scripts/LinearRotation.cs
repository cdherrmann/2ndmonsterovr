using UnityEngine;
using System.Collections;

public class LinearRotation : MonoBehaviour {

    [SerializeField]
    bool rotating = true;

    [SerializeField]
    Vector3 rotationVector;

    [SerializeField]
    float anglePerSeconds;

	// Use this for initialization
	void Start () {
        StartCoroutine("MyRotation");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDisable ()
    {
        StopAllCoroutines();
    }

    IEnumerator MyRotation ()
    {
        float deltaRotation;
        while (rotating)
        {
            deltaRotation = Time.deltaTime * anglePerSeconds;
            transform.Rotate(deltaRotation * rotationVector);
            yield return null;
        }
    }
}
