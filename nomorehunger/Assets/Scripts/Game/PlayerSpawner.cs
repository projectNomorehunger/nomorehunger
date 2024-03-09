using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner instance;
    [SerializeField] GameObject spawnPoint;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this object when loading a new scene
        }
        else
        {
            // If an instance already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }

    public void SpawnPlayer(Vector3 spawnPosition)
    {
        PlayerController.instance.Spawn(spawnPosition);
    }

    public void OnWarpMap(string from, string to) { 
        if (from == "Map1" && to == "Map2")
        {

        }else if (from == "Map2" && to == "Map1")
        {
            SpawnPlayer(new Vector3 (18,32,0));
        }
        else if(from == "Map3" && to == "Map2")
        {
            SpawnPlayer(new Vector3(39, 7, 0));
        }
        else if(from == "Arena" && to == "Map2")
        {
            SpawnPlayer(new Vector3(13, 51, 0));
        }
        else if(from == "Puzzle Scene" && to == "Map2")
        {
            SpawnPlayer(new Vector3(-50, 16, 0));
        }
        else
        {

        }
    }

    public void Respawn()
    {
        if(spawnPoint == null)
        {
            SpawnPlayer(new Vector3(0, 0, 0));
        }
        else
        {
            SpawnPlayer(spawnPoint.transform.position);
        }
        
    }
   
}