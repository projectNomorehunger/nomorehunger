using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;
    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    public string quest;

    private bool talking = false;   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerIsClose && talking == false && !QuestUIManager.uiManager.questPanelActive) //Start Talking
        {
            /*if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
                
            }*/
            StartTalking();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && playerIsClose && talking == true) //Talking
        {
            NextLine();
        }
           
        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true); 
        }

    }


    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false) ;
        if (index < dialogue.Length - 1) //next index
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
            talking = false; 
            /* Open Quest Panel */
            QuestObject.instance.OpenQuestPanel();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }

    private void StartTalking()
    {
        if (dialoguePanel.activeInHierarchy)
        {
            zeroText();
        }
        else
        {
            talking = true;
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }
}
