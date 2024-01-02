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
    //from round1
    private Vector3 scale;
    
    public GameObject Melee;
    bool isAttacking = false;
    float atkDuration = 0.07f;
    float atkTimer = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;

        speed = 5;

        
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
        Debug.Log(movementValue.ToString());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        /* movement part */
        Vector3 movement = new Vector3(movementX, movementY, 0.0f);
        /*transform.Translate(movement.normalized * Time.deltaTime * speed);*/
        rb.MovePosition(transform.position + movement.normalized * Time.deltaTime * speed);

        
        //animation part
        if (movementX != 0 || movementY != 0)
        {
            animator.SetFloat("RunState", 0.5f);
        }
        else
        {
            animator.SetFloat("RunState", 0);
        }
        Debug.Log("Test");
        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack");
        }*/
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
            animator.SetTrigger("Attack");
        }
        

        //make sprite look left/right
        if (movementX > 0) transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        else if (movementX < 0) transform.localScale = new Vector3(scale.x, scale.y, scale.z);

        /*Debug.Log(rb.position);*/

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Debug.Log("Trigger!");*/
    }

    void Onattack()
    {
        if (!isAttacking)
        {
            Melee.SetActive(true);
            isAttacking = true;
        }
    }
    void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttacking= false;
                Melee.SetActive(false);
            }
        }
    }
   
}




