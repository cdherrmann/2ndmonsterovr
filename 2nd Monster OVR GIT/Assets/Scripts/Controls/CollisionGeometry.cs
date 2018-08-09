using UnityEngine;
using System.Collections;

public class CollisionGeometry : MonoBehaviour {

    public PhysicMaterial colliderMaterial;

    // Use this for initialization

    void Start()
    {
        foreach (Transform child in transform)
        {
            MeshRenderer myRenderer = child.GetComponent<MeshRenderer>();
            myRenderer.enabled = false;
            MeshCollider childMeshCollider = child.gameObject.AddComponent<MeshCollider>();
            childMeshCollider.material = colliderMaterial;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
