using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionHandlerDevRoom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        QuestManager.questManager.AddQuestItem("Leave Town 1", 1);
    }
}
