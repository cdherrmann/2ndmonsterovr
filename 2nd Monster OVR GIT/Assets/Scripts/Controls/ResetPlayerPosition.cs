using UnityEngine;
using System.Collections;

public class ResetPlayerPosition : MonoBehaviour {

    public delegate void PlayerLoadAction(Vector3 newPosition);
    public static event PlayerLoadAction OnReset;

	// Use this for initialization
	void Start () {
        //if (OnReset != null)
        //{
        //    OnReset(gameObject.transform.position);
        //}
        StartCoroutine(WaitForPlayerThenResetPlayerPosition());
	}
	
    IEnumerator WaitForPlayerThenResetPlayerPosition()
    {
        while (OnReset == null)
        {
            yield return null;
        }
        OnReset(gameObject.transform.position);
        yield return null;
    }


}
