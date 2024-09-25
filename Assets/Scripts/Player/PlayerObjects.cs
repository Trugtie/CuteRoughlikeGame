using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStatsManager))]
public class PlayerObjects : MonoBehaviour
{
    [Header(" Elements ")]
    private PlayerStatsManager _playerStatsManager;

    [field: SerializeField] public List<ObjectDataSO> Objects { get; private set; }

    private void Awake()
    {
        _playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    public void AddObject(ObjectDataSO objectDataSO)
    {
        Objects.Add(objectDataSO);
        _playerStatsManager.AddObject(objectDataSO.BaseStats);
    }

    public void RemoveObject(ObjectDataSO objectDataSO)
    {
        Objects.Remove(objectDataSO);
        _playerStatsManager.RemoveObject(objectDataSO.BaseStats);
    }
}
