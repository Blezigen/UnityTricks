using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpAdd : MonoBehaviour
{
    private Player exp;
    public float expPesSecond = 1;


    void OnTriggerExit(Collider collider)
    {
        exp = null;
    }

    void OnTriggerEnter(Collider collider)
    {
        var Player = collider.GetComponent<Player>();
        if (Player != null)
        {
            exp = Player;
        }
    }


    private float Timer = 1;
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <0)
        {
            if (exp != null)
            {
                exp.PlayerTakeExp(expPesSecond);
            }
            Timer = 1;
        }

        //Debug.Log("pisec");
    }

}
