using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDataSO", menuName = "ObjectDataSO/New ObjectDataSO", order = 0)]
public class ObjectDataSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int RecyclePrice { get; private set; }

    [field: Range(0, 3)]
    [field: SerializeField] public int Rality { get; private set; }

    public StatsData[] Stats;

    public Dictionary<Stats, float> BaseStats
    {
        get
        {
            Dictionary<Stats, float> resultDictionary = new Dictionary<Stats, float>();

            foreach (StatsData data in Stats)
            {
                resultDictionary.Add(data.stat, data.value);
            }

            return resultDictionary;
        }

        private set { }
    }
}

[Serializable]
public struct StatsData
{
    public Stats stat;
    public float value;
}
