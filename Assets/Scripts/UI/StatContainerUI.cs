using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatContainerUI : MonoBehaviour, IPlayerStatsDependency
{
    [Header(" Elements ")]
    [SerializeField] private Transform _statsValueContainerParent;

    public void UpdatePlayerStats(PlayerStatsManager playerStatsManager)
    {
        UpdateVisual(playerStatsManager);
    }

    private void UpdateVisual(PlayerStatsManager playerStatsManager)
    {
        int childStatContainerIndex = 0;
        foreach (Stats stat in Enum.GetValues(typeof(Stats)))
        {
            StatsValueContainerUI statsValueContainerUI = _statsValueContainerParent.GetChild(childStatContainerIndex).GetComponent<StatsValueContainerUI>();

            Sprite statIcon = ResourceManager.GetStatIcon(stat);
            string statName = Enums.FormatEnumString(stat);
            string statValue = playerStatsManager.GetStatValue(stat).ToString("F0");

            statsValueContainerUI.Configure(statIcon, statName, statValue);
            childStatContainerIndex++;
        }

        for (int i = childStatContainerIndex; i < _statsValueContainerParent.childCount; i++)
        {
            _statsValueContainerParent.GetChild(childStatContainerIndex).gameObject.SetActive(false);
        }
    }
}
