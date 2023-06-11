using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 JoystickSize = new Vector2(300, 300);

    [SerializeField]
    private RectTransform Knob;

    [SerializeField]
     private MyFloatingJoystick Joystick;

/*    [SerializeField]
    private NavMeshAgent Player;*/

    private Finger MovementFinger;
    private Vector2 MovementAmount;

    public float lookRadius = 500f;
    public string enemyTag = "enemy";
    public float rotationSpeed = 10f;
    float speedThreshold = 0.01f;
    private Transform target;

    public bool IsMoving = false;
    public float playerSpeed;
    private PlayerTouchMovement player;

    private void Start()
    {
        player = GetComponent<PlayerTouchMovement>();
        // Calculate scaling factor based on screen width
        float screenWidth = Screen.width;
        float scalingFactor = screenWidth / 1080f; // assuming 1080p resolution as base
                                                   // Apply scaling factor to joystick size
        JoystickSize *= scalingFactor;
        Knob.localScale *= scalingFactor;
        Knob.anchoredPosition *= scalingFactor;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;

    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null && (TouchedFinger.screenPosition.x <= Screen.width / 2f || TouchedFinger.screenPosition.x > Screen.width / 2f) && Time.timeScale != 0f)
        {
            MovementFinger = TouchedFinger;
            MovementAmount = Vector2.zero;
            Joystick.gameObject.SetActive(true);
            Joystick.RectTransform.sizeDelta = JoystickSize;
            Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);

        }
    }

    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        if(StartPosition.x < JoystickSize.x / 2)
        {
            StartPosition.x = JoystickSize.x / 2;
        }

        if (StartPosition.y < JoystickSize.y / 2)
        {
            StartPosition.y = JoystickSize.y / 2;
        }

        else if(StartPosition.y > Screen.height - JoystickSize.y / 2)
        {
            StartPosition.y = Screen.height - JoystickSize.y  / 2;
        }
        return StartPosition;
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if(LostFinger == MovementFinger)
        {
            IsMoving = false;
            MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;
            Joystick.gameObject.SetActive(false);
            MovementAmount = Vector2.zero;
        }
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        if(MovedFinger == MovementFinger)
        {
            IsMoving = true;
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 2f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            if (Vector2.Distance(currentTouch.screenPosition,Joystick.RectTransform.anchoredPosition) > maxMovement)
            {
                knobPosition = (currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition).normalized * maxMovement; 
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition;
            }
            Joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;

        }

    }

    private void Update()
    {
         float movementSpeed = PlayerSpeed() * Time.deltaTime;
    Vector3 movement = new Vector3(MovementAmount.x, 0f, MovementAmount.y) * movementSpeed;
    transform.position += movement;
    IsMoving = movement.magnitude > 0f;
        IsPlayerMoving();
        if (IsMoving)
        {
           player.transform.LookAt(player.transform.position + movement, Vector3.up);
        }
        else
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

    public bool IsPlayerMoving()
    {
        return IsMoving;
    }
    public float PlayerSpeed()
    {
        return playerSpeed; 
    }
}
