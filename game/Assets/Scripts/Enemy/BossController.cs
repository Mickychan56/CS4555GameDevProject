using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    
    GameObject player;
    Animator anim;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetTrigger("Idle");
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("Player"))
        {
            // Add this object's tag to destroy later after battle
            GameController.destroyEnemy(this.gameObject.tag);

            // Save position to spawn player
            GameController.setPlayerSpawn(this.gameObject.transform.position);

            // Load scene
            SceneManager.LoadScene("Boss");
        }
    }
}

