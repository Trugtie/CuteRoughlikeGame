using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance { get; private set; }

    private Dictionary<Stats, float> _addends = new Dictionary<Stats, float>();

    private void Awake()
    {
        if (Instance != null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddStat(Stats stat, float value)
    {
        if (_addends.ContainsKey(stat))
            _addends[stat] += value;
        else
            Debug.LogError("Not valid stat data");

        UpdatePlayerStats();
    }

    private void UpdatePlayerStats()
    {
        IEnumerable<IPlayerStatsDependency> playerStatsDependencies = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IPlayerStatsDependency>();

        foreach (IPlayerStatsDependency playerStatsDependency in playerStatsDependencies)
        {
            playerStatsDependency.UpdatePlayerStats(this);
        }
    }
}
