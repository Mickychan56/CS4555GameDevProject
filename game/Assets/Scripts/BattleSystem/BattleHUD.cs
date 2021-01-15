using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;
    public Slider mpSlider;
    public Button Attack;
    public Button Heal;
    public Button Block;
    public Button Run;

    public void SetHUD(string dialogue)
    {
        dialogueText.text = dialogue;

        levelText.text = "Lv. " + PlayerStats.Level;

        hpSlider.maxValue = PlayerStats.Health;
        hpSlider.value = PlayerStats.CurrentHealth;
        hpText.text = "HP  " + PlayerStats.CurrentHealth + "/" + PlayerStats.Health;

        mpSlider.maxValue = PlayerStats.Magic;
        mpSlider.value = PlayerStats.CurrentMagic;
        mpText.text = "MP  " + PlayerStats.CurrentMagic + "/" + PlayerStats.Magic;
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

    public void buttonVisibility(bool state)
    {
        Attack.gameObject.SetActive(state);
        Heal.gameObject.SetActive(state);
        Block.gameObject.SetActive(state);
        Run.gameObject.SetActive(state);
    }
}
