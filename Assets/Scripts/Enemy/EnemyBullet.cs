using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Elements")]
    private Vector2 _targetDirection;
    private Rigidbody2D _bulletRb;
    private Player player;
    private RangeEnemyAttack _rangeEnemyAttack;
    private Collider2D _collider;

    [Header("Settings")]
    [SerializeField] private float _flySpeed;
    private int _damge;

    private void Awake()
    {
        _bulletRb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        LeanTween.delayedCall(gameObject, 5f, () => { _rangeEnemyAttack.RealeaseBullet(this); });
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<Player>(out player))
            return;

        LeanTween.cancel(gameObject);

        _collider.enabled = false;
        player.TakeDamge(_damge);
        _rangeEnemyAttack.RealeaseBullet(this);
    }

    private void MoveToTarget()
    {
        transform.right = _targetDirection;
        _bulletRb.velocity = _targetDirection * _flySpeed * Time.deltaTime;
    }

    public void Configue(Vector3 targetDirection, int damge, RangeEnemyAttack rangeEnemyAttack)
    {
        _targetDirection = targetDirection;
        _damge = damge;
        _rangeEnemyAttack = rangeEnemyAttack;
    }

    public void Reload()
    {
        _collider.enabled = true;
        _bulletRb.velocity = Vector2.zero;
    }

}
