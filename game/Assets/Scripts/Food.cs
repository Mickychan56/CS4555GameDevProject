using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            PlayerStats.CurrentStamina += 30;

            if (PlayerStats.CurrentStamina > PlayerStats.Stamina)
                PlayerStats.CurrentStamina = PlayerStats.Stamina;
        }
    }
}
