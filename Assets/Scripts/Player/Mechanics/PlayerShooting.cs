using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float fireRate = 0.2f;
    public float speedThreshold = 0.1f;
    public Transform shootPoint;
    public GameObject obstaclePrefab;
    public PlayerTouchMovement player;


    private bool isMoving;
    private float lastFireTime;

   

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        isMoving = player.IsPlayerMoving();

        if (!isMoving && Time.time > lastFireTime + fireRate)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("enemy"))
                {
                    Shoot();
                    break;
                }
            }

            lastFireTime = Time.time;
        }
    }
    public bool CheckPlayerIsMoving()
    {
        return isMoving;
    }

    void Shoot()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, shootPoint.position, shootPoint.rotation);
        obstacle.GetComponent<Rigidbody>().velocity = shootPoint.forward * 10f;
    }

   
}


