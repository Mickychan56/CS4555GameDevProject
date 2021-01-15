using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            PlayerStats.CurrentHealth += 20;

            if (PlayerStats.CurrentHealth > PlayerStats.Health)
                PlayerStats.CurrentHealth = PlayerStats.Health;
        }
    }
}
