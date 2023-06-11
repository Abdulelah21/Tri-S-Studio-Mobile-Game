using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
 
    private BoxCollider boundsCollider; // the box collider that represents the camera bounds

    // Start is called before the first frame update
    void Start()
    {
        boundsCollider = GetComponent<BoxCollider>();
        // adjust the box collider size to match the camera's aspect ratio
        Camera mainCamera = Camera.main;
        float height = 2f * mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;
        boundsCollider.size = new Vector3(width, 0f, height);
    }

    // Update is called once per frame
    void Update()
    {
        // adjust the box collider position to match the camera's position
        Camera mainCamera = Camera.main;
        boundsCollider.transform.position = new Vector3(mainCamera.transform.position.x, 0f, mainCamera.transform.position.z);
    }
}