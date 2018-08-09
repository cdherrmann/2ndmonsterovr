using UnityEngine;
using System.Collections;

public class NervenDisease : MonoBehaviour {

    Animator[] myAnimators;

    [SerializeField]
    float delayTime;

    [SerializeField]
    string triggerName;

	// Use this for initialization
	void Start () {
        myAnimators = GetComponentsInChildren<Animator>();
        StartCoroutine("startInfection");
	}
	
    IEnumerator startInfection()
    {
        yield return new WaitForSeconds(delayTime);
        foreach (Animator anim in myAnimators)
        {
            anim.SetTrigger(triggerName);
        }
    }
}
