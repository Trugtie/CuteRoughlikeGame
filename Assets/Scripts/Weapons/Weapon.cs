using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour, IPlayerStatsDependency
{
    private enum WeaponState
    {
        Idle,
        Attack
    }

    [Header("Data")]
    [SerializeField] protected WeaponDataSO _weaponDataSO;

    [Header("Elements")]
    private Animator _animator;
    protected List<Enemy> _enemiesAttackedList;

    [Header("Settings")]
    protected int _weaponDamge;
    [SerializeField] protected float _weaponRange;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] protected LayerMask _enemyLayer;
    [SerializeField] protected float _attackDelay;
    protected float _criticalPercent;
    protected int _criticalChance;

    private WeaponState _weaponState;
    protected Enemy _enemyClosest;
    private float _minDistance;

    private float _attackTimer;

    [field: SerializeField] public int Level { get; private set; }

    protected virtual void Awake()
    {
        _enemiesAttackedList = new List<Enemy>();
        _animator = GetComponent<Animator>();
        _weaponState = WeaponState.Idle;
        SetEnemyClosetWithMinDistance(_weaponRange);
    }

    private void Update()
    {
        switch (_weaponState)
        {
            case WeaponState.Idle:
                AutoAimTarget();
                ManageAttack();
                break;
            case WeaponState.Attack:
                Attacking();
                break;
            default:
                break;
        }
    }

    private void ManageAttack()
    {
        CheckinghEnemyClosestMissing();

        bool canAttack = _attackTimer >= _attackDelay && _enemyClosest != null;

        if (canAttack)
            StartAttack();

        IncreaseAttackTimer();
    }

    private void IncreaseAttackTimer()
    {
        _attackTimer += Time.deltaTime;
    }

    [NaughtyAttributes.Button]
    private void StartAttack()
    {
        _animator.Play("Attack");
        _weaponState = WeaponState.Attack;
        _attackTimer = 0f;
        _enemiesAttackedList.Clear();
        _animator.speed = 1f / _attackDelay;
    }

    private void Attacking()
    {
        Attack();
    }

    public void StopAttack()
    {
        SetEnemyClosetWithMinDistance(_weaponRange);
        _weaponState = WeaponState.Idle;
        _enemiesAttackedList.Clear();
    }

    private void AutoAimTarget()
    {
        Vector2 lookAtDirection = Vector2.up;

        FindEnemyClosest();

        if (_enemyClosest != null)
        {
            lookAtDirection = (_enemyClosest.transform.position - transform.position).normalized;
            transform.up = lookAtDirection;
        }

        transform.up = Vector3.Lerp(transform.up, lookAtDirection, _rotationSpeed * Time.deltaTime);
    }

    protected void FindEnemyClosest()
    {
        Collider2D[] enemies = GetAllEnemyInRange();

        if (enemies.Length <= 0)
        {
            SetEnemyClosetWithMinDistance(_weaponRange);
            return;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();

            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

            if (distanceToEnemy < _minDistance)
            {
                SetEnemyClosetWithMinDistance(distanceToEnemy, enemyChecked);
            }
        }
    }

    private void CheckinghEnemyClosestMissing()
    {
        bool isMissing = _enemyClosest == null;

        if (isMissing)
        {
            SetEnemyClosetWithMinDistance(_weaponRange);
        }
    }

    protected Enemy GetEnemyClosest()
    {
        return _enemyClosest;
    }

    protected Collider2D[] GetAllEnemyInRange()
    {
        return Physics2D.OverlapCircleAll(transform.position, _weaponRange, _enemyLayer);
    }

    protected virtual void Attack()
    {
    }

    private void SetEnemyClosetWithMinDistance(float minDistance, Enemy enemyCloset = null)
    {
        _minDistance = minDistance;
        _enemyClosest = enemyCloset;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _weaponRange);
    }

    protected int GetCriticalWeaponDamge(out bool isCritical)
    {
        isCritical = false;
        int randomPercent = Random.Range(0, 101);

        if (randomPercent <= _criticalChance)
        {
            isCritical = true;
            return Mathf.RoundToInt(_weaponDamge * _criticalPercent);
        }

        return _weaponDamge;
    }

    protected void ConfigueDamge()
    {
        float multiplier = 1 + (float)Level / 3;
        _weaponDamge = Mathf.RoundToInt(_weaponDataSO.GetStatValue(Stats.Attack) * multiplier);
        _attackDelay = 1 / (_weaponDataSO.GetStatValue(Stats.AttackSpeed) * multiplier);

        _criticalChance = Mathf.RoundToInt(_weaponDataSO.GetStatValue(Stats.CriticalChance) * multiplier);
        _criticalPercent = _weaponDataSO.GetStatValue(Stats.CriticalPercent) * multiplier;

        if (_weaponDataSO.Prefab.GetType() == typeof(RangeWeapon))
            _weaponRange = _weaponDataSO.GetStatValue(Stats.Range) * multiplier;

    }

    public abstract void UpdatePlayerStats(PlayerStatsManager playerStatsManager);
}
