using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
       
            QuestManager.questManager.AddQuestItem("go to mine town", 1);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
          
        }
    }
}
