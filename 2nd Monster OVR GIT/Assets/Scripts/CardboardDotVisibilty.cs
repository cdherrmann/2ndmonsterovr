using UnityEngine;
using System.Collections;

public class CardboardDotVisibilty : MonoBehaviour {

    [SerializeField] bool dotVisible;

    private GameObject cardboardRecticle;

	// Use this for initialization
	void Start () {
        StartCoroutine(LookForRectacleAndSetVisibility());
	}
	
	IEnumerator LookForRectacleAndSetVisibility ()
    {
        yield return new WaitForEndOfFrame();

        while (cardboardRecticle == null)
        {
            cardboardRecticle = GameObject.Find("GvrReticlePointer");
            yield return null;
        }

        cardboardRecticle.GetComponent<Renderer>().enabled = dotVisible;

        yield return null;

    }
}
