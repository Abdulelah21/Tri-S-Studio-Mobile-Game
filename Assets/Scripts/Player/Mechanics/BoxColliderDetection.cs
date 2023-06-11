using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BoxColliderDetection : MonoBehaviour
{
    public bool IsEnemyDetected { get; private set; }
    public string enemyTag = "enemy";
    [SerializeField]
    private PlayerAction player;
    public int CurrentNumberOfEnemies;



    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy with the "enemy" tag
        if (other.CompareTag(enemyTag))
        {
            AddEnemy();
            // Add the enemy object to the list
/*            enemyObjects.Add(other.gameObject);
            CurrentNumberOfEnemies = enemyObjects.Count;*/
            Debug.Log("Number Of Enemies  " + CurrentNumberOfEnemies);
            // Set the IsEnemyDetected variable to true
            IsEnemyDetected = true;
            player.GetNumbers(CurrentNumberOfEnemies);
        }

    }

    public void AddEnemy()
    {
        CurrentNumberOfEnemies += 1;
        player.GetNumbers(CurrentNumberOfEnemies);
    }


    public void SubEnemy()
    {
        if (CurrentNumberOfEnemies >= 0)
        {

            CurrentNumberOfEnemies -= 1;
            Debug.Log("Number Of Enemies  " + CurrentNumberOfEnemies);
            player.GetNumbers(CurrentNumberOfEnemies);

        }
       
    }



}