using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangeWeapon : Weapon
{
    [Header("Elements")]
    [SerializeField] private Transform _shootStartPosition;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletPoolContainer;
    private ObjectPool<Bullet> _rangeBulletPool;


    protected override void Awake()
    {
        base.Awake();
        _rangeBulletPool = new ObjectPool<Bullet>(ActionOnCreateBullet, ActionOnGetBullet, ActionOnReleaseBullet, ActionOnDestroyBullet, false, 10, 50);
    }

    private void ActionOnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void ActionOnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnGetBullet(Bullet bullet)
    {
        bullet.Reload();
        bullet.gameObject.SetActive(true);
    }

    private Bullet ActionOnCreateBullet()
    {
        Bullet bullet = Instantiate(_bullet, _bulletPoolContainer);

        int damge = GetCriticalWeaponDamge(out bool isCritical);

        bullet.Configue(_shootStartPosition, damge, isCritical, _rangeBulletPool);
        return bullet;
    }

    private void Shoot()
    {
        if (_enemyClosest == null) return;

        Vector2 toEnemyCLosestDirection = ((Vector2)(_enemyClosest.transform.position - _shootStartPosition.position)).normalized;
        Bullet bullet = _rangeBulletPool.Get();
        bullet.SetTargetDirection(toEnemyCLosestDirection);
    }

    public override void UpdatePlayerStats(PlayerStatsManager playerStatsManager)
    {
        ConfigueDamge();
        _weaponDamge = Mathf.RoundToInt(_weaponDamge * (1 + playerStatsManager.GetStatValue(Stats.Attack) / 100));
    }
}
