using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public AudioManager audioManager;
    //Animator animator;
    public int maxHitpoints;
    public int hitpoints;
    public int damage;
    public int ECO;
    public int ENVI;
    public int SOC;
    public int gold;
    public List<Loot> items;

    public static bool isDead { get; set; }
    public static bool isOver {  get; set; }
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
        isDead = false;
        isOver = false;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
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
        audioManager.PlaySFX(audioManager.hurt);
        hitpoints = hitpoints - dmg;
        if (this.hitpoints <= 0 && isDead == false)
        {
            Death();
        }else if(this.hitpoints <= 0 && isDead == true)
        {

        }
    }

    public void Death()
    {
        Debug.Log("Player is Dead");
        isDead = true;
        PlayerController.instance.DeathAnimation();
        audioManager.PlaySFX(audioManager.death);

        //World Stats Decrease
        ECO -= 1;
        ENVI -= 1;
        SOC -= 1;

        //UI popup
        if (ECO <= 0 || ENVI <= 0 || SOC <= 0)
        {
            //Debug.Log("OVER");
            isOver = true;
            GameEndMenu.instance.GameEnd();

        }
        else 
        {
            DeathMenu.instance.GameOver();
            //Debug.Log("DEAD");
        }
        
    }

    public void Respawn()
    {
        hitpoints = maxHitpoints;
        isDead = false;
    }

    public void GetItemDropped()
    {
        // items.Add(new Loot("Monster Fang", 1));
        QuestManager.questManager.AddQuestItem("find Obj from monster", 1);
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
        if (QuestManager.questManager.RequestAcceptedQuest(5))
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
        if (QuestManager.questManager.RequestAcceptedQuest(5)) // quest 3 is item dropped
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

    public void ResetStats()
    {
        maxHitpoints = 100;
        hitpoints = maxHitpoints;
        damage = 30;
        ECO = 1;
        ENVI = 5;
        SOC = 5;
        gold = 0;
        items = new List<Loot>();
        isDead = false;
        isOver = false;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void BuffDamage(int dmg)
    {
        damage += dmg;
    }

    public void Heal()
    {
        hitpoints = maxHitpoints;
    }

    public void StatsIncrease()
    {
        ECO += 1;
        ENVI += 1;
        SOC += 1;
        QuestManager.questManager.CheckECO();
    }
    #endregion
}
