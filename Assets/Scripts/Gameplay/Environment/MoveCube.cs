using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float speed = 1.0f; // Set the speed of movement in the Inspector
    public float distance = 4.0f; // Set the distance to move in the Inspector

    private float startX; // Store the starting X-position of the cube

    // Start is called before the first frame update
    void Start()
    {
        // Get the starting X-position of the cube
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current position of the cube
        Vector3 pos = transform.position;

        // Calculate the new position of the cube
        float newX = startX + Mathf.PingPong(Time.time * speed, distance * 2) - distance;

        // Set the new position of the cube
        transform.position = new Vector3(newX, pos.y, pos.z);
    }
}