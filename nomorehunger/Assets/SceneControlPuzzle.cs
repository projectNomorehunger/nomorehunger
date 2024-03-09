using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlPuzzle : MonoBehaviour
{
    public static SceneControlPuzzle instance;
    public string nextSceneName;
    private Scene scene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    //SCENE TRANSITION
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void FinishedPuzzle()
    {
        SceneController.beforeSceneName = scene.name;
        SceneManager.LoadScene(nextSceneName);
    }
}
