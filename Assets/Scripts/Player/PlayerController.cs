using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IPlayerStatsDependency, IGameStateListener
{
    public static PlayerController Instance { get; private set; }

    [Header("Elements")]
    [SerializeField] private WonMobileJoystick _joystick;

    [Header("Settings")]
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _baseMoveSpeed;
    public float MoveSpeed { get; private set; }
    public float PlayerRbVelocityMagnitude { get; private set; }

    private Rigidbody2D _playerRb;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _playerRb.velocity = _joystick.GetMoveVector() * MoveSpeed * Time.deltaTime;
        PlayerRbVelocityMagnitude = _playerRb.velocity.magnitude;
    }

    public void UpdatePlayerStats(PlayerStatsManager playerStatsManager)
    {
        float moveSpeedStatValue = playerStatsManager.GetStatValue(Stats.MoveSpeed) / 100;

        MoveSpeed = _baseMoveSpeed * (1 + moveSpeedStatValue);

        MoveSpeed = Mathf.Max(MoveSpeed, _minSpeed);
    }

    public void GameStateChangedCallback(GameStates gameState)
    {
        if (gameState != GameStates.GAMEPLAY)
            _playerRb.velocity = Vector2.zero;
    }
}
