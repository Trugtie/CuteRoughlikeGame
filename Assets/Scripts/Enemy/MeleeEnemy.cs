using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Attack")]
    [SerializeField] private int _attackDamge;
    [SerializeField] private int _attackFrequence;
    private float _attackDelay;
    private float _attackTimer;

    protected override void Start()
    {
        base.Start();
        _attackDelay = 1f / _attackFrequence;
    }

    private void Update()
    {
        if (!IsRenderedEnable())
            return;

        if (_attackTimer > _attackDelay)
            TryAttack();
        else
            WaitAttackDelay();
    }

    private void WaitAttackDelay()
    {
        _attackTimer += Time.deltaTime;
    }

    protected override void TryAttack()
    {
        float canAttackDistance = Vector2.Distance(transform.position, _player.transform.position);

        bool canAttack = canAttackDistance <= _playerDetectionRadius ? true : false;

        if (canAttack)
        {
            Attack();
        }

        _enemyMovement.FollowPlayer();

    }
    private void Attack()
    {
        _player.TakeDamge(_attackDamge);
        _attackTimer = 0f;
    }
}
