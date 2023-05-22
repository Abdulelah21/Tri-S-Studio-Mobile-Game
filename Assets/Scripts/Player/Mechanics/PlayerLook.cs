using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float lookRadius = 10f;
    public string enemyTag = "enemy";
    public float rotationSpeed = 10f;
    float speedThreshold = 0.01f;
    private Transform target;

    void Update()
    {
        // Find the nearest game object with the enemy tag within the look radius
        Collider[] enemies = Physics.OverlapSphere(transform.position, lookRadius);
        float closestDistance = Mathf.Infinity;
        foreach (Collider enemy in enemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = enemy.transform;
                }
            }
        }

        // Face and look at the nearest enemy when the target is not null and the player speed is less than 0.01 (Speed Threshold)
        if (target != null && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && GetComponent<Rigidbody>().velocity.magnitude < speedThreshold)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }
}
