using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    private EnemyMovement _enemyMovement;

    [Header("Elements")]
    private Player _player;

    [Header("Settings")]
    [SerializeField] private float _playerDetectionRadius;

    [Header("Spawn Indicator")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _spawnIndicatorRenderer;

    [Header("Attack")]
    [SerializeField] private int _attackDamge;
    [SerializeField] private int _attackFrequence;
    private float _attackDelay;
    private float _attackTimer;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _deadVFX;

    [Header("Debug")]
    [SerializeField] private bool _gizmos;

    void Start()
    {
        _player = FindFirstObjectByType<Player>();
        _enemyMovement = GetComponent<EnemyMovement>();

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

        _attackDelay = 1f / _attackFrequence;
    }

    private void SpawnIndicatorRenderToggle(bool isShow)
    {
        _spriteRenderer.enabled = !isShow;
        _spawnIndicatorRenderer.enabled = isShow;
    }

    void Update()
    {
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
        SpawnIndicatorRenderToggle(false);
        _enemyMovement.SetPlayer(_player);
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
