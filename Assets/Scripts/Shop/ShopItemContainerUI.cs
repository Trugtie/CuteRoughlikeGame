using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemContainerUI : MonoBehaviour
{
    public static Action<ShopItemContainerUI, int> OnAnyPurchaseItem;

    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _priceText;

    [SerializeField] private Button _purchaseButton;

    [Header(" Stats ")]
    [SerializeField] private Transform _statContainerTransform;
    [SerializeField] private StatsValueContainerUI _statsValueFrameUI;

    [Header("Colorable Containers")]
    [SerializeField] private Image[] _backgroundColorContainer;
    [SerializeField] private Outline _outline;

    [Header(" Lock Elements ")]
    [SerializeField] private Button _lockButton;
    [SerializeField] private Sprite _lockSprite, _unlockSprite;

    [Header("Purchase")]
    private int _level;
    private int _price;
    public WeaponDataSO WeaponDataSO { get; private set; }
    public ObjectDataSO ObjectDataSO { get; private set; }

    public bool IsLock { get; private set; }

    private void Start()
    {
        CurrencyManager.Instance.OnUpdatedCurrency += OnUpdatedCurrencyCallback;
    }

    private void OnDestroy()
    {
        CurrencyManager.Instance.OnUpdatedCurrency -= OnUpdatedCurrencyCallback;
    }

    private void OnUpdatedCurrencyCallback()
    {
        _purchaseButton.interactable = CurrencyManager.Instance.HasEnoughCurrency(_price);
    }

    public void Configure(WeaponDataSO weaponDataSO, int level)
    {
        WeaponDataSO = weaponDataSO;
        _level = level;

        _itemNameText.text = weaponDataSO.name + " (lv " + (level + 1) + ")";
        _itemNameText.color = ColorPalleteSystem.Instance.GetLevelColor(level);
        _itemIcon.sprite = weaponDataSO.WeaponSprite;

        _price = WeaponCalculator.GetCalculatedWeaponPrice(weaponDataSO, level);
        _priceText.text = _price.ToString();

        _purchaseButton.interactable = CurrencyManager.Instance.HasEnoughCurrency(_price);

        _purchaseButton.onClick.AddListener(PurchaseItem);
        _lockButton.onClick.AddListener(LockCallback);

        foreach (Image background in _backgroundColorContainer)
            background.color = ColorPalleteSystem.Instance.GetLevelColor(level);

        _outline.effectColor = ColorPalleteSystem.Instance.GetLevelOulineColor(level);

        Dictionary<Stats, float> calculatedDictionary = WeaponCalculator.GetCalculatedWeaponStats(weaponDataSO, level);
        ConfigureStats(calculatedDictionary);
    }

    public void Configure(ObjectDataSO objectDataSO)
    {
        ObjectDataSO = objectDataSO;
        _itemNameText.text = objectDataSO.name;
        _itemNameText.color = ColorPalleteSystem.Instance.GetLevelColor(objectDataSO.Rality);
        _itemIcon.sprite = objectDataSO.Icon;

        _price = objectDataSO.Price;
        _priceText.text = _price.ToString();

        _purchaseButton.interactable = CurrencyManager.Instance.HasEnoughCurrency(_price);
        _purchaseButton.onClick.AddListener(PurchaseItem);

        _lockButton.onClick.AddListener(LockCallback);

        foreach (Image background in _backgroundColorContainer)
            background.color = ColorPalleteSystem.Instance.GetLevelColor(objectDataSO.Rality);

        _outline.effectColor = ColorPalleteSystem.Instance.GetLevelOulineColor(objectDataSO.Rality);

        Dictionary<Stats, float> objectBaseStats = objectDataSO.BaseStats;
        ConfigureStats(objectBaseStats);
    }

    private void PurchaseItem()
    {
        OnAnyPurchaseItem?.Invoke(this, _level);
    }

    private void ConfigureStats(Dictionary<Stats, float> stats)
    {
        StatContainerManager.GenerateStatContainerWithFrame(stats, _statsValueFrameUI, _statContainerTransform);
    }

    private void LockCallback()
    {
        IsLock = !IsLock;
        UpdateLockVisual();
    }

    private void UpdateLockVisual()
    {
        _lockButton.image.sprite = IsLock ? _lockSprite : _unlockSprite;
    }
}
