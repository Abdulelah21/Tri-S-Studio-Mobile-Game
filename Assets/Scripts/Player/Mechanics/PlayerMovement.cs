using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 3.0f;
    public Joystick joystick;

    private Rigidbody rb;
    private float horizontal;
    private float vertical;

    float horizontalSpeed = 0f;
    float verticalSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        // Get input for movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        float joystickHorizontalMove = joystick.Horizontal * moveSpeed;
        float joystickVerticalMove = joystick.Vertical * moveSpeed;


        // Calculate movement direction based on input relative to world axes
        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized;
        moveDirection.Normalize();
        float magnitude = moveDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);

        Vector3 joystickMovement = new Vector3(joystickHorizontalMove, 0, joystickVerticalMove).normalized;
        joystickMovement.Normalize();
        float joystickMagnitude = joystickMovement.magnitude;
        joystickMagnitude = Mathf.Clamp01(joystickMagnitude);

        if (horizontalSpeed >= .2f)
        {
            joystickHorizontalMove = moveSpeed;
        }
        else if (horizontal <= .2f) 
        {
            joystickHorizontalMove = -moveSpeed;
        }
        else
        {
            horizontalSpeed = 0f;
        }

        if (verticalSpeed >= .2f)
        {
            joystickVerticalMove = moveSpeed;
        }
        else if (vertical <= .2f)
        {
            joystickVerticalMove = -moveSpeed;
        }
        else
        {
                verticalSpeed = 0f;
        }

        // Rotate player to face movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
        }

        if (joystickMovement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(joystickMovement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
        }


        // Move player using Rigidbody
        Vector3 movement = moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

    
    }

   
}