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

    [field: SerializeField] public Button PurchaseButton { get; private set; }

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
    public WeaponDataSO WeaponDataSO { get; private set; }
    public ObjectDataSO ObjectDataSO { get; private set; }

    public bool IsLock { get; private set; }

    public void Configure(WeaponDataSO weaponDataSO, int level)
    {
        WeaponDataSO = weaponDataSO;
        _level = level;

        _itemNameText.text = weaponDataSO.name + " (lv " + (level + 1) + ")";
        _itemNameText.color = ColorPalleteSystem.Instance.GetLevelColor(level);
        _itemIcon.sprite = weaponDataSO.WeaponSprite;

        int price = WeaponCalculator.GetCalculatedWeaponPrice(weaponDataSO, level);
        _priceText.text = price.ToString();

        PurchaseButton.interactable = CurrencyManager.Instance.HasEnoughCurrency(price);

        PurchaseButton.onClick.AddListener(PurchaseItem);
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
        _priceText.text = objectDataSO.Price.ToString();

        PurchaseButton.interactable = CurrencyManager.Instance.HasEnoughCurrency(objectDataSO.Price);
        PurchaseButton.onClick.AddListener(PurchaseItem);

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
