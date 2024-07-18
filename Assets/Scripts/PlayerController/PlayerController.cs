using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private WonMobileJoystick _joystick;
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _playerRb;

    private void Start()
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
