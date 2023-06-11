using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemies : MonoBehaviour
{

    public float delay = 4.0f;
    public GameObject[] enemies;
    public BoxColliderDetection boxColliderE;
    public void LoadLevel()
    {
        Invoke("LoadEnemies", delay);

    }


    private void LoadEnemies()
    {
        Debug.Log($"Found {enemies.Length} enemies");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }
}
