using UnityEngine;
using UnityEngine.EventSystems;

public class UIJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject joystick;

    void Start()
    {
        joystick.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystick.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.SetActive(false);
    }
}