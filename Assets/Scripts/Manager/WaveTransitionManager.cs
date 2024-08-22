using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header(" Elements ")]
    [SerializeField] private UpgradeButtonUI[] _upgradeButtons;

    public void GameStateChangedCallback(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.WAVETRANSITION:
                ConfigueUpgradeButtons();
                break;
        }
    }

    [Button]
    private void ConfigueUpgradeButtons()
    {
        for (int i = 0; i < _upgradeButtons.Length; i++)
        {
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stats)).Length);
            Stats stat = (Stats)Enum.GetValues(typeof(Stats)).GetValue(randomIndex);

            string randomStatString = Enums.FormatEnumString(stat);

            _upgradeButtons[i].ConfigueUpgradeButton(null, randomStatString, Random.Range(0, 100));

            _upgradeButtons[i].Button.onClick.RemoveAllListeners();

            Action action = GetPerformActionFromStat(stat);

            _upgradeButtons[i].Button.onClick.AddListener(() => action?.Invoke());

        }
    }

    private Action GetPerformActionFromStat(Stats stat)
    {
        switch (stat)
        {
            case Stats.Attack:
                return () => Debug.Log("Applied Attack");
            case Stats.AttackSpeed:
                return () => Debug.Log("Applied Attack Speed");
            default:
                return () => Debug.Log("Invalid Action");

        }
    }
}
