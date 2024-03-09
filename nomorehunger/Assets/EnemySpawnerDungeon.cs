using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerDungeon : MonoBehaviour
{
    [SerializeField] private GameObject monster0;
    [SerializeField] private GameObject monster1;
    [SerializeField] private GameObject monster2;
    [SerializeField] private GameObject monster3;

    private void Start()
    {
        spawnEnemyOne(monster2);
        spawnEnemyOne(monster2);
        spawnEnemyOne(monster2);
        spawnEnemyOne(monster1);

        spawnEnemyTwo(monster3);
        spawnEnemyTwo(monster3);
        spawnEnemyTwo(monster3);
        spawnEnemyTwo(monster3);
        
        spawnEnemyThree(monster1);
        spawnEnemyThree(monster2);
        spawnEnemyThree(monster3);
        spawnEnemyThree(monster0);
        spawnEnemyThree(monster1);
        spawnEnemyThree(monster2);
        spawnEnemyThree(monster3);
        spawnEnemyThree(monster0);
        spawnEnemyThree(monster1);
        spawnEnemyThree(monster2);
        spawnEnemyThree(monster3);
        spawnEnemyThree(monster0);
    }

    private void spawnEnemyOne(GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-43f, -31f), Random.Range(-6f, -1f), 0), Quaternion.identity);

    }

    private void spawnEnemyTwo(GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(24f, 40f), Random.Range(-14f, -2f), 0), Quaternion.identity);

    }

    private void spawnEnemyThree(GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(13f, 50f), Random.Range(14f, 20f), 0), Quaternion.identity);

    }


}
