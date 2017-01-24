using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public PathNode EnemyPath;
    public float EnemySpeed = 10.0f;
    public ParticleSystem collisionEfect;
    public PathNode NextDotEnemyPath;
    public Vector3 EffectSize = new Vector3(3f, 3f, 3f);
	// Use this for initialization
	void Start () {
	    if (EnemyPath == null)
	    {
	        Debug.Log("Enemy start is null");
	    }
	    else
	    {
	        NextDotEnemyPath = EnemyPath;
	    }
       
	}
    void OnDestroy()
    {

        ParticleSystem explosionEffect = Instantiate(collisionEfect) as ParticleSystem;

        explosionEffect.transform.position = transform.position;
        explosionEffect.transform.localScale = EffectSize;
        //play it
        explosionEffect.loop = false;
        explosionEffect.Play();

        Destroy(explosionEffect.gameObject, explosionEffect.duration);
    }
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.B))
        {
            Destroy(this.gameObject);
            return;
        }
        Vector3 dir = NextDotEnemyPath.transform.position - this.transform.localPosition;

        float distThisFrame = EnemySpeed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            // We reached the node
            NextDotEnemyPath = NextDotEnemyPath.GetNextNode();
        }
        else
        {
            // TODO: Consider ways to smooth this motion.

            // Move towards node
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
        }
	}
}
