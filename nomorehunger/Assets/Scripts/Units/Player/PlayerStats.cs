using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class PlayerStats : MonoBehaviour
{    
    Animator animator;
    public int maxHitpoints;
    public int hitpoints;
    public int damage;
    public int economy;
    public int environment;
    public int social;
    public int gold;
    public List<Loot> items;

    public void TakeDamage(int dmg)
    {
        animator.SetTrigger("Hurt");

        this.hitpoints = this.hitpoints - dmg;
        if (this.hitpoints <= 0)
        {
            Death();
        }
       
   
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }
    public void Death()
    {
        Debug.Log("Player is Dead");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        maxHitpoints = 100;
        hitpoints = maxHitpoints;
        damage = 10;
        items = new List<Loot>();
        

    }

    public void GetItemDropped()
    {
        // items.Add(new Loot("Monster Fang", 1));
        int index = FindItem("Monster Fang");
        if ( index != -1)
        {
            items[index].amount++;
        }
        else
        {
            items.Add(new Loot("Monster Fang", 1));
        }
        
        
        CheckQuestItems();
        Debug.Log("Items Dropped!");
    }

    private int FindItem(string name)
    {
        for(int i = 0; i < items.Count;i++) {
            if (items[i].name == name)
            {
                return i;
            }
        }
        return -1;
    
    }

    void CheckQuestItems()
    {
        //if questActive;
        if (QuestManager.questManager.RequestAcceptedQuest(3))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == "Monster Fang" && items[i].amount > 0)
                {
                    QuestManager.questManager.AddQuestItem("Get 1 Monster Teeth", 1);
                }
            }
        }


    }

    public void CheckQuestItemsAmount()
    {
        if (QuestManager.questManager.RequestAcceptedQuest(3)) // quest 3 is item dropped
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == "Monster Fang")
                {
                    QuestManager.questManager.AddQuestItem("Get 1 Monster Teeth", items[FindItem("Monster Fang")].amount);
                }
            }
        }
    }


}
