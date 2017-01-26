using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float Health = 100;
    public float Exp = 0;
    public float Gold = 0;

    public float MaxHealth = 100;
    public float MaxExp = 1;

    public GUIScrollBar HealthBar;
    public GUIScrollBar ExpBar;

    public Text GoldBar;

	// Use this for initialization
	void Start ()
	{
	        HealthBar.minValue = 0f;
	        HealthBar.maxValue = MaxHealth;
	    
	}

    public void PlayerTakeDamage(float damage)
    {
        Health -= damage;
    }

    public void PlayerTakeExp(float exp)
    {
        Exp += exp;
    }

    public void PlayerTakeGold(float gold)
    {
        Gold += gold;
    }
    // Update is called once per frame
	void Update ()
	{
	    ExpBar.maxValue = MaxExp;

	    if (Input.GetKeyDown(KeyCode.A))
	    {
	        Vector3 pos = transform.position;
	        transform.position = pos + new Vector3(0.1f,0,0);
	        
	    }

        if (Exp >= MaxExp)
        {
            Exp = 0;
            MaxExp = MaxExp * 2;
        }


	    if (Health > MaxHealth) Health = MaxHealth;
	    if (Health <= 0)
	    {
	        Health = 0;
            Destroy(gameObject);
	    }

        HealthBar.SetValue(Health);
        ExpBar.SetValue(Exp);
	    GoldBar.text = Gold.ToString();
	}
}
