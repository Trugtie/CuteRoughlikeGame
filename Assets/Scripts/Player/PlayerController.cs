using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private WonMobileJoystick _joystick;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _playerRb;

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _playerRb.velocity = _joystick.GetMoveVector() * _moveSpeed * Time.deltaTime;
    }
}
