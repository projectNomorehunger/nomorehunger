using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public float speed = 0.5f;
    public Transform Player;
    float health, MaxHealth = 10f;

    // Use this for initialization
    void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displacement = Player.position - transform.position;
        displacement = displacement.normalized;
        if (Vector2.Distance(Player.position, transform.position) > 1.0f)
        {
            transform.position += (displacement * speed * Time.deltaTime);

        }

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0){
            Destroy(gameObject);
        }
        Debug.Log(health);

    }     
        
    

}