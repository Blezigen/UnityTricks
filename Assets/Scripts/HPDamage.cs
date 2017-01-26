using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDamage : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        var Player = collider.GetComponent<Player>();
        if (Player != null)
        {
            Player.PlayerTakeDamage(10);
            Debug.Log("damage");
        }
    }

    void Update()
    {
        //Debug.Log("pisec");
    }

}
