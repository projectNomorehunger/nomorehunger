using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster0 : MonoBehaviour
{
    public int maxHitpoints = 100;
    int hitpoints;

    private void Start()
    {
        hitpoints = maxHitpoints;  
    }

    public void TakeDamage(int dmg)
    {
        hitpoints -= dmg;

        if (hitpoints <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Monster is dead");

        //die anim

        //disable enemy
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        if(QuestManager.questManager.RequestAcceptedQuest(2) == true) //Quest id using 2
        {
            QuestManager.questManager.AddQuestItem("Defeated 1 Monster", 1);
        }
    }
}
