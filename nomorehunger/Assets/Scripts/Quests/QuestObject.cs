using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Events;
public class QuestObject : MonoBehaviour
{
    public static QuestObject instance;
    [SerializeField]
    private bool inTrigger = false;

    public List<int> availableQuestIDs = new List<int>();
    public List<int> receivableQuestIDs = new List<int>();

    public GameObject questMarker;
    public GameObject questionMark;
    public GameObject exclaimationMark;
    public Image questAvailableSprite;
    public Image questReceivableSprite;

    public UnityEvent questAccepted;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetQuestMarker();
    }

    public void SetQuestMarker()
    {
        if (QuestManager.questManager.CheckCompleteQuests(this))
        {
            questMarker.SetActive(true);
            questionMark.SetActive(true);
            exclaimationMark.SetActive(false);
            questReceivableSprite.color = Color.yellow;   
        }
        else if(QuestManager.questManager.CheckAvailableQuests(this))
        {
            questMarker.SetActive(true);
            questionMark.SetActive(false);
            exclaimationMark.SetActive(true);
            questAvailableSprite.color = Color.yellow;
        }
        else if (QuestManager.questManager.CheckAcceptedQuests(this))
        {
            questMarker.SetActive(true);
            questionMark.SetActive(true);
            exclaimationMark.SetActive(false);
            questReceivableSprite.color = Color.gray;
        }
        else
        {
            questMarker.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (inTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            if(!QuestUIManager.uiManager.questPanelActive)
            {
                //quest ui manager
                QuestUIManager.uiManager.CheckQuests(this);
                //QuestManager.questManager.QuestRequest(this);
                questAccepted.Invoke();
            }

        }*/
    }

    private void LateUpdate()
    {
        SetQuestMarker();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = false;
        }
    }

    public void OpenQuestPanel()
    {
        if (!QuestUIManager.uiManager.questPanelActive)
        {
            //quest ui manager
            QuestUIManager.uiManager.CheckQuests(this);
            //QuestManager.questManager.QuestRequest(this); 
            questAccepted.Invoke();
        }
    }
}
