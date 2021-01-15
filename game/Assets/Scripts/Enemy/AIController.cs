using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    GameObject player;
    Animator anim;

    private int hitTaken = 0;
    private bool enemyDown = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {       
        if(enemyDown)
        {
            anim.SetTrigger("Dead");
        }
        else if(DistanceToPlayer() < chaseDistance)
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            anim.SetTrigger("Walk");
            GetComponent<NavMeshAgent>().destination = player.transform.position;
        }
        else
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            anim.SetTrigger("Idle");

        }
    }

    private float DistanceToPlayer()
    {
        
        return Vector3.Distance(player.transform.position, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shot"))
        {
            anim.SetTrigger("Hit");
            hitTaken++;

            if(hitTaken > 3)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                enemyDown = true;
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (enemyDown)
            {
                PlayerStats.HasAdvantage = true;
            }

            // Add this object's tag to destroy later after battle
            GameController.destroyEnemy(this.gameObject.tag);

            // Save position to spawn player
            GameController.setPlayerSpawn(this.gameObject.transform.position);

            // Load scene
            SceneManager.LoadScene("Battle");
        }
    }
}
