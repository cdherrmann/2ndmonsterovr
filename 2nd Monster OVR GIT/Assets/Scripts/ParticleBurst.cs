using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurst : MonoBehaviour {

    public ParticleSystem particleEmitter;

    public int emitCount;

	// Use this for initialization
	void Start () {
        particleEmitter = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	    //if (Input.GetMouseButtonUp(0))
     //   {
     //       Debug.Log("Particle Burst!");
     //       EmitParticles(50);
     //   }	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Particles sprayed!");
        Vector3 newPosition = new Vector3();
        newPosition = GameObject.Find("Player").transform.position;
        transform.position = newPosition;
        EmitParticles(emitCount);
    }

    void EmitParticles(int _count)
    {
        particleEmitter.Clear();
        particleEmitter.Emit(_count);
    }
}
