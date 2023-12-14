using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero
{
    public int id;
    public int hp;
    public int mp;
    public int gold;
    public string name; 
    public string description;
}
public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Hero player = new Hero();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
