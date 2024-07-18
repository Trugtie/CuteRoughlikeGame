using UnityEngine;

public class WonMobileJoystick : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform _joystickOutline;
    [SerializeField] private RectTransform _joystickKnob;

    [Header("Setting")]
    private Vector3 _touchedOnScreenPos;
    private bool _canControl;

    private void Start()
    {
        HideJoyStick();
    }

    private void Update()
    {
        if (_canControl)
            ControlJoystick();
    }

    public void TouchedScreenCallpack()
    {
        _touchedOnScreenPos = Input.mousePosition;
        _joystickOutline.transform.position = _touchedOnScreenPos;

        ShowJoystick();
    }

    private void ShowJoystick()
    {
        _joystickOutline.gameObject.SetActive(true);
        _canControl = true;
    }

    private void HideJoyStick()
    {
        _joystickOutline.gameObject.SetActive(false);
        _canControl = false;
    }

    private void ControlJoystick()
    {
        Vector3 currentTouchedPos = Input.mousePosition;
        Vector3 moveDir = currentTouchedPos - _touchedOnScreenPos;

        _joystickKnob.position = _joystickOutline.position + moveDir;
    }
}
