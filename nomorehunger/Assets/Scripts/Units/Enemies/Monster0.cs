using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Monster0 : MonoBehaviour
{
    private Animator _animator;
    private int maxHitpoints = 100;
    public int hitpoints;
    public UnityEvent itemDropped;

    //MOVING PART
    public GameObject player;
    public float speed;
    private float distance;

    private Vector3 scale;
    private bool GotHit;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        hitpoints = maxHitpoints;
        scale = transform.localScale;
        GotHit = false;
    }

    public void TakeDamage(int dmg)
    {
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
        Debug.Log("Monster is dead");
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

    private void Update()
    {
        /* MOVING PART */
        distance = Vector2.Distance(transform.transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();


        if (distance < 4 && GotHit == false)
        {
            _animator.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            //ANIMATION LEFT/RIGHT
            if (direction.x < 0) transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            else if (direction.x > 0) transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        else if (distance < 4 && GotHit == true)
        {
            _animator.SetBool("isRunning", false);
            transform.position = Vector2.MoveTowards(this.transform.position, KnockbackPostion(this.transform), speed * Time.deltaTime);
            //ANIMATION LEFT/RIGHT
            if (direction.x < 0) transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            else if (direction.x > 0) transform.localScale = new Vector3(scale.x, scale.y, scale.z);

            GotHit = false;
        }
        else {
            _animator.SetBool("isRunning", false);
        }

    }

    private Vector2 KnockbackPostion(Transform objectTransform) 
    {
        return new Vector2(objectTransform.position.x, objectTransform.position.y); //knockback at same position for now
    }

}
