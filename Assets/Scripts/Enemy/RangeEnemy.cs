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

    private void Update()
    {
        if (!IsRenderedEnable())
            return;

        ManageAttack();

        transform.localScale = _player.transform.position.x > transform.position.x ? Vector3.one : new Vector3(-1, 1, 1);
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
