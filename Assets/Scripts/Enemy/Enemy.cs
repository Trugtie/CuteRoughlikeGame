using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public abstract class Enemy : MonoBehaviour
{
    public static Action<Vector2, int> OnAnyHit;

    [Header("Components")]
    protected EnemyMovement _enemyMovement;

    [Header("Elements")]
    [SerializeField] private Transform _damgeTextSpawnPosition;
    protected Player _player;
    private CircleCollider2D _enemyCollider;

    [Header("Settings")]
    [SerializeField] protected float _playerDetectionRadius;
    [SerializeField] private int _maxHealth;
    private int _health;

    [Header("Spawn Indicator")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _spawnIndicatorRenderer;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _deadVFX;

    [Header("Debug")]
    [SerializeField] private bool _gizmos;

    protected virtual void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyCollider = GetComponent<CircleCollider2D>();
        _health = _maxHealth;
    }

    protected virtual void Start()
    {
        _player = FindFirstObjectByType<Player>();

        if (_player == null)
        {
            Debug.LogWarning("No player in game !");
            Destroy(gameObject);
        }

        SpawnIndicatorRenderToggle(true);

        Vector3 targetIndicatorScale = _spawnIndicatorRenderer.transform.localScale * 1.2f;
        LeanTween.scale(_spawnIndicatorRenderer.gameObject, targetIndicatorScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);
    }

    protected virtual void Update()
    {
        if (!_spriteRenderer.enabled)
            return;
    }

    private void SpawnIndicatorRenderToggle(bool isShow)
    {
        _spriteRenderer.enabled = !isShow;
        _spawnIndicatorRenderer.enabled = isShow;
    }

    private void SpawnSequenceCompleted()
    {
        _enemyCollider.enabled = true;
        SpawnIndicatorRenderToggle(false);
        _enemyMovement.SetPlayer(_player);
    }

    protected void PassAway()
    {
        _deadVFX.Play();
        _deadVFX.transform.SetParent(null);
        Destroy(gameObject);
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

    private void OnDrawGizmos()
    {
        if (!_gizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _playerDetectionRadius);
    }

    protected virtual void TryAttack()
    {

    }

}
