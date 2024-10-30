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
    private IDamgedable _iDamgedable;

    [Header("Settings")]
    [SerializeField] private float _flySpeed;
    [SerializeField] private float _angularRotateSpeed;
    private int _damge;
    private bool _isCriticalHit;

    private void Awake()
    {
        _bulletRb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        LeanTween.delayedCall(gameObject, 5f, () => { _bulletPool.Release(this); });
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_iDamgedable != null)
            return;

        if (!IsDamgedableLayer(other.gameObject.layer, _canDamgeLayer))
            return;

        _iDamgedable = other.GetComponent<IDamgedable>();

        LeanTween.cancel(gameObject);

        _collider.enabled = false;
        _iDamgedable.TakeDamge(_damge, _isCriticalHit);

        DestroyBullet();
    }


    private void MoveToTarget()
    {
        transform.right = _targetDirection;
        _bulletRb.linearVelocity = _targetDirection * _flySpeed * Time.deltaTime;
        _bulletRb.AddTorque(_angularRotateSpeed * Time.fixedDeltaTime);
    }

    public void Reload()
    {
        _iDamgedable = null;
        _collider.enabled = true;
        transform.position = _shootingPosition.position;
        _bulletRb.linearVelocity = Vector2.zero;
        _bulletRb.angularVelocity = 0;

        LeanTween.cancel(gameObject);
        LeanTween.delayedCall(gameObject, 5f, () => { _bulletPool.Release(this); });
    }

    public void Configue(Transform shootingPosition, int damge, bool isCriticalHit, ObjectPool<Bullet> pool)
    {
        _shootingPosition = shootingPosition;
        _damge = damge;
        _isCriticalHit = isCriticalHit;
        _bulletPool = pool;
        transform.position = shootingPosition.position;
    }

    public void SetTargetDirection(Vector3 direction)
    {
        _targetDirection = direction;

        if (MathF.Abs(direction.x + 1) < 0.01f)
        {
            direction.y += 0.1f;
        }
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
