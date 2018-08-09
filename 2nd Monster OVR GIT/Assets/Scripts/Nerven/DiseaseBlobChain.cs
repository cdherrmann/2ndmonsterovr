using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DiseaseBlobChain : MonoBehaviour {

    // Nach einem Delay alle Childblobs im einem Zeitabstand anwachsen lassen.

    [SerializeField]
    float delayTime = 20f;

    [SerializeField]
    float chainBeatTime = 0.5f;

    [SerializeField]
    float growDuration = 5f;

    private List<Transform> blobTransforms = new List<Transform>();
        
    private int blobCounter = 0;

	// Use this for initialization
	void Start () {
        CollectChildTransforms();
        foreach (Transform myTransform in blobTransforms)
        {
            myTransform.localScale = Vector3.zero;
        }
        StartCoroutine("startChainAfterDelay");
    }

    private void CollectChildTransforms()
    {
        foreach (Transform T in transform)
        {
            blobTransforms.Add(T);
        }
    }

    IEnumerator startChainAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        if (blobTransforms[blobCounter] != null)
        {
            StartCoroutine("growBlob", blobTransforms[blobCounter]);
        }
    }

    IEnumerator growBlob (Transform myTransform)
    {
        StartCoroutine("waitUntilNextBeat");
        
        float timer = 0f;
        float size = myTransform.gameObject.GetComponent<BlobSize>().finalSize;
        float percentage = 0f;

        while (timer <= growDuration)
        {
            timer += Time.deltaTime;
            percentage = timer / growDuration;
            myTransform.localScale = percentage * size * Vector3.one;
            yield return null;
        }

        
    }

    IEnumerator waitUntilNextBeat ()
    {
        yield return new WaitForSeconds(chainBeatTime);
        if (blobTransforms[blobCounter] != null)
        {
            StartCoroutine("growBlob", blobTransforms[blobCounter]);
            blobCounter += 1;
        }
        else yield return null;
    }

}
