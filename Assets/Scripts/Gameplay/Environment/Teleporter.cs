using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject NextLevel;
    public Transform teleportTarget; // The transform of the teleport destination
    public GameObject player; // The player object
    public RespawnEnemies spawnEnemy;
    public GameObject EnemyDetector;
    public GameObject PreviousLevel;
    public bool noEnemies = false;
    public Material material;
    public Color newColor;
    private void Start()
    {
        // Get the material component
        material = GetComponent<Renderer>().material;
        newColor = Color.red;
        material.color = newColor;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && noEnemies)
        {
            NextLevel.SetActive(true);


            // Teleport the player to the teleport target
            player.transform.position = teleportTarget.position;
            player.transform.rotation = teleportTarget.rotation;
            spawnEnemy.LoadLevel();
            noEnemies = false;
            material.color = Color.red;
            EnemyDetector.SetActive(true);
            PreviousLevel.SetActive(false);

}
    }

    
}