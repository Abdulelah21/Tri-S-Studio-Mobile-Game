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

    [SerializeField]
    private NavMeshAgent Player;

    private Finger MovementFinger;
    private Vector2 MovementAmount;
    
    
    bool IsMoving = false;
    float MoveSpeed;

    private void Start()
    {
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
        if (MovementFinger == null && TouchedFinger.screenPosition.x <= Screen.width /1f)
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
        Vector3 scaledMovement = Player.speed * Time.deltaTime * new Vector3(MovementAmount.x, 0, MovementAmount.y);
        Player.Move(scaledMovement);
        IsPlayerMoving();
    }

    public bool IsPlayerMoving()
    {
        return IsMoving;
    }
    public float PlayerSpeed()
    {
        return MoveSpeed = Player.speed;
    }
}
