using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Micosmo.SensorToolkit;

public class PlayerLook : MonoBehaviour
{
   
    public PlayerAction player;
    public string enemyTag = "enemy";
    public float maxDistance = 1000f;
    public float maxAngle = 30f;
    public LayerMask enemyLayerMask;
    private void Start()
    {
        player = FindObjectOfType<PlayerAction>();
    }
    private void Update()
    {
        player.IsPlayerLooking = false;

        RaycastHit hit;
        Vector3 direction = transform.forward;
        float angle = Vector3.Angle(transform.forward, direction);

        if (angle <= maxAngle && Physics.Raycast(transform.position, direction, out hit, maxDistance, enemyLayerMask))
        {
            if (hit.collider.CompareTag(enemyTag))
            {
                player.IsPlayerLooking = true;
            }
        }
    }
}