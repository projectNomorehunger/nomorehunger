using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderC : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            QuestManager.questManager.AddQuestItem("talk to header Port Town", 1);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

        }
    }
}
