using System;
using UnityEngine;

public class MeleeWeapon : Weapon
{

    [Serializable]
    private struct HitPoint
    {
        public Transform hitTransform;
        public float hitRange;
    }

    [Header("Elements")]
    [SerializeField] private HitPoint[] _hitPositions;

    protected override void Attack()
    {
        foreach (HitPoint hitpoint in _hitPositions)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(hitpoint.hitTransform.position, hitpoint.hitRange, _enemyLayer);

            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy enemyTargetAttack = enemies[i].GetComponent<Enemy>();

                if (_enemiesAttackedList.Contains(enemyTargetAttack))
                    continue;

                int damge = GetCriticalWeaponDamge(out bool isCritical);

                enemyTargetAttack.TakeDamge(damge, isCritical);
                _enemiesAttackedList.Add(enemyTargetAttack);
            }
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        foreach (HitPoint hitpoint in _hitPositions)
        {
            Gizmos.DrawWireSphere(hitpoint.hitTransform.position, hitpoint.hitRange);
        }
    }

    public override void UpdatePlayerStats(PlayerStatsManager playerStatsManager)
    {
        _weaponDamge = Mathf.RoundToInt(_weaponBaseDamge * (1 + (_weaponDataSO.BaseStats[Stats.Attack] / 100) + playerStatsManager.GetStatValue(Stats.Attack) / 100));
    }
}
