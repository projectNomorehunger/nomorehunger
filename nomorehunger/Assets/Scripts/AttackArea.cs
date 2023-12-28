using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage=1;

    private void OntriggerEnter2D(Collider2D collider){
        if(collider.GetComponent<Health>()!= null )
        {
            Health.health = colllider.GetComponent<Health>();
            health.Damage(damage);
        }
    }
}
