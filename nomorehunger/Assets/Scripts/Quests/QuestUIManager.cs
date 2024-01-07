using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager uiManager;

    //BOOLS 
    public bool questAvailable = false;
    public bool questRunning = false;
    public bool questPanelActive = false;
    private bool questLogPanelActive = false;

    //PANELS
    public GameObject questPanel;
    public GameObject questLogPanel;

    //QUESTOBJECT
    private QuestObject currentQuestObject;

    //QUESTLISTS
    public List<Quest> availableQuests = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();

    //BUTTONS
    public GameObject qButton;
    public GameObject qLogButton;
    private List<GameObject> qButtons = new List<GameObject>();

    public GameObject acceptButton;
    public GameObject giveUpButton;
    public GameObject completeButton;

    //SPACER
    public Transform qButtonSpacer1; //qButton available
    public Transform qButtonSpacer2; // running qButtons
    public Transform qLogButtonSpacer;//running in qLog

    //QUEST INFOS
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questSummary;

    //QUEST LOG INFOS
    public TextMeshProUGUI questLogTitle;
    public TextMeshProUGUI questLogDescription;
    public TextMeshProUGUI questLogSummary;

    public QButtonScript acceptButtonScript;
    public QButtonScript giveUpButtonScript;
    public QButtonScript completeButtonScript;

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

    private void Awake()
    {
        if (uiManager == null)
        {
            uiManager = this;
        }
        else if(uiManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        HideQuestPanel();
        HideQuestLogPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questLogPanelActive = !questLogPanelActive;
            ShowQuestLogPanel();
            
        }
    }

    // CALLED FROM QUEST OBJECT
    public void CheckQuests(QuestObject questObject)
    {
        currentQuestObject = questObject;
        QuestManager.questManager.QuestRequest(questObject);
        if((questRunning || questAvailable) && !questPanelActive)
        {
            //showquestLogpanel
            ShowQuestPanel();
        }
        else
        {
            Debug.Log("No Quest Available");
        }
    }

    //SHOW PANEL
    public void ShowQuestPanel()
    {
        questPanelActive = true;
        questPanel.SetActive(questPanelActive);
        //FILL IN DATA
        FillQuestButtons();
    }

    public void ShowQuestLogPanel()
    {
        questLogPanel.SetActive(questLogPanelActive);
        if(questLogPanelActive && !questPanelActive)
        {
            foreach(Quest curQuest in QuestManager.questManager.currentQuestList)
            {
                GameObject questButton = Instantiate(qLogButton);
                QLogButtonScript qbutton = questButton.GetComponent<QLogButtonScript>();

                qbutton.questID = curQuest.id;
                qbutton.questTitle.text = curQuest.title;

                questButton.transform.SetParent(qLogButtonSpacer, false);
                qButtons.Add(questButton);
            }
        }
        else if(!questLogPanelActive && !questPanelActive)
        {
            HideQuestLogPanel();
        }
    }
    public void ShowQuestLog(Quest activeQuest)
    {
        questLogTitle.text = activeQuest.title;
        if(activeQuest.progress == Quest.QuestProgress.ACCEPTED)
        {
            questLogDescription.text = activeQuest.hint;
            questLogSummary.text = activeQuest.questObjective + " : " + activeQuest.questObjectiveCount + " / " + activeQuest.questObjectiveRequirement;
        }
        else if(activeQuest.progress == Quest.QuestProgress.COMPLETE)
        {
            questLogDescription.text = activeQuest.congratulation;
            questLogSummary.text = activeQuest.questObjective + " : " + activeQuest.questObjectiveCount + " / " + activeQuest.questObjectiveRequirement;
        }
    }
    // quest Log

    //HIDE QUEST PANEL
    public void HideQuestPanel()
    {
        questPanelActive = false;
        questAvailable = false; 
        questRunning = false;

        //CLEAR TEXT
        questTitle.text = "";
        questDescription.text = "";
        questSummary.text = "";

        //CLEAR LISTS
        availableQuests.Clear();
        activeQuests.Clear();
        //CLEAR BUTTON LIST
        for(int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();
        //HIDE PANEL
        questPanel.SetActive(questPanelActive);

    }
    //HIDE QUEST LOG PANEL
    public void HideQuestLogPanel()
    {
        questLogPanelActive = false;

        questLogTitle.text = "";
        questLogDescription.text = "";
        questLogSummary.text = "";

        // CLEAR BUTTON LIST
        for(int i = 0;i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();
        questLogPanel.SetActive(questLogPanelActive);
    }

    //FILL BUTTONS FOR QUEST PANEL
    void FillQuestButtons()
    {
        foreach (Quest availableQuest in availableQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.questID = availableQuest.id;
            qBScript.questTitle.text = availableQuest.title;

            questButton.transform.SetParent(qButtonSpacer1, false);
            qButtons.Add(questButton);
        }

        foreach (Quest activeQuest in activeQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.questID = activeQuest.id;
            qBScript.questTitle.text = activeQuest.title;

            questButton.transform.SetParent(qButtonSpacer2, false);
            qButtons.Add(questButton);
        }
    }

    //SHOW QUEST ON BUTTON PRESS IN QUEST PANEL
    public void ShowSelectedQuest(int questID)
    {
        for(int i = 0; i < availableQuests.Count; i++)
        {
            if (availableQuests[i].id == questID)
            {
                questTitle.text = availableQuests[i].title;
                if (availableQuests[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    questDescription.text = availableQuests[i].description;
                    questSummary.text = availableQuests[i].questObjective + " : " + availableQuests[i].questObjectiveCount + " / " + availableQuests[i].questObjectiveRequirement;
                }
            }
        }

        for(int i = 0;i < activeQuests.Count; i++)
        {
            if (activeQuests[i].id == questID)
            {
                questTitle.text = activeQuests[i].title;
                if (activeQuests[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    questDescription.text = activeQuests[i].hint;
                    questSummary.text = activeQuests[i].questObjective + " : " + activeQuests[i].questObjectiveCount + " / " + activeQuests[i].questObjectiveRequirement;
                }
                else if(activeQuests[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    questDescription.text = activeQuests[i].congratulation;
                    questSummary.text = activeQuests[i].questObjective + " : " + activeQuests[i].questObjectiveCount + " / " + activeQuests[i].questObjectiveRequirement;
                }
            }
        }
    }
}
