using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Bullet _enemyBullet;
    [SerializeField] private Transform _bulletPoolContainer;

    private Player _player;
    private ObjectPool<Bullet> _enemyBulletPool;


    [Header("Settings")]
    [SerializeField] private int _attackDamge;
    [SerializeField] private int _attackFrequence;

    private float _attackDelay;
    private float _attackTimer;

    private void Awake()
    {
        _attackDelay = 1f / _attackFrequence;
        _attackTimer = _attackDelay;
        _enemyBulletPool = new ObjectPool<Bullet>(ActionOnCreateBullet, ActionOnGet, ActionOnReLease, ActionOnDestroy, false, 10);
    }

    private void ActionOnDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void ActionOnReLease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnGet(Bullet bullet)
    {
        bullet.Reload();
        bullet.gameObject.SetActive(true);
    }

    private Bullet ActionOnCreateBullet()
    {
        Bullet bullet = Instantiate(_enemyBullet, _bulletPoolContainer);
        bullet.Configue(_shootingPoint, _attackDamge, false, _enemyBulletPool);
        return bullet;
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
        Bullet enemyBullet = _enemyBulletPool.Get();
        enemyBullet.Configue(_shootingPoint, _attackDamge, false, _enemyBulletPool);
        enemyBullet.SetTargetDirection(toPlayerDirection);
    }

    public void Configue(Player player)
    {
        _player = player;
    }
}
