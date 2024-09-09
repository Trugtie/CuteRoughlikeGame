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
    public static WaveTransitionManager Instance { get; private set; }

    [Header(" Elements ")]
    [SerializeField] private UpgradeButtonUI[] _upgradeButtons;
    [SerializeField] private Transform _upgradeButtonsParent;
    [SerializeField] private PlayerObjects _playerObjects;

    [Header(" Settings ")]
    private int _chestCollectedCounts;

    [Header("Chest Relate Settings")]
    [SerializeField] private ChestObjectContainerUI _chestObjectContainerPrefab;
    [SerializeField] private Transform _chestObjectContainerParent;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Chest.OnAnyChestCollected += CheckCollectedCallback;
    }

    private void OnDestroy()
    {
        Chest.OnAnyChestCollected -= CheckCollectedCallback;
    }

    public void GameStateChangedCallback(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.WAVETRANSITION:
                TryOpenChest();
                break;
        }
    }

    private void TryOpenChest()
    {
        ClearAllChest();

        if (_chestCollectedCounts > 0)
            ShowChest();
        else
            ConfigueUpgradeButtons();
    }

    private void ShowChest()
    {
        _chestCollectedCounts--;

        ObjectDataSO[] objects = ResourceManager.Objects;
        ObjectDataSO randomObject = objects[Random.Range(0, objects.Length)];

        ChestObjectContainerUI chestObjectContainerUIInstance = Instantiate(_chestObjectContainerPrefab, _chestObjectContainerParent);
        chestObjectContainerUIInstance.Configure(randomObject);
        chestObjectContainerUIInstance.TakeButton.onClick.AddListener(() => TakeObjectCallback(randomObject));
        chestObjectContainerUIInstance.RecycleButton.onClick.AddListener(() => RecycleObjectCallback(randomObject));

        _chestObjectContainerParent.gameObject.SetActive(true);
        _upgradeButtonsParent.gameObject.SetActive(false);
    }

    private void RecycleObjectCallback(ObjectDataSO objectToRecycle)
    {
        CurrencyManager.Instance.AddCurrency(objectToRecycle.RecyclePrice);
        TryOpenChest();
    }

    private void TakeObjectCallback(ObjectDataSO objectToTake)
    {
        _playerObjects.AddObject(objectToTake);
        TryOpenChest();
    }

    private void ClearAllChest()
    {
        foreach (Transform child in _chestObjectContainerParent)
        {
            Destroy(child.gameObject);
        }
    }

    [Button]
    private void ConfigueUpgradeButtons()
    {
        _upgradeButtonsParent.gameObject.SetActive(true);

        for (int i = 0; i < _upgradeButtons.Length; i++)
        {
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stats)).Length);
            Stats stat = (Stats)Enum.GetValues(typeof(Stats)).GetValue(randomIndex);

            Sprite icon = ResourceManager.GetStatIcon(stat);
            string randomStatString = Enums.FormatEnumString(stat);
            string buttonString;

            Action action = GetPerformActionFromStat(stat, out buttonString);

            _upgradeButtons[i].ConfigueUpgradeButton(icon, randomStatString, buttonString);

            _upgradeButtons[i].Button.onClick.RemoveAllListeners();

            _upgradeButtons[i].Button.onClick.AddListener(() => action?.Invoke());
            _upgradeButtons[i].Button.onClick.AddListener(() => BonusUpgradeCallback());

        }
    }

    private void BonusUpgradeCallback()
    {
        GameManager.Instance.WaveTransitionCallback();
    }

    private void CheckCollectedCallback(Chest chest)
    {
        _chestCollectedCounts++;
    }

    private Action GetPerformActionFromStat(Stats stat, out string buttonString)
    {
        float randomValue;
        buttonString = null;

        switch (stat)
        {
            case Stats.Attack:
                randomValue = Random.Range(1, 10);
                buttonString = $"+{randomValue}%";
                break;
            case Stats.AttackSpeed:
                randomValue = Random.Range(1, 10);
                buttonString = $"+{randomValue}%";
                break;
            case Stats.CriticalChance:
                randomValue = Random.Range(1, 10);
                buttonString = $"+{randomValue}%";
                break;
            case Stats.CriticalPercent:
                randomValue = Random.Range(1f, 2f);
                buttonString = $"+{randomValue.ToString("F2")}x";
                break;
            case Stats.MoveSpeed:
                randomValue = Random.Range(1, 10);
                buttonString = $"+{randomValue}%";
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
                buttonString = $"+{randomValue}%";
                break;
            case Stats.Armor:
                randomValue = Random.Range(1, 10);
                buttonString = $"+{randomValue}%";
                break;
            case Stats.Luck:
                randomValue = Random.Range(1, 10);
                buttonString = $"+{randomValue}%";
                break;
            case Stats.Dodge:
                randomValue = Random.Range(1, 10);
                buttonString = $"+{randomValue}%";
                break;
            case Stats.Lifesteal:
                randomValue = Random.Range(1, 10);
                buttonString = $"+{randomValue}%";
                break;
            default:
                return () => Debug.Log("Invalid Action");
        }

        return () => PlayerStatsManager.Instance.AddStat(stat, randomValue);
    }

    public bool HasCollectedChest()
    {
        return _chestCollectedCounts > 0;
    }
}
