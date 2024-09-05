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

    private void Start()
    {
        foreach (ObjectDataSO data in Objects)
        {
            _playerStatsManager.AddObject(data.BaseStats);
        }
    }
}
