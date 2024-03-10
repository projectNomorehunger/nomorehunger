using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    public TextMeshProUGUI ECOScoreText;
    public TextMeshProUGUI ENVIScoreText;
    public TextMeshProUGUI SOCScoreText;

    void Start()
    {
        ECOScoreText.text = "0";
        ENVIScoreText.text = "0";
        SOCScoreText.text = "0";
    }

    void Update()
    {
      
        if (ECOScoreText != null)
        {
            ECOScoreText.text = PlayerStats.instance.ECO.ToString();
        }
        if (ENVIScoreText != null) {
            ENVIScoreText.text = PlayerStats.instance.ENVI.ToString();
        }
        if (SOCScoreText != null)
        {
            SOCScoreText.text = PlayerStats.instance.SOC.ToString();
        }
    }
}
