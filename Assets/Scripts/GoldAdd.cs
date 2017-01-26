using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldAdd : MonoBehaviour
{
    private Player gold;
    public float expPesSecond = 1;


    void OnTriggerExit(Collider collider)
    {
        gold = null;
    }

    void OnTriggerEnter(Collider collider)
    {
        var Player = collider.GetComponent<Player>();
        if (Player != null)
        {
            gold = Player;
        }
    }


    private float Timer = 1;
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            if (gold != null)
            {
                gold.PlayerTakeGold(expPesSecond);
            }
            Timer = 1;
        }

        //Debug.Log("pisec");
    }
}
