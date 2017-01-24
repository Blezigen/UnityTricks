using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public ParticleSystem collisionEfect;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        Destroy(this);
        Destroy(other.transform.gameObject);
    }

    void OnDestroy()
    {

        ParticleSystem explosionEffect = Instantiate(collisionEfect) as ParticleSystem;

        explosionEffect.transform.position = transform.position;

        //play it
        explosionEffect.loop = false;
        explosionEffect.Play();

        Destroy(explosionEffect.gameObject, explosionEffect.duration);
    }
}
