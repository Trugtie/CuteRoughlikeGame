using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private enum WeaponState
    {
        Idle,
        Attack
    }

    [Serializable]
    private struct HitPoint
    {
        public Transform hitTransform;
        public float hitRange;
    }

    [Header("Elements")]
    [SerializeField] private HitPoint[] _hitPositions;
    private Animator _animator;
    private List<Enemy> _enemiesAttackedList;

    [Header("Settings")]
    [SerializeField] private int _weaponDamge;
    [SerializeField] private float _weaponRange;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _attackDelay;

    private WeaponState _weaponState;
    private Enemy _enemyCloset;
    private float _minDistance;

    private float _attackTimer;

    private void Awake()
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
        if (_attackTimer >= _attackDelay)
        {
            _attackTimer = 0f;
            StartAttack();
        }
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

    private void StopAttack()
    {
        _weaponState = WeaponState.Idle;
        _enemiesAttackedList.Clear();
    }

    private void AutoAimTarget()
    {
        Vector2 lookAtDirection = Vector2.up;

        GetEnemyClosest();

        if (_enemyCloset != null)
        {
            lookAtDirection = (_enemyCloset.transform.position - transform.position).normalized;
            transform.up = lookAtDirection;
            ManageAttack();
        }

        transform.up = Vector3.Lerp(transform.up, lookAtDirection, _rotationSpeed * Time.deltaTime);

        IncreaseAttackTimer();
    }

    private void GetEnemyClosest()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _weaponRange, _enemyLayer);

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

    private void Attack()
    {
        foreach (HitPoint hitpoint in _hitPositions)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(hitpoint.hitTransform.position, hitpoint.hitRange, _enemyLayer);

            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy enemyTargetAttack = enemies[i].GetComponent<Enemy>();

                if (_enemiesAttackedList.Contains(enemyTargetAttack))
                    continue;

                enemyTargetAttack.TakeDamge(_weaponDamge);
                _enemiesAttackedList.Add(enemyTargetAttack);
            }
        }
    }

    private void SetEnemyClosetWithMinDistance(float minDistance, Enemy enemyCloset = null)
    {
        _minDistance = minDistance;
        _enemyCloset = enemyCloset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _weaponRange);

        Gizmos.color = Color.red;
        foreach (HitPoint hitpoint in _hitPositions)
        {
            Gizmos.DrawWireSphere(hitpoint.hitTransform.position, hitpoint.hitRange);
        }
    }
}
