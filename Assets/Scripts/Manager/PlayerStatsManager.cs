using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private CharacterDataSO _characterDataSO;

    public static PlayerStatsManager Instance { get; private set; }

    private Dictionary<Stats, float> _addends = new Dictionary<Stats, float>();
    private Dictionary<Stats, float> _baseStats;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitData();
    }

    private void Start()
    {
        UpdatePlayerStats();
    }

    private void InitData()
    {
        _baseStats = _characterDataSO.BaseStats;

        foreach (KeyValuePair<Stats, float> keyValuePair in _baseStats)
        {
            _addends.Add(keyValuePair.Key, 0);
        }
    }

    public void AddStat(Stats stat, float value)
    {
        if (_addends.ContainsKey(stat))
            _addends[stat] += value;
        else
            Debug.LogError("Not valid stat data");

        UpdatePlayerStats();
    }

    public float GetStatValue(Stats stat)
    {
        return _baseStats[stat] + _addends[stat];
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
