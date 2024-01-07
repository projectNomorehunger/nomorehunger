using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Unit : MonoBehaviour
{
    public int hitpoints;
    public int maxHitpoints;
    public int damage;

    public void recieveDamage(int dmg)
    {
       this.hitpoints = this.hitpoints - dmg;
       if (this.hitpoints <= 0)
        {
            death();
        }
    }

    abstract public void death();
}
