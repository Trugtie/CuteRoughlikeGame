using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform _shootingPoint;
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

    private Vector2 gizmodsDirection;

    private void Shoot()
    {
        Vector2 toPlayerDirection = (_player.GetCenterPoint() - _shootingPoint.position).normalized;
        gizmodsDirection = toPlayerDirection;
    }

    public void Configue(Player player)
    {
        _player = player;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_shootingPoint.position, _shootingPoint.position + (Vector3)gizmodsDirection * 5);
    }

}
