using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAroundAxis : MonoBehaviour {

    [SerializeField]
    Vector3 rotationVector;

	// Use this for initialization
	void Start () {
        StartCoroutine(RotateObject());
	}

    private IEnumerator RotateObject()
    {
        while (true)
        {
            transform.Rotate(rotationVector * Time.deltaTime);
            yield return null;
        }
    }
}
