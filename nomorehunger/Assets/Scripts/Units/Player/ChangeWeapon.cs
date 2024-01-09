using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("R_Weapon_Sword").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("R_Weapon_Bow").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("R_Weapon_Axe").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("R_Weapon_Wand").GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        equipWeapon();    
    }

    void equipWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject.Find("R_Weapon_Sword").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("R_Weapon_Bow").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("R_Weapon_Axe").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("R_Weapon_Wand").GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject.Find("R_Weapon_Sword").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("R_Weapon_Bow").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("R_Weapon_Axe").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("R_Weapon_Wand").GetComponent<SpriteRenderer>().enabled = false;
        }  
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject.Find("R_Weapon_Sword").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("R_Weapon_Bow").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("R_Weapon_Axe").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("R_Weapon_Wand").GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameObject.Find("R_Weapon_Sword").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("R_Weapon_Bow").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("R_Weapon_Axe").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("R_Weapon_Wand").GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {

        }
        
    }
}
