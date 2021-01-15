using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoHUD : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;
    public Slider staminaSlider;

    public void SetHUD()
    {
        hpText.text = "HP  " + PlayerStats.CurrentHealth + "/" + PlayerStats.Health;
        hpSlider.maxValue = PlayerStats.Health;
        hpSlider.value = PlayerStats.CurrentHealth;

        mpText.text = "MP  " + PlayerStats.CurrentMagic + "/" + PlayerStats.Magic;
        mpSlider.maxValue = PlayerStats.Magic;
        mpSlider.value = PlayerStats.CurrentMagic;

        levelText.text = "Lv. " + PlayerStats.Level;

        expText.text = "Exp. " + PlayerStats.CurrentExp + "/" + PlayerStats.NextLevel;
        expSlider.maxValue = PlayerStats.NextLevel;
        expSlider.value = PlayerStats.CurrentExp;

        staminaSlider.maxValue = PlayerStats.Stamina;
        staminaSlider.value = PlayerStats.CurrentStamina;
    }

    public void setHP()
    {
        hpSlider.value = PlayerStats.CurrentHealth;
        hpText.text = "HP  " + PlayerStats.CurrentHealth + "/" + PlayerStats.Health;
    }
    public void setMP()
    {
        mpSlider.value = PlayerStats.CurrentMagic;
        mpText.text = "MP  " + PlayerStats.CurrentMagic + "/" + PlayerStats.Magic;
    }

    public void setStamina()
    {
        staminaSlider.value = PlayerStats.CurrentStamina;
    }
}
