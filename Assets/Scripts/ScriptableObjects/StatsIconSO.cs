using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat Icon", menuName = "Stat Icon/New Stat Icon", order = 0)]
public class StatsIconSO : ScriptableObject
{
    [field: SerializeField] public StatIconData[] StatsIconDatas { get; private set; }
}

[Serializable]
public struct StatIconData
{
    public Stats Stats;
    public Sprite Icon;
}
