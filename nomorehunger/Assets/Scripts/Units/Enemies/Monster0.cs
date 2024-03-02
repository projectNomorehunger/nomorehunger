using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class Monster0 : MonoBehaviour
{
    public enum EnemyState {IDLE, RUN, HURT, ATTACK, DEATH };
    private Animator _animator;
    private int maxHitpoints = 100;
    public int hitpoints;
    public int damage;
    public UnityEvent itemDropped;
    public EnemyState enemyState;

    //MOVING PART
    public GameObject player;
    public float speed;
    private float distance;

    private Vector3 scale;
    private bool GotHit;

    //attackPoint thingy
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayers;

    public float attackRate = 1f;
    float nextAttackTime = 0f;
    void Start()
    {
        enemyState = EnemyState.IDLE;
        _animator = GetComponent<Animator>();
        hitpoints = maxHitpoints;
        scale = transform.localScale;
        damage = 10;
        GotHit = false;
    }

    public void TakeDamage(int dmg)
    {
        _animator.SetBool("isRunning", false);
        _animator.SetTrigger("isHurt");
        GotHit = true;
        hitpoints -= dmg;
  
        if (hitpoints <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        UnityEngine.Debug.Log("Monster is dead");
        itemDropped.Invoke();

        //die anim
        _animator.SetBool("isDead", true);
        //disable enemy
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        if (QuestManager.questManager.RequestAcceptedQuest(2) == true) //Quest id using 2
        {
            QuestManager.questManager.AddQuestItem("Defeated 1 Monster", 1);
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("HitPlayer");
            //enemy.GetComponent<PlayerStats>().TakeDamage(GetComponent<Monster0>().damage);
            PlayerStats.instance.TakeDamage(damage);
        }
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        switch (enemyState)
        {
            case EnemyState.IDLE:
                if (distance < 8) enemyState = EnemyState.RUN;
                break;

            case EnemyState.RUN:
                //if got hit RUN->HURT
                if (GotHit == true)
                {
                    _animator.SetBool("isRunning", false);
                    enemyState = EnemyState.HURT;
                    break;
                }
                //if out of range RUN->IDLE
                else if (GotHit == false && distance > 4)
                {
                    _animator.SetBool("isRunning", false);
                    enemyState = EnemyState.IDLE;
                    break;
                }
                // RUN -> ATTACK
                else if (distance <= 1.2) //ATTACK RANGE
                {
                    enemyState = EnemyState.ATTACK;
                    break;
                }
                else
                {
                    _animator.SetBool("isRunning", true);
                    Moving();
                    break;
                }
                

            case EnemyState.HURT:
                GotHit = false;
                enemyState = EnemyState.IDLE;
                break;

            case EnemyState.ATTACK:
                if(GotHit == true)
                {
                    enemyState = EnemyState.HURT;
                    break;
                }
                else
                {
                    if (Time.time >= nextAttackTime)
                    {
                        Attack();
                        nextAttackTime = Time.time + 1f / attackRate;
                    }

                    enemyState = EnemyState.IDLE;
                    break;
                }

            default:
                break;
    
        }
       

    }

    private Vector2 KnockbackPostion(Transform objectTransform) 
    {
      return new Vector2(objectTransform.position.x, objectTransform.position.y); //knockback at same position for now
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3f);
    }

    void Moving()
    {
        /* MOVING PART*/
        distance = Vector2.Distance(transform.transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        //Debug.Log(direction);
        /*Debug.Log(distance);*/
        if(GotHit == false) {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        
        //ANIMATION LEFT/RIGHT
        if (direction.x < 0) transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        else if (direction.x > 0) transform.localScale = new Vector3(scale.x, scale.y, scale.z);


    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
