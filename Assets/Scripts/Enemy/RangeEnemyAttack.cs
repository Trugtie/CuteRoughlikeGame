using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private EnemyBullet _enemyBullet;
    private Player _player;

    [Header("Settings")]
    [SerializeField] private int _attackDamge;
    [SerializeField] private int _attackFrequence;
    private float _attackDelay;
    private float _attackTimer;

    private void Awake()
    {
        _attackDelay = 1f / _attackFrequence;
        _attackTimer = _attackDelay;
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
        EnemyBullet enemyBullet = Instantiate(_enemyBullet, _shootingPoint.position, Quaternion.identity);
        enemyBullet.Configue(toPlayerDirection, _attackDamge);
    }

    public void Configue(Player player)
    {
        _player = player;
    }
}
