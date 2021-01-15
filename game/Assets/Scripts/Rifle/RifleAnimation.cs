using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAnimation : MonoBehaviour
{
    void Update()
    {
        if (!PlayerStats.HasRifle)
        {
            transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerActions.HoldRifle();
        }
    }
}
