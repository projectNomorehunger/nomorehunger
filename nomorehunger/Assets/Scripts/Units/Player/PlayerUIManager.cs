using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    public TextMeshProUGUI hitpointsText;

    // Start is called before the first frame update
    void Start()
    {
        
        hitpointsText.text = "HP: " + 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Update method called."); // Add this line for debugging

        if (hitpointsText != null)
        {
            hitpointsText.text = "HP: " + PlayerStats.instance.hitpoints.ToString();
            // Debug.Log("UI text updated."); // Add this line for debugging
        }
    }
}
