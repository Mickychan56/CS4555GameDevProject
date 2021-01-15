using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyEncounterPrefab;

    //private bool spawning = true;

    void Start()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /*private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Garbage for now, doesnt do anything
        if(scene.name == "Battle")
        {
            if(this.spawning) 
            {
                Instantiate(enemyEncounterPrefab);
            }
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Add this object's tag to destroy later after battle
            GameController.destroyEnemy(this.gameObject.tag);

            // Save position to spawn player
            GameController.setPlayerSpawn(this.gameObject.transform.position);

            // Load scene
            SceneManager.LoadScene("Battle");
        }
    }
}
