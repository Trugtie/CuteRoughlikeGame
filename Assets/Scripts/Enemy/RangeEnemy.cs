using UnityEngine;

[RequireComponent(typeof(RangeEnemyAttack))]
public class RangeEnemy : Enemy
{
    private RangeEnemyAttack _rangeEnemyAttack;

    protected override void Awake()
    {
        base.Awake();
        _rangeEnemyAttack = GetComponent<RangeEnemyAttack>();
    }

    protected override void Start()
    {
        base.Start();
        _rangeEnemyAttack.Configue(_player);
    }

    protected override void Update()
    {
        base.Update();

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

    protected override void TryAttack()
    {
        _rangeEnemyAttack.AutoAim();
    }
}
