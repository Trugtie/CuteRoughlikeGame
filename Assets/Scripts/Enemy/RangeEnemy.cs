using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement), typeof(RangeEnemyAttack))]
public class RangeEnemy : MonoBehaviour
{
    public static Action<Vector2, int> OnAnyHit;

    [Header("Components")]
    private EnemyMovement _enemyMovement;

    [Header("Elements")]
    [SerializeField] private Transform _damgeTextSpawnPosition;
    private Player _player;
    private CircleCollider2D _enemyCollider;
    private RangeEnemyAttack _rangeEnemyAttack;

    [Header("Settings")]
    [SerializeField] private float _playerDetectionRadius;
    [SerializeField] private int _maxHealth;
    private int _health;

    [Header("Spawn Indicator")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _spawnIndicatorRenderer;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _deadVFX;

    [Header("Debug")]
    [SerializeField] private bool _gizmos;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyCollider = GetComponent<CircleCollider2D>();
        _rangeEnemyAttack = GetComponent<RangeEnemyAttack>();
        _health = _maxHealth;
    }

    void Start()
    {
        _player = FindFirstObjectByType<Player>();

        if (_player == null)
        {
            Debug.LogWarning("No player in game !");
            Destroy(gameObject);
        }

        _rangeEnemyAttack.Configue(_player);

        SpawnIndicatorRenderToggle(true);

        Vector3 targetIndicatorScale = _spawnIndicatorRenderer.transform.localScale * 1.2f;
        LeanTween.scale(_spawnIndicatorRenderer.gameObject, targetIndicatorScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);
    }

    private void SpawnIndicatorRenderToggle(bool isShow)
    {
        _spriteRenderer.enabled = !isShow;
        _spawnIndicatorRenderer.enabled = isShow;
    }

    private void Update()
    {
        if (!_spriteRenderer.enabled)
            return;

        ManageAttack();


    }

    private void ManageAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer >= _playerDetectionRadius)
        {
            _enemyMovement.FollowPlayer();
        }
        else
        {
            TryAttack();
        }
    }

    private void SpawnSequenceCompleted()
    {
        _enemyCollider.enabled = true;
        SpawnIndicatorRenderToggle(false);
        _enemyMovement.SetPlayer(_player);
    }

    private void TryAttack()
    {
        _rangeEnemyAttack.AutoAim();
    }

    public void TakeDamge(int damge)
    {
        int realDamge = Mathf.Min(damge, _health);

        _health -= realDamge;

        OnAnyHit?.Invoke(_damgeTextSpawnPosition.position, damge);

        if (_health <= 0)
        {
            PassAway();
        }
    }

    private void PassAway()
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
