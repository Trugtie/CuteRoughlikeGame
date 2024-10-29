using System;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Zoomby : Enemy
{
    [Header(" Health Bar ")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;

    private enum State
    {
        None, Idle, Moving, Attacking
    }

    [Header(" State Machine ")]
    private State _state;
    private float _timer;

    [Header(" Idle State ")]
    [SerializeField] private float _maxIdleDuration;
    private float _idleDuration;

    [Header(" Moving State")]
    [SerializeField] private float _moveSpeed;
    private Vector2 _targetPosition;

    private void Awake()
    {
        base.Awake();
        _state = State.None;
    }

    private void Start()
    {
        base.Start();

        _healthBar.gameObject.SetActive(false);

        onSpawnSequenceCompleted += onSpawnSequenceCompletedCallBack;
        OnAnyHit += OnAnyHitCallback;
    }

    private void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch (_state)
        {
            case State.Idle:
                ManageIdleState();
                break;
            case State.Moving:
                ManageMovingState();
                break;
            case State.Attacking:
                ManageAttackingState();
                break;
            default:
                break;
        }
    }

    private void ManageIdleState()
    {
        _timer += Time.deltaTime;

        if (_timer > _maxIdleDuration)
        {
            _timer = 0;
            StartMovingState();
        }
    }

    private void StartMovingState()
    {
        Debug.Log("Start Moving State");
        _state = State.Moving;
        _targetPosition = GetRandomMovePosition();
    }

    private Vector2 GetRandomMovePosition()
    {
        Vector2 targetPosition = Vector2.zero;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -19, 19);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -9, 16);

        return targetPosition;
    }

    private void ManageMovingState()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

        bool isCloseTargetPos = Vector2.Distance(transform.position, _targetPosition) < 0.01f;

        if (isCloseTargetPos)
            StartAttacking();
    }

    private void StartAttacking()
    {
        Debug.Log("Start Attacking State");
    }

    private void ManageAttackingState()
    {

    }

    private void OnDestroy()
    {
        onSpawnSequenceCompleted += onSpawnSequenceCompletedCallBack;
        OnAnyHit -= OnAnyHitCallback;
    }

    private void OnAnyHitCallback(Vector2 position, int damge, bool isCriticalHit)
    {
        UpdateVisual();
    }

    private void onSpawnSequenceCompletedCallBack()
    {
        _healthBar.gameObject.SetActive(true);
        UpdateVisual();
        StartIdleState();
    }

    private void StartIdleState()
    {
        Debug.Log("Start Idle State");
        _state = State.Idle;
        _idleDuration = Random.Range(1f, _maxIdleDuration);
    }

    private void UpdateVisual()
    {
        float healthValue = (float)_health / _maxHealth;

        _healthBar.value = healthValue;

        _healthText.SetText($"{(int)_health} / {_maxHealth}");
    }

}
