using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamgedable
{
    public static Action<Vector2, int, bool> OnAnyHit;
    public static Action<Vector2> OnAnyPassAway;
    protected Action onSpawnSequenceCompleted;

    [Header("Components")]
    protected EnemyMovement _enemyMovement;

    [Header("Elements")]
    [SerializeField] private Transform _damgeTextSpawnPosition;
    protected Player _player;
    private CircleCollider2D _enemyCollider;

    [Header("Settings")]
    [SerializeField] protected float _playerDetectionRadius;
    [SerializeField] protected int _maxHealth;
    protected int _health;

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

    protected virtual bool IsRenderedEnable()
    {
        return _spriteRenderer.enabled;
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

        if (_enemyMovement != null)
            _enemyMovement.SetPlayer(_player);

        onSpawnSequenceCompleted?.Invoke();
    }

    protected void PassAway()
    {
        OnAnyPassAway?.Invoke(transform.position);
        PassAwayAfterWave();
    }

    public void PassAwayAfterWave()
    {
        _deadVFX.Play();
        _deadVFX.transform.SetParent(null);
        Destroy(gameObject);
    }

    public void TakeDamge(int damge, bool isCriticalHit)
    {
        int realDamge = Mathf.Min(damge, _health);

        _health -= realDamge;

        OnAnyHit?.Invoke(_damgeTextSpawnPosition.position, damge, isCriticalHit);

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
