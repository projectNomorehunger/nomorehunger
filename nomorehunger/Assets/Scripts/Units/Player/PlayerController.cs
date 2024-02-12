using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI ui;

    private Rigidbody2D rb;
    private Animator animator;

    private float movementX;
    private float movementY;
    private Vector3 scale;

    //attackPoint thingy
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;

        speed = 6;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        //MOVEMENT
        Vector3 movement = new Vector3(movementX, movementY, 0.0f);
        rb.MovePosition(transform.position + movement.normalized * Time.deltaTime * speed);


        //ANIMATION
        if (movementX != 0 || movementY != 0)
        {
            animator.SetFloat("RunState", 0.5f);
        }
        else
        {
            animator.SetFloat("RunState", 0);
        }

    }

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        


        //UPDATE ANIMATION
        if (movementX > 0) transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        else if (movementX < 0) transform.localScale = new Vector3(scale.x, scale.y, scale.z);

    }


    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit " + enemy.name);
            enemy.GetComponent<Monster0>().TakeDamage( PlayerStats.instance.damage );
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void HurtAnimation()
    {
        animator.SetTrigger("Hurt");
    }
}
