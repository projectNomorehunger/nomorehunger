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
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        

    }

    
}


