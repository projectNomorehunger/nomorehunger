using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public static DeathMenu instance;
    public GameObject deathMenu;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        deathMenu.SetActive(false);
    }

    
    void Update()
    {
        if (PlayerController.instance.GameOverInput && PlayerStats.isDead)
        {
            //RESPAWN
            deathMenu.SetActive(false);
            //Debug.Log("DEAD2");
            //PlayerController.instance.Spawn(new Vector3(0, 0, 0));
            PlayerSpawner.instance.Respawn();
            PlayerStats.instance.Respawn();
        }
    }

    public void GameOver()
    {
        deathMenu.SetActive(true);
    }
}
