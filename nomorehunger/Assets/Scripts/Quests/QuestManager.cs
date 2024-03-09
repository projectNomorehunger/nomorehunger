using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questManager;

    public List<Quest> questList = new List<Quest>();   //Master Quest List
    public List<Quest> currentQuestList = new List<Quest>(); //Current Quest List

    //private vars for QuestObject

    private void Awake()
    {
        if(questManager == null)
        {
            questManager = this;
        }
        else if(questManager != this) //prevent double questManager
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void QuestRequest(QuestObject NPCQuestObject) //bug
    {
        //AVAILABLE QUEST
        if(NPCQuestObject.availableQuestIDs.Count > 0)
        {
            for(int i = 0; i < questList.Count; i++)
            {
                for (int j = 0; j < NPCQuestObject.availableQuestIDs.Count; j++)
                {
                    if (questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                    {
                        Debug.Log("Quest ID: " + NPCQuestObject.availableQuestIDs[j] + " " + questList[i].progress);
                        //TESTING
                        //AcceptQuest(NPCQuestObject.availableQuestIDs[j]);
                        // quest ui manager
                        QuestUIManager.uiManager.questAvailable = true;
                        QuestUIManager.uiManager.availableQuests.Add(questList[i]);
                    }
                }
            }
        }

        //ACTIVE QUEST
        for(int i = 0;i < currentQuestList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (currentQuestList[i].id == NPCQuestObject.receivableQuestIDs[j] && (currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED || currentQuestList[i].progress == Quest.QuestProgress.COMPLETE))
                {
                    Debug.Log("Quest ID: " + NPCQuestObject.receivableQuestIDs[j] + " is " + currentQuestList[i].progress);

                    //CompleteQuest(NPCQuestObject.receivableQuestIDs[j]);
                    // quest ui manager
                    QuestUIManager.uiManager.questRunning = true;
                    QuestUIManager.uiManager.activeQuests.Add(currentQuestList[i]); //!
                }
            }
        }
        
    }
    

    //ACCEPT QUEST
    public void AcceptQuest(int questID)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if ((questList[i].id == questID) && (questList[i].progress == Quest.QuestProgress.AVAILABLE))
            {
                currentQuestList.Add(questList[i]);
                questList[i].progress = Quest.QuestProgress.ACCEPTED;
                //Debug.Log("current Quest id: " + currentQuestList[0].id);
                //current Quest id is still 2
            } 
        }
    }

    //GIVE UP
    public void GiveUpQuest(int questID)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if ((currentQuestList[i].id == questID) && (currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED))
            {
                currentQuestList[i].progress = Quest.QuestProgress.AVAILABLE;
                currentQuestList[i].questObjectiveCount = 0;
                currentQuestList.Remove(currentQuestList[i]);
            }
        }
    }

    //COMPLETE QUEST
    public void CompleteQuest(int questID)
    {
        for(int i = 0; i < currentQuestList.Count; i++)
        {
            if ((currentQuestList[i].id == questID) && (currentQuestList[i].progress == Quest.QuestProgress.COMPLETE))
            {
                currentQuestList[i].progress = Quest.QuestProgress.DONE;
                UpdateDoneCurrentQuest(currentQuestList[i].id);
                // REWARD
                PlayerStats.instance.ECO += currentQuestList[i].ECOReward;
                PlayerStats.instance.SOC += currentQuestList[i].SOCReward;
                PlayerStats.instance.ENVI += currentQuestList[i].ENVIReward;

                currentQuestList.Remove(currentQuestList[i]);
                //check load scene 4
                CheckECO();
                // CHECK WORLDSTATS METER
                CheckWorldMeter();
                

            }
        }
        //check for chain quest
        CheckChainQuest(questID);
    }
      
    // CHECK CHAIN QUEST
    void CheckChainQuest(int questID)
    {
        int tempID = 0;
        for (int i = 0;i < questList.Count;i++) 
        {
            if (questList[i].id == questID && questList[i].nextQuest > 0)
            {
                tempID = questList[i].nextQuest;
            }
        }

        if (tempID > 0)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].id == tempID && questList[i].progress == Quest.QuestProgress.NOT_AVAILABLE) 
                {
                    questList[i].progress = Quest.QuestProgress.AVAILABLE;
                }
            }
        }
    }

    //ADD ITEMS
    public void AddQuestItem(string questObjective,int itemAmount)
    {
        for(int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].questObjective == questObjective && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED) 
            {
                currentQuestList[i].questObjectiveCount += itemAmount;
            }

            if (currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequirement && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED) 
            {
                currentQuestList[i].progress = Quest.QuestProgress.COMPLETE;
                UpdateCompleteCurrentQuest(currentQuestList[i].id);

            }
        }
    }

    //REMOVE ITEMS

    // BOOLS

    public bool RequestAvailableQuest(int questID)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                return true;
            }
        }
        return false;  
    }

    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                return true;
            }
        }
        return false;
    }

    // BOOLS 2

    public bool CheckAvailableQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for(int j = 0;j < NPCQuestObject.availableQuestIDs.Count;j++)
            {
                if (questList[i].id == NPCQuestObject.availableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckAcceptedQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckCompleteQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //SHOW QUEST LOG
    public void ShowQuestLog(int questID)
    {
        for(int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID) 
            {
                QuestUIManager.uiManager.ShowQuestLog(currentQuestList[i]);
            }
        }
    }

    public void UpdateCompleteCurrentQuest(int questID)
    {
        foreach (Quest quest in questList)
        {
            if (quest.id == questID) {
                quest.progress = Quest.QuestProgress.COMPLETE;
            }
        }
        
    }

    public void UpdateDoneCurrentQuest(int questID)
    {
        foreach (Quest quest in questList)
        {
            if (quest.id == questID)
            {
                quest.progress = Quest.QuestProgress.DONE;
            }
        }

    }

    public void CheckWorldMeter()
    {
        if (PlayerStats.instance.ECO <= 0 || PlayerStats.instance.SOC <= 0 || PlayerStats.instance.ENVI <= 0)
        {
            //GAME OVER
            PlayerStats.isOver = true;
            GameEndMenu.instance.GameEnd();
        }
    }

    public void CheckECO()
    {
        if(PlayerStats.instance.ECO >= 5)
        {
            SceneManager.LoadScene("CS 4");
        }
    }
}
