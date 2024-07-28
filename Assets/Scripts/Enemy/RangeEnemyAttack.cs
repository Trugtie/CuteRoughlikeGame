using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private EnemyBullet _enemyBullet;
    private Player _player;
    private ObjectPool<EnemyBullet> _enemyPulletPool;

    [Header("Settings")]
    [SerializeField] private int _attackDamge;
    [SerializeField] private int _attackFrequence;
    private float _attackDelay;
    private float _attackTimer;

    private void Awake()
    {
        _attackDelay = 1f / _attackFrequence;
        _attackTimer = _attackDelay;
        _enemyPulletPool = new ObjectPool<EnemyBullet>(ActionCreateBullet, ActionGetBullet, ActionRealeaseBullet, ActionDestroyBullet);
    }

    private EnemyBullet ActionCreateBullet()
    {
        return Instantiate(_enemyBullet, _shootingPoint);
    }
    private void ActionGetBullet(EnemyBullet bullet)
    {
        bullet.Reload();
        bullet.transform.position = _shootingPoint.position;
        bullet.gameObject.SetActive(true);
    }
    private void ActionRealeaseBullet(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionDestroyBullet(EnemyBullet bullet)
    {
        Destroy(bullet);
    }

    public void AutoAim()
    {
        ManageShooting();
    }


    private void ManageShooting()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer >= _attackDelay)
        {
            _attackTimer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector2 toPlayerDirection = (_player.GetCenterPoint() - _shootingPoint.position).normalized;

        EnemyBullet enemyBullet = _enemyPulletPool.Get();
        enemyBullet.Configue(toPlayerDirection, _attackDamge, this);
    }

    public void Configue(Player player)
    {
        _player = player;
    }

    public void RealeaseBullet(EnemyBullet enemyBullet)
    {
        _enemyPulletPool.Release(enemyBullet);
    }
}
