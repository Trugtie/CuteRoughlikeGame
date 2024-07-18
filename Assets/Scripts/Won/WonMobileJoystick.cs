using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonMobileJoystick : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform _joystickOutline;

    public void TouchedScreenCallpack()
    {
        Vector2 touchScreenPos = Input.mousePosition;
        _joystickOutline.transform.position = touchScreenPos;
    }
}
