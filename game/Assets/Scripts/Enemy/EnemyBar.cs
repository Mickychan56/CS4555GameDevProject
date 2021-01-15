using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBar : MonoBehaviour
{
    public Slider hpSlider;

    public void setEnemy(Unit unit)
    {
        hpSlider.maxValue = unit.HP;
        hpSlider.value = unit.currentHP;
    }

    public void setHP(int currentHP)
    {
        hpSlider.value = currentHP;
    }
}
