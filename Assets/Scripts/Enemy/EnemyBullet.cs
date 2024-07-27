using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Elements")]
    private Vector2 _targetDirection;
    private Rigidbody2D _bulletRb;
    private Player player;

    [Header("Settings")]
    [SerializeField] private float _flySpeed;
    private int _damge;

    private void Awake()
    {
        _bulletRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<Player>(out player))
            return;
        player.TakeDamge(_damge);
        Destroy(gameObject);
    }

    private void MoveToTarget()
    {
        transform.right = _targetDirection;
        _bulletRb.velocity = _targetDirection * _flySpeed * Time.deltaTime;
    }

    public void Configue(Vector3 targetDirection, int damge)
    {
        _targetDirection = targetDirection;
        _damge = damge;
    }

}
