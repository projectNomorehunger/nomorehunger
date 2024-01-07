using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public int maxHitpoints;
    public int hitpoints;
    public int damage;
    public int economy;
    public int environment;
    public int social;
    public int gold;
    public string[] items;

    public void recieveDamage(int dmg)
    {
        this.hitpoints = this.hitpoints - dmg;
        if (this.hitpoints <= 0)
        {
            death();
        }
    }

    public void death()
    {
        //enter death state
        Debug.Log("dead");
    }

    private void Start()
    {
        maxHitpoints = 100;
        hitpoints = maxHitpoints;
        damage = 30;
    }
}
