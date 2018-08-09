using UnityEngine;
using System.Collections;

public class TriggerAnimation : MonoBehaviour {

    [SerializeField]
    string triggerName;

    [SerializeField]
    private Animator anim;

	// Use this for initialization
	void Start () {
        //anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetAnimationTrigger (string myStrings)
    {
        if (myStrings != "")
        {
            triggerName = myStrings;
        }
        anim.SetTrigger(triggerName);
    }
}
