using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public float hp;
    private Slider slider;
    public void OnSliderValueChanged(float value)
    {
        
    }

    private void Start()
    {
        hp = PlayerStats.instance.hitpoints;
        slider = GetComponent<Slider>();
        slider.value = hp;
    }

    private void Update()
    {
        hp = PlayerStats.instance.hitpoints;
        UpdateHealthbar();
    }

    public void UpdateHealthbar()
    {
        slider.value = hp;
    }
}
