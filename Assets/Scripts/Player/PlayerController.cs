using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IPlayerStatsDependency
{
    [Header("Elements")]
    [SerializeField] private WonMobileJoystick _joystick;

    [Header("Settings")]
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _baseMoveSpeed;
    private float _moveSpeed;

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

    public void UpdatePlayerStats(PlayerStatsManager playerStatsManager)
    {
        float moveSpeedStatValue = playerStatsManager.GetStatValue(Stats.MoveSpeed) / 100;

        _moveSpeed = _baseMoveSpeed + _baseMoveSpeed * moveSpeedStatValue;

        _moveSpeed = Mathf.Max(_moveSpeed, _minSpeed);
    }
}
