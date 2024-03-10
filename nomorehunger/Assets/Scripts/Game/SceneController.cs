using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static string beforeSceneName { get; set; }
    public string currentSceneName;

    private void Start()
    {
        beforeSceneName = "";
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        //Debug.Log("CurrentSceneName = " + currentSceneName);
        //Debug.Log("BeforeSceneName = " + beforeSceneName);

        PlayerSpawner.instance.OnWarpMap(beforeSceneName,currentSceneName);

    }
}
