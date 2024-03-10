using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPause;

    [SerializeField] private GameObject _resumeFirst;
    [SerializeField] private GameObject _mainMenuFirst;
    [SerializeField] private GameObject _quitFirst;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.MenuOpenCloseInput && QuestUIManager.questLogPanelUIEnabled == false) 
        {
            if (isPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    #region Pause/Unpause Functions
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;

        EventSystem.current.SetSelectedGameObject(_resumeFirst);

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;

    }

    #endregion

    #region Canvas Activations
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        PlayerStats.instance.ResetStats();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

    #region Main Menu Button Actions

    public void OnResumePress()
    {
        ResumeGame();
    }

    public void OnMainMenuPress()
    {
        GoToMainMenu();
    }

    public void OnQuitPress()
    {
        QuitGame();
    }
    #endregion
}
