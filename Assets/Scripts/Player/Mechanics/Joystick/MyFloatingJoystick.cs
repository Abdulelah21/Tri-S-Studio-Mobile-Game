using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class MyFloatingJoystick : MonoBehaviour
{
    [HideInInspector]
    public RectTransform RectTransform;
    public RectTransform Knob;


    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
    }
}
