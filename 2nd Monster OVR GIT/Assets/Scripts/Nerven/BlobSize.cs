using UnityEngine;
using System.Collections;

public class BlobSize : MonoBehaviour {

    public float finalSize = 1f;

    void OnEnable ()
    {
        finalSize = transform.localScale.x;
    }
}
