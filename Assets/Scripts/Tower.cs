using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private bool _turelAttack;
    private GameObject _turelHead;
    private float _attackCooldown;
    private Animator _animator;
    private List<BulletHole> _bulletHols;
    public Vector3 _turelFireParticleSize;

    public GameObject TurelHole;
    public GameObject Turel;

    public bool TurelActive = false;
    
    public float TurelTurnSpeed = 10.0F;
    public float TurelAttackCooldown = 0.25f;

    public GameObject TurelBullet;
    public float TurelBulletSpeed = 25.0f;
    public float TurelBulletMaxLifeTime = 1F;
    
    public ParticleSystem TurelFireParticle;


    void Activate()
    {
        TurelActive = true;
    }

    void DiActivate()
    {
        TurelActive = false;
    }

    // Use this for initialization
	void Start ()
	{
	    _turelAttack = false;

        _turelHead = Instantiate(Turel, TurelHole.transform.position, TurelHole.transform.rotation,TurelHole.transform); 
       
        _attackCooldown = 0;

        _animator = GetComponent<Animator>();
        
        _bulletHols = new List<BulletHole>();
        foreach (Transform child in _turelHead.transform)
        {
            BulletHole tempory = child.GetComponent<BulletHole>();
            _bulletHols.Add(tempory);
        }
        _turelFireParticleSize = new Vector3(2f,2f,2f);
        Debug.Log(_bulletHols.Count);
	}

    void TowerAttack(GameObject enemy)
    {
        if (_bulletHols == null) return;
        foreach (var hole in _bulletHols)
        {
            ParticleSystem explosionEffect = Instantiate(TurelFireParticle) as ParticleSystem;
            explosionEffect.transform.position = hole.transform.position;
            explosionEffect.transform.rotation = hole.transform.rotation;
            explosionEffect.transform.localScale = _turelFireParticleSize;
            //play it
            explosionEffect.loop = false;
            explosionEffect.Play();
            Destroy(explosionEffect.gameObject, explosionEffect.duration);

            GameObject bullet = Instantiate(TurelBullet.gameObject, hole.transform.position, hole.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * TurelBulletSpeed;
            Destroy(bullet, TurelBulletMaxLifeTime);
        }
    }

    // Update is called once per frame
	void Update () {
        _animator.SetBool("Active", TurelActive);
        _animator.SetBool("Fire", _turelAttack);

        if (Input.GetKeyDown(KeyCode.N))
        {
            TurelActive = !TurelActive;
            if (!TurelActive)
            {
                _turelAttack = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (TurelActive)
            {
                _turelAttack = !_turelAttack;
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


        Vector3 dir = _turelHead.transform.position - nearestEnemy.transform.position;
        Quaternion targetQuaternion = Quaternion.LookRotation(dir);

        _turelHead.transform.rotation = Quaternion.Slerp(_turelHead.transform.rotation, Quaternion.Euler(-90, targetQuaternion.eulerAngles.y + 90, 0), TurelTurnSpeed * Time.deltaTime);
        
        _attackCooldown -= Time.deltaTime;
        if (_turelAttack && _attackCooldown < 0)
	    {

            TowerAttack(nearestEnemy.gameObject);
            _attackCooldown = TurelAttackCooldown;
	    }
	}
}
