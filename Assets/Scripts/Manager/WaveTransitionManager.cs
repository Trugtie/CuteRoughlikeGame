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

            string buttonString;
            Action action = GetPerformActionFromStat(stat, out buttonString);

            _upgradeButtons[i].ConfigueUpgradeButton(null, randomStatString, buttonString);

            _upgradeButtons[i].Button.onClick.RemoveAllListeners();

            _upgradeButtons[i].Button.onClick.AddListener(() => action?.Invoke());
            _upgradeButtons[i].Button.onClick.AddListener(() => BonusUpgradeCallback());

        }
    }

    private void BonusUpgradeCallback()
    {
        GameManager.Instance.WaveTransitionCallback();
    }

    private Action GetPerformActionFromStat(Stats stat, out string buttonString)
    {
        float randomValue = Random.Range(1, 10);
        buttonString = $"+{randomValue}%";

        switch (stat)
        {
            case Stats.Attack:
                randomValue = Random.Range(1, 10);
                break;
            case Stats.AttackSpeed:
                randomValue = Random.Range(1, 10);
                break;
            case Stats.CriticalChance:
                randomValue = Random.Range(1, 10);
                break;
            case Stats.CriticalPercent:
                randomValue = Random.Range(1f, 2f);
                buttonString = $"+{randomValue.ToString("F2")}x";
                break;
            case Stats.MoveSpeed:
                randomValue = Random.Range(1, 10);
                break;
            case Stats.MaxHealth:
                randomValue = Random.Range(1, 5);
                buttonString = $"+{randomValue}";
                break;
            case Stats.Range:
                randomValue = Random.Range(1f, 5f);
                buttonString = $"+{randomValue.ToString("F2")}";
                break;
            case Stats.HealthRecoverySpeed:
                randomValue = Random.Range(1, 10);
                break;
            case Stats.Armor:
                randomValue = Random.Range(1, 10);
                break;
            case Stats.Luck:
                randomValue = Random.Range(1, 10);
                break;
            case Stats.Dodge:
                randomValue = Random.Range(1, 10);
                break;
            case Stats.Lifesteal:
                randomValue = Random.Range(1, 10);
                break;
            default:
                return () => Debug.Log("Invalid Action");

        }
        return () => { Debug.Log("Action Perform"); };
    }
}
