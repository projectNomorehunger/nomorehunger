using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsChange : MonoBehaviour
{   
    public bool isBuff;
    [SerializeField] public int dmg;
    private void Start()
    {
        isBuff = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isBuff)
        {
            PlayerStats.instance.BuffDamage(dmg);
            PlayerStats.instance.Heal();
            isBuff = true;
            //Debug.Log("isbuff");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

        }
    }


}
