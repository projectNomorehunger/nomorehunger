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
    /*public List<Loot> items;*/

    public void recieveDamage(int dmg)
    {
        this.hitpoints = this.hitpoints - dmg;
        if (this.hitpoints <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("dead");
    }

    private void Start()
    {
        maxHitpoints = 100;
        hitpoints = maxHitpoints;
        damage = 30;
        /*items = new List<Loot>();*/

    }

/*    public void GetItemDropped()
    {
        items.Add(new Loot("Monster Fang",1));
        CheckQuestItems();
        Debug.Log("Items Dropped!");
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
    }*/

}
