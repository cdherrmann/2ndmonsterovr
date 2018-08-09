using UnityEngine;
using System.Collections;

public class RotationConstraint : MonoBehaviour
{

    [SerializeField]
    public GameObject targetRotObject;

    Quaternion newRotation;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CopyRotation();
        
    }

    void CopyRotation()
    {
        //transform.rotation = targetRotObject.transform.rotation;
        //var target = GameObject.Find("center");
        newRotation = Quaternion.Euler(targetRotObject.transform.rotation.x, targetRotObject.transform.rotation.y, targetRotObject.transform.rotation.z);
        transform.rotation = newRotation;
    }
}
