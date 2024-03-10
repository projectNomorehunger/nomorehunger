using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QLogButtonScript : MonoBehaviour
{
    public int questID;
    public TextMeshProUGUI questTitle;

    public void ShowAllInfos()
    {
        QuestManager.questManager.ShowQuestLog(questID);
    }

    public void ClosePanel()
    {
        QuestUIManager.uiManager.HideQuestLogPanel();
    }
}


