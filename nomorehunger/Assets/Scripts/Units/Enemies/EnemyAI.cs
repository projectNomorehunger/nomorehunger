using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;

    private void Start()
    {
        
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if(distance < 4)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            
        }
    }
}


