using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public bool TurelActive = false;
    public bool TurelAttack = false;
    public float TurelTurnSpeed = 10.0F;


    public GameObject TurelHole;
    public GameObject Turel;
    private GameObject Head;

    public GameObject bullet_obj;
    public float DelayAttackCooldown = 0.25f;
    private float AttackCooldown = 0.25f;
    public float bulletSpeed = 25.0f;
    public float BulletLifeTime = 1F;

    private Animator current_Animator;

    private int fireAnimHash = Animator.StringToHash("Fire");
    private List<BulletHole> bulletHole;
    public ParticleSystem fireParticle;
    public Vector3 particleSize;

	// Use this for initialization
	void Start ()
	{
        current_Animator = GetComponent<Animator>();

	    GameObject temp = Instantiate(Turel, TurelHole.transform.position, TurelHole.transform.rotation,TurelHole.transform);
	    Head = temp;
        bulletHole = new List<BulletHole>();
        foreach (Transform child in Head.transform)
        {
            BulletHole tempory = child.GetComponent<BulletHole>();
            bulletHole.Add(tempory);
        }
        Debug.Log(bulletHole.Count);
	}

    void TowerAttack(GameObject enemy)
    {
        if (bulletHole == null) return;
        foreach (var hole in bulletHole)
        {
            ParticleSystem explosionEffect = Instantiate(fireParticle) as ParticleSystem;
            explosionEffect.transform.position = hole.transform.position;
            explosionEffect.transform.rotation = hole.transform.rotation;
            explosionEffect.transform.localScale = particleSize;
            //play it
            explosionEffect.loop = false;
            explosionEffect.Play();
            Destroy(explosionEffect.gameObject, explosionEffect.duration);

            GameObject bullet = Instantiate(bullet_obj.gameObject, hole.transform.position, hole.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, BulletLifeTime);
        }
    }

    // Update is called once per frame
	void Update () {
        current_Animator.SetBool("Active", TurelActive);
        current_Animator.SetBool("Fire", TurelAttack);

        if (Input.GetKeyDown(KeyCode.N))
        {
            TurelActive = !TurelActive;
            if (!TurelActive)
            {
                TurelAttack = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (TurelActive)
            {
                TurelAttack = !TurelAttack;
            }
        }


        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>() ;

        Enemy nearestEnemy = null;
        float dist = Mathf.Infinity;

        foreach (Enemy e in enemies)
        {
            float d = Vector3.Distance(this.transform.position, e.transform.position);
            if (nearestEnemy == null || d < dist)
            {
                nearestEnemy = e;
                dist = d;
            }
        }

        if (nearestEnemy == null)
        {
            return;
        }


        Vector3 dir = Head.transform.position - nearestEnemy.transform.position;
        Quaternion targetQuaternion = Quaternion.LookRotation(dir);

        Head.transform.rotation = Quaternion.Slerp(Head.transform.rotation, Quaternion.Euler(-90, targetQuaternion.eulerAngles.y+90, 0), TurelTurnSpeed * Time.deltaTime);
        
        AttackCooldown -= Time.deltaTime;
	    if (TurelAttack && AttackCooldown < 0)
	    {

            TowerAttack(nearestEnemy.gameObject);
	        AttackCooldown = DelayAttackCooldown;
	    }
	}
}
