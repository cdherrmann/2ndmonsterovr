using UnityEngine;
using System.Collections;

public class WakeUpParticles : MonoBehaviour {

    [SerializeField]
    private ParticleSystem myParticles;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayParticlesNow()
    {
        myParticles.Play();
    }
}
