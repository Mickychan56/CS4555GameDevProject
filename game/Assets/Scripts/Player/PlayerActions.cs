using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerActions
{
    public static bool TakeDamage(int dmg)
    {
        PlayerStats.CurrentHealth -= dmg;

        if (PlayerStats.CurrentHealth <= 0)
        {
            PlayerStats.CurrentHealth = 0;
            return true;
        }
        return false;
    }

    public static bool Heal(int amount)
    {
        if (PlayerStats.CurrentMagic > 20)
        {
            PlayerStats.CurrentHealth += amount;
            PlayerStats.CurrentMagic -= 20;

            if (PlayerStats.CurrentHealth > PlayerStats.Health)
                PlayerStats.CurrentHealth = PlayerStats.Health;

            return true;
        }
        else
            return false;
    }

    public static void getExp(int exp)
    {
        PlayerStats.CurrentExp += exp;

        while (PlayerStats.CurrentExp >= PlayerStats.NextLevel)
        {
            PlayerStats.CurrentExp -= PlayerStats.NextLevel;
            PlayerStats.Level++;

            PlayerStats.Health += 10;
            PlayerStats.CurrentHealth += 10;
            PlayerStats.Magic += 10;
            PlayerStats.CurrentMagic += 10;
            PlayerStats.Power += 3;
        }
    }

    public static void HoldRifle()
    {
        if(!PlayerStats.HasRifle)
        {
            // Add power of rifle once you equip it
            PlayerStats.Power += 5;
        }

        // Change it to true, so player can change to rifle animation
        PlayerStats.HasRifle = true;

        // Gets the Rifle and Player's hand object by tag
        GameObject rifle = GameObject.FindGameObjectWithTag("Rifle");
        GameObject hand = GameObject.FindGameObjectWithTag("TriggerHand");

        // Get the transform of hand and set it as parent of the rifle
        Transform trigger = hand.GetComponent<Transform>();
        rifle.transform.SetParent(trigger);

        // Locate the rifle in the players hand
        rifle.transform.localPosition = new Vector3(0, 0, 0);
        rifle.transform.localEulerAngles = new Vector3(88f, 121f, 125f);
    }

    public static void Reset()
    {
        PlayerStats.CurrentHealth = 100;
        PlayerStats.Health = 100;
        PlayerStats.CurrentMagic = 100;
        PlayerStats.Magic = 100;
        PlayerStats.CurrentStamina = 100;
        PlayerStats.Level = 1;
        PlayerStats.CurrentExp = 0;
        PlayerStats.Power = 5;
        PlayerStats.HasRifle = false;
    }
}
