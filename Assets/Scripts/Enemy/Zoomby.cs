using System;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using Unity.VisualScripting;

[RequireComponent(typeof(RangeEnemyAttack))]
public class Zoomby : Enemy
{
    public Action OnStartIdle;
    public Action OnStartAttacking;
    public Action OnStartMoving;

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

    [Header(" Attacking State ")]
    private int _attackCounter;
    private RangeEnemyAttack _rangeEnemyAttack;

    private void Awake()
    {
        base.Awake();
        _rangeEnemyAttack = GetComponent<RangeEnemyAttack>();
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
        _state = State.Moving;
        _targetPosition = GetRandomMovePosition();
        OnStartMoving?.Invoke();
    }

    private Vector2 GetRandomMovePosition()
    {
        Vector2 targetPosition = Vector2.zero;

        targetPosition.x = Random.Range(-Constants.arenaSize.x, Constants.arenaSize.x);
        targetPosition.y = Random.Range(-Constants.arenaSize.y, Constants.arenaSize.y);

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
        _state = State.Attacking;
        _attackCounter = 0;
        OnStartAttacking?.Invoke();
    }

    public void Attack()
    {
        Vector2 direction = Quaternion.Euler(0, 0, -45 * _attackCounter) * Vector2.up;
        _rangeEnemyAttack.ShootToDirection(direction);
        _attackCounter++;
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

    public void StartIdleState()
    {
        _state = State.Idle;
        _idleDuration = Random.Range(1f, _maxIdleDuration);
        OnStartIdle?.Invoke();
    }

    protected override void PassAway()
    {
        OnBossPassAway?.Invoke(transform.position);
        PassAwayAfterWave();
    }

    private void UpdateVisual()
    {
        float healthValue = (float)_health / _maxHealth;

        _healthBar.value = healthValue;

        _healthText.SetText($"{(int)_health} / {_maxHealth}");
    }

}
