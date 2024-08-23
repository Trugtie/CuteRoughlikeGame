using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance { get; private set; }

    private Dictionary<Stats, float> _statsData = new Dictionary<Stats, float>();

    private void Awake()
    {
        if (Instance != null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddStat(Stats stat, float value)
    {
        if (_statsData.ContainsKey(stat))
            _statsData[stat] += value;
        else
            Debug.LogError("Not valid stat data");
    }
}
