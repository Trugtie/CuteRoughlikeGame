using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponData/New Weapon Data", order = 1)]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public string WeaponName { get; private set; }
    [field: SerializeField] public Sprite WeaponSprite { get; private set; }
    [field: SerializeField] public int PurchasePrice { get; private set; }

    [field: SerializeField] public Weapon Prefab { get; private set; }

    [HorizontalLine]
    [SerializeField] private float attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float criticalChance;
    [SerializeField] private float criticalPercent;

    public Dictionary<Stats, float> BaseStats
    {
        get
        {
            return new Dictionary<Stats, float>
            {
                { Stats.Attack,attack},
                { Stats.AttackSpeed,attackSpeed},
                { Stats.CriticalChance,criticalChance},
                { Stats.CriticalPercent,criticalPercent},
            };
        }

        private set
        {

        }
    }

    public float GetStatValue(Stats stat)
    {
        foreach (KeyValuePair<Stats, float> kvp in BaseStats)
        {
            if (kvp.Key == stat)
                return kvp.Value;
        }

        Debug.LogError("Stat is not conxist");
        return 0;
    }
}
