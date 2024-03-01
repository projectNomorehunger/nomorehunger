using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    /* Component Variable */
    private Rigidbody2D rb;
    private Animator animator;

    /* Movement Variable */
    private float movementX;
    private float movementY;
    private Vector3 scale;
    public float speed;

    /* Combat System Variable */
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    /* Input Variable */
    public bool MenuOpenCloseInput { get; private set; }
    public bool QuestMenuOpenCloseInput { get; private set; }
    public bool GameOverInput { get; private set; }

    private PlayerInput _playerInput;

    private InputAction _menuOpenCloseAction;
    private InputAction _questMenuOpenCloseAction;
    private InputAction _gameOverAction;

    #region UnityWorkFlow
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;

        _playerInput = GetComponent<PlayerInput>();
        _menuOpenCloseAction = _playerInput.actions["MenuOpenClose"];
        _questMenuOpenCloseAction = _playerInput.actions["QuestMenuOpenClose"];
        _gameOverAction = _playerInput.actions["GameOver"];
    }
    private void Start()
    {
        /*animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale;*/

        speed = 6;
    }
    void Update()
    {
        //UI MENU PRESS
        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
        QuestMenuOpenCloseInput = _questMenuOpenCloseAction.WasPressedThisFrame();
        GameOverInput = _gameOverAction.WasPressedThisFrame();

        //ATTACK PRESS
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.W))
        {
            Spawn(SpawnPoint.position);
            //Debug.Log(SpawnPoint.position);
        }*/
        //UPDATE ANIMATION
        if (movementX > 0) transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        else if (movementX < 0) transform.localScale = new Vector3(scale.x, scale.y, scale.z);

    }
    void FixedUpdate()
    {
        if (!QuestUIManager.questLogPanelUIEnabled && !PlayerStats.isDead)
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
            
    }

    #endregion

    #region Input
    public void OnMove(InputValue movementValue)
    {
        if(!PauseMenu.isPause && !PlayerStats.isDead)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
        else
        {
            movementX = 0;
            movementY = 0;  
        }
        
    }

    #endregion

    #region Combat System
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

    public void DeathAnimation()
    {
        animator.SetTrigger("Die");
    }
    public void Spawn(Vector3 position)
    {
        instance.rb.position = position;
        instance.transform.position = position;
        animator.SetTrigger("Alive");
    }

    #endregion
}
