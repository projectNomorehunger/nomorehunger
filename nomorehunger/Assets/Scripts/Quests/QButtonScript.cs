using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QButtonScript : MonoBehaviour
{
    public int questID;
    public TextMeshProUGUI questTitle;

    private GameObject acceptButton;
    private GameObject giveUpButton;
    private GameObject completeButton;

    private QButtonScript acceptButtonScript;
    private QButtonScript giveUpButtonScript;
    private QButtonScript completeButtonScript;

    void Start()
    {
        acceptButton = GameObject.Find("QuestCanvas").transform.Find("QuestPanel").transform.Find("QuestDescription").transform.Find("GameObject").transform.Find("AcceptButton").gameObject;
        acceptButtonScript = acceptButton.GetComponent<QButtonScript>();

        giveUpButton = GameObject.Find("QuestCanvas").transform.Find("QuestPanel").transform.Find("QuestDescription").transform.Find("GameObject").transform.Find("GiveUpButton").gameObject;
        giveUpButtonScript = giveUpButton.GetComponent<QButtonScript>();

        completeButton = GameObject.Find("QuestCanvas").transform.Find("QuestPanel").transform.Find("QuestDescription").transform.Find("GameObject").transform.Find("CompleteButton").gameObject;
        completeButtonScript = completeButton.GetComponent<QButtonScript>();

        acceptButton.SetActive(false);
        giveUpButton.SetActive(false); 
        completeButton.SetActive(false);
    }

    //SHOW ALL INFOS
    public void ShowAllInfos()
    {
        QuestUIManager.uiManager.ShowSelectedQuest(questID);
        //ACCEPT BUTTON
        if(QuestManager.questManager.RequestAvailableQuest(questID)) 
        {
            acceptButton.SetActive(true);
            acceptButtonScript.questID = questID;
        }
        else
        {
            acceptButton.SetActive(false);
        }
        //GIVEUP BUTTON
        if (QuestManager.questManager.RequestAcceptedQuest(questID))
        {
            giveUpButton.SetActive(true);
            giveUpButtonScript.questID = questID;
        }
        else
        {
            giveUpButton.SetActive(false);
        }
        //COMPLETE BUTTON
        if (QuestManager.questManager.RequestCompleteQuest(questID))
        {
            completeButton.SetActive(true);
            completeButtonScript.questID = questID;
        }
        else
        {
            completeButton.SetActive(false);
        }
    }

    public void AcceptQuest()
    {
        QuestManager.questManager.AcceptQuest(questID);
        QuestUIManager.uiManager.HideQuestPanel();

        //UPDATE ALL NPCs
        QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
        foreach(QuestObject obj in currentQuestGuys)
        {
            obj.SetQuestMarker();
        }
    }

    public void GiveUpQuest()
    {
        QuestManager.questManager.GiveUpQuest(questID);
        QuestUIManager.uiManager.HideQuestPanel();

        //UPDATE ALL NPCs
        QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
        foreach (QuestObject obj in currentQuestGuys)
        {
            obj.SetQuestMarker();
        }
    }

    public void CompleteQuest()
    {
        QuestManager.questManager.CompleteQuest(questID);
        QuestUIManager.uiManager.HideQuestPanel();

        //UPDATE ALL NPCs
        QuestObject[] currentQuestGuys = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
        foreach (QuestObject obj in currentQuestGuys)
        {
            obj.SetQuestMarker();
        }
    }

    public void ClosePanel()
    {
        QuestUIManager.uiManager.HideQuestPanel();
    }
}
