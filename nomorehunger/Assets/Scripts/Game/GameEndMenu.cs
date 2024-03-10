using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndMenu : MonoBehaviour
{
    public static GameEndMenu instance;
    public GameObject gameEndMenu;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        gameEndMenu.SetActive(false);
    }

    
    void Update()
    {
        if (PlayerController.instance.GameOverInput && PlayerStats.isDead && PlayerStats.isOver)
        {
            //RESPAWN
            Debug.Log("OVER2");
            gameEndMenu.SetActive(false);
            SceneManager.LoadScene("MainMenu");
            PlayerStats.instance.ResetStats();
        }
    }

    public void GameEnd()
    {
        gameEndMenu.SetActive(true);
    }
}
