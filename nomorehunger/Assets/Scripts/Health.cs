using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 10;
    private int Max_Health = 10;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(this.health);
            Damage(1);
            
            
        }
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("cannot have negative Damage");
        }
        this.health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("cannot have negative Healing");
        }

        bool wouldBeOverMaxHealth = health + amount > Max_Health;
        if (wouldBeOverMaxHealth)
        {
            this.health = Max_Health;
        }
        else
        {
            this.health += amount;
        }

    }
    private void Die()
    {
        Debug.Log("I am Dead");
        Destroy(gameObject);
    }
}