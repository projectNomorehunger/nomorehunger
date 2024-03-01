using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public string nextSceneName;
    private Scene scene;

    //SCENE TRANSITION
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //before scene name
            SceneController.beforeSceneName = scene.name;
            SceneManager.LoadScene(nextSceneName);

        }
    }

}
