using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField]
    int totalPipes = 0;
    [SerializeField]
    int correctedPipes = 0;

    public GameObject WinText;

    // Start is called before the first frame update
    void Start()
    {
        WinText.SetActive(false);
        totalPipes = PipesHolder.transform.childCount;
        
        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes += 1;
        Debug.Log("correct Move");

        if (correctedPipes == totalPipes)
        {
            Debug.Log("You win!");
            WinText.SetActive(true);
            
            QuestManager.questManager.AddQuestItem("Puzzle Solved", 1);
            //SceneManager.LoadScene("Map2");
            SceneControlPuzzle.instance.FinishedPuzzle();
        }
    }

    public void wrongMove()
    {
        Debug.Log("wrong Move");
    }

    public void CorrectToWrongMove()
    {
        correctedPipes -= 1;
        Debug.Log("CorrectToWrong Move");
    }
}
