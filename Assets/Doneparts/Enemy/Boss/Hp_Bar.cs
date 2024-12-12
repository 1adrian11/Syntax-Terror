using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Bar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int hp){
        slider.maxValue = hp;
        slider.value = hp;
    }

    public void SetHealth (int health){
        slider.value = health;
    }
}
