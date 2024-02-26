using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    //Animator animator;
    public int maxHitpoints;
    public int hitpoints;
    public int damage;
    public int ECO;
    public int ENVI;
    public int SOC;
    public int gold;
    public List<Loot> items;

    //public UnityEvent Hurt;

    #region UnityWorkFlow
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this object when loading a new scene
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        maxHitpoints = 100;
        hitpoints = maxHitpoints;
        damage = 30;
        ECO = 1;
        ENVI = 5;
        SOC = 5;
        gold = 0;
        items = new List<Loot>();
        
    }

    private void Update()
    {
        //DEBUG
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }
    #endregion

    public void TakeDamage(int dmg)
    {
        PlayerController.instance.HurtAnimation();

        hitpoints = hitpoints - dmg;
        if (this.hitpoints <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("Player is Dead");
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

    #region Quest System
    void CheckQuestItems()
    {
        //if questActive;
        if (QuestManager.questManager.RequestAcceptedQuest(3))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == "Monster Fang" && items[i].amount >= 0)
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

    #endregion
}
