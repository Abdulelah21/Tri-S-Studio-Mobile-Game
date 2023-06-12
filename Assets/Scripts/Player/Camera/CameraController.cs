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
    public GameObject levelGround;
    public float paddingX;
    public float paddingZ;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        sSpeed = playerspeed.PlayerSpeed();

        // get the bounds of the level ground object
        Bounds levelBounds = levelGround.GetComponent<Renderer>().bounds;

        // set the camera clamp values based on the bounds of the level ground object and the padding values
        minX = levelBounds.min.x + paddingX;
        maxX = levelBounds.max.x - paddingX;
        minZ = levelBounds.min.z + paddingZ;
        maxZ = levelBounds.max.z - paddingZ;
    }

    private void FixedUpdate()
    {
        // calculate the desired position of the camera
        Vector3 dPos = cameraTarget.position + dist;

        // clamp the desired position to the desired range
        dPos.x = Mathf.Clamp(dPos.x, minX, maxX);
        dPos.z = Mathf.Clamp(dPos.z, minZ, maxZ);

        // interpolate the camera position towards the desired position
        Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);

        // set the camera position and rotation
        transform.position = sPos;
        transform.LookAt(lookTarget.position);
    }

    void LateUpdate()
    {
        // Set camera rotation to initial rotation
        mainCamera.transform.rotation = Quaternion.Euler(52.21f, 0.0f, 0.0f);
    }
}