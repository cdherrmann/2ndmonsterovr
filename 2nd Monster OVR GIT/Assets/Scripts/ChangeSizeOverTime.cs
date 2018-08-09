using UnityEngine;
using System.Collections;

public class ChangeSizeOverTime : MonoBehaviour {

    [SerializeField]
    float scaleBefore;

    [SerializeField]
    float scaleAfter;

    [SerializeField]
    float scaleTime;

    // Use this for initialization
    void Start () {
        StopAllCoroutines();
        StartCoroutine("ScaleMe");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDisable ()
    {
        StopAllCoroutines();
    }

    IEnumerator ScaleMe ()
    {
        float timer = 0f;
        float scalar = (scaleAfter - scaleBefore) / scaleTime;

        while (timer <= scaleTime)
        {
            timer += Time.deltaTime;
            transform.localScale = (scalar * timer + scaleBefore) * Vector3.one ;
            yield return null;
        }

        yield return null;
    }
}
