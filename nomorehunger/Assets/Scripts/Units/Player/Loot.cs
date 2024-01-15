using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public string name;
    public int amount;
    
    public Loot(string name, int amount)
    {
        this.name = name;
        this.amount = amount;
    } 
}
