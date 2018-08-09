using UnityEngine;
using System.Collections;

public class RollCredits : MonoBehaviour {

    [SerializeField]
    string creditTrigger;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter (Collider other)
    {
        //Animator creditsAnimator = GameObject.Find("CreditsHolder").GetComponent<Animator>();
        CreditsHolder newCreditsholder = (CreditsHolder)FindObjectOfType(typeof(CreditsHolder));
        Animator creditsAnimator = newCreditsholder.GetComponent<Animator>();

        creditsAnimator.SetBool("CreditsRoll", true);
        creditsAnimator.gameObject.GetComponent<RotateIntoView>().rotateToFrontOfView();
    }
}
