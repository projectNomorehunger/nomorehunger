using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject monsterPrefab;

    private void Start()
    {
        spawnEnemy(monsterPrefab);
        spawnEnemy(monsterPrefab);
        spawnEnemy(monsterPrefab);
        spawnEnemy(monsterPrefab);
        spawnEnemy(monsterPrefab);
    }

    private void spawnEnemy(GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy , new Vector3 (Random.Range(-11f,12f),Random.Range(-3f,10f),0), Quaternion.identity) ;
        //Debug.Log("Enemy Spawn");

    }
}
