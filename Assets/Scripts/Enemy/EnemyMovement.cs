using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player _player;

    [Header("Spawn Indicator")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _spawnIndicatorRenderer;
    private bool _hasSpawned;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _playerDetectionRadius;

    [Header("Attack")]
    [SerializeField] private int _attackDamge;
    [SerializeField] private int _attackFrequence;
    private float _attackDelay;
    private float _attackTimer;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _deadVFX;

    [Header("Debug")]
    [SerializeField] private bool _gizmos;

    private void Start()
    {
        _player = FindFirstObjectByType<Player>();

        if (_player == null)
        {
            Debug.LogWarning("No player in game !");
            Destroy(gameObject);
        }

        _spriteRenderer.enabled = false;
        _spawnIndicatorRenderer.enabled = true;

        Vector3 targetIndicatorScale = _spawnIndicatorRenderer.transform.localScale * 1.2f;
        LeanTween.scale(_spawnIndicatorRenderer.gameObject, targetIndicatorScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);

        _attackDelay = 1f / _attackFrequence;
    }

    private void Update()
    {
        if (!_hasSpawned)
            return;

        FollowPlayer();

        if (_attackTimer > _attackDelay)
            TryAttack();
        else
            WaitAttackDelay();
    }

    private void WaitAttackDelay()
    {
        _attackTimer += Time.deltaTime;
    }

    private void SpawnSequenceCompleted()
    {
        _spriteRenderer.enabled = true;
        _spawnIndicatorRenderer.enabled = false;
        _hasSpawned = true;
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }

    private void TryAttack()
    {
        float canAttackDistance = Vector2.Distance(transform.position, _player.transform.position);

        bool canAttack = canAttackDistance <= _playerDetectionRadius ? true : false;

        if (canAttack)
        {
            Attack();
        }

    }

    private void Attack()
    {
        Debug.Log($"Attack {_attackDamge} damge to player");
        _attackTimer = 0f;
    }

    private void EnemyDeadHandle()
    {
        _deadVFX.Play();
        _deadVFX.transform.SetParent(null);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!_gizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _playerDetectionRadius);
    }
}
