using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData/NewCharacterData", order = 0)]
public class CharacterDataSO : ScriptableObject
{
    [field: SerializeField] public string CharacterName { get; private set; }
    [field: SerializeField] public Sprite CharacterSprite { get; private set; }
    [field: SerializeField] public int PurchasePrice { get; private set; }

    [HorizontalLine]
    [SerializeField] private float attack;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float criticalChance;
    [SerializeField] private float criticalPercent;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float range;
    [SerializeField] private float healthRecoverySpeed;
    [SerializeField] private float armor;
    [SerializeField] private float luck;
    [SerializeField] private float dodge;
    [SerializeField] private float lifesteal;

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
                { Stats.MoveSpeed,moveSpeed},
                { Stats.MaxHealth,maxHealth},
                { Stats.Range,range},
                { Stats.HealthRecoverySpeed,healthRecoverySpeed},
                { Stats.Armor,armor},
                { Stats.Luck, luck},
                {Stats.Dodge, dodge},
                {Stats.Lifesteal, lifesteal},
            };
        }

        private set { }
    }

    public Dictionary<Stats, float> NonNeutralStats
    {
        get
        {
            Dictionary<Stats, float> nonNeutralStats = new Dictionary<Stats, float>();

            foreach (KeyValuePair<Stats, float> kvp in BaseStats)
            {
                if (kvp.Value != 0)
                    nonNeutralStats.Add(kvp.Key, kvp.Value);
            }

            return nonNeutralStats;
        }

        private set { }
    }
}
