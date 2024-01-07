using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Monster0 : MonoBehaviour
{
    private Animator _animator;
    public int maxHitpoints = 100;
    int hitpoints;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        hitpoints = maxHitpoints;  
    }

    public void TakeDamage(int dmg)
    {
        hitpoints -= dmg;
        _animator.SetTrigger("isHurt");
        if (hitpoints <= 0)
        {
            Death();
        }
        /*_animator.SetBool("isHurt", false);*/
    }

    private void Death()
    {
        Debug.Log("Monster is dead");

        //die anim
        _animator.SetBool("isDead",true);
        //disable enemy
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        if(QuestManager.questManager.RequestAcceptedQuest(2) == true) //Quest id using 2
        {
            QuestManager.questManager.AddQuestItem("Defeated 1 Monster", 1);
        }
    }
}
