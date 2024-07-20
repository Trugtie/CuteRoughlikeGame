using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player _player;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _playerDetectionRadius;

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
    }

    private void Update()
    {
        FollowPlayer();
        TryAttack();
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
            EnemyDeadHandle();
        }

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
