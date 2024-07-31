using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private LayerMask _canDamgeLayer;
    private Transform _shootingPosition;
    private Vector2 _targetDirection;
    private Rigidbody2D _bulletRb;
    private Collider2D _collider;
    private ObjectPool<Bullet> _bulletPool;

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
        LeanTween.delayedCall(gameObject, 5f, () => { _bulletPool.Release(this); });
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsDamgedableLayer(other.gameObject.layer, _canDamgeLayer))
            return;

        LeanTween.cancel(gameObject);

        _collider.enabled = false;
        IDamgedable iDamgedable = other.GetComponent<IDamgedable>();
        iDamgedable.TakeDamge(_damge);
        DestroyBullet();
    }


    private void MoveToTarget()
    {
        transform.right = _targetDirection;
        _bulletRb.velocity = _targetDirection * _flySpeed * Time.deltaTime;
    }

    public void Reload()
    {
        _collider.enabled = true;
        transform.position = _shootingPosition.position;
        _bulletRb.velocity = Vector2.zero;
    }

    public void Configue(Transform shootingPosition, int damge, ObjectPool<Bullet> pool)
    {
        _shootingPosition = shootingPosition;
        _damge = damge;
        _bulletPool = pool;
        transform.position = shootingPosition.position;
    }

    public void SetTargetDirection(Vector3 direction)
    {
        _targetDirection = direction;
    }

    public void DestroyBullet()
    {
        _bulletPool.Release(this);
    }

    private bool IsDamgedableLayer(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }

}
