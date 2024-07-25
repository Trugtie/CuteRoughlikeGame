using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform _hitPosition;

    [Header("Settings")]
    [SerializeField] private float _hitRange;
    [SerializeField] private float _weaponRange;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private LayerMask _enemyLayer;

    private Enemy _enemyCloset;
    private float _minDistance;

    private void Awake()
    {
        SetEnemyClosetWithMinDistance(_weaponRange);
    }

    private void Update()
    {
        AutoAimTarget();
        Attack();
    }

    private void AutoAimTarget()
    {
        Vector2 lookAtDirection = Vector2.up;

        GetEnemyClosest();

        if (_enemyCloset != null)
            lookAtDirection = (_enemyCloset.transform.position - transform.position).normalized;

        transform.up = Vector3.Lerp(transform.up, lookAtDirection, _rotationSpeed * Time.deltaTime);
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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_hitPosition.position, _hitRange, _enemyLayer);

        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
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
        Gizmos.DrawWireSphere(_hitPosition.position, _hitRange);
    }
}
