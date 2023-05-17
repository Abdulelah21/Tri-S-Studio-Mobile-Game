using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform obstacleSpawn;
    public float fireRate = 0.5f;
    public float speedThreshold = 0.01f;

    private float nextFire = 0.0f;

    void Update()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && GetComponent<Rigidbody>().velocity.magnitude < speedThreshold)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, obstacleSpawn.position, obstacleSpawn.rotation);
        Rigidbody obstacleRb = obstacle.GetComponent<Rigidbody>();
        obstacleRb.velocity = transform.forward * 10f;
    }
}
