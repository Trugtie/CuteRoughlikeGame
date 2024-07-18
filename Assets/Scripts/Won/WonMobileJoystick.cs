using UnityEngine;

public class WonMobileJoystick : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform _joystickOutline;
    [SerializeField] private RectTransform _joystickKnob;

    [Header("Setting")]
    [SerializeField] private float _moveFactor;
    private Vector3 _touchedOnScreenPos;
    private Vector3 _move;
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

        _move = Vector3.zero;
    }

    private void ControlJoystick()
    {
        Vector3 currentTouchedPos = Input.mousePosition;
        Vector3 moveDir = currentTouchedPos - _touchedOnScreenPos;

        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;

        float moveMagnitude = moveDir.magnitude * _moveFactor * canvasScale;

        float absoluteWidth = _joystickOutline.rect.width / 2;

        float realWidth = absoluteWidth * canvasScale;

        moveMagnitude = Mathf.Min(moveMagnitude, realWidth);

        _move = moveDir.normalized * moveMagnitude;

        Vector3 targetPosition = _touchedOnScreenPos + _move;

        _joystickKnob.position = targetPosition;

        if (Input.GetMouseButtonUp(0))
        {
            HideJoyStick();
        }
    }

    public Vector3 GetMoveVector()
    {
        float canvasScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.x;
        return _move / canvasScale;
    }
}
