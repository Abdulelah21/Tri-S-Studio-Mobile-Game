using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    public Transform cameraTarget;
    public float sSpeed;
    public Vector3 dist;
    public Transform lookTarget;
    public PlayerTouchMovement playerspeed;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        sSpeed = playerspeed.PlayerSpeed();
    }

    private void FixedUpdate()
    {
        Vector3 dPos = cameraTarget.position + dist;
        Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);
        transform.position = sPos;
        transform.LookAt(lookTarget.position);
    }

    void LateUpdate()
    {
        // Set camera rotation to initial rotation
        mainCamera.transform.rotation = Quaternion.Euler(52.21f, 0.0f, 0.0f);

    }
}
