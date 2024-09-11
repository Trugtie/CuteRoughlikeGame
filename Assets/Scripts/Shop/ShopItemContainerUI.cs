using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemContainerUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private StatsValueContainerUI _statsValueFrameUI;

    [field: SerializeField] public Button PurchaseButton { get; private set; }

    [Header(" Stats ")]
    [SerializeField] private Transform _statContainerTransform;

    [Header("Colorable Containers")]
    [SerializeField] private Image[] _backgroundColorContainer;
    [SerializeField] private Outline _outline;

    public void Configure(WeaponDataSO weaponDataSO, int level)
    {
        _itemNameText.text = weaponDataSO.name + " (lv " + (level + 1) + ")";
        _itemNameText.color = ColorPalleteSystem.Instance.GetLevelColor(level);
        _itemIcon.sprite = weaponDataSO.WeaponSprite;
        _priceText.text = WeaponCalculator.GetCalculatedWeaponPrice(weaponDataSO, level).ToString();

        foreach (Image background in _backgroundColorContainer)
            background.color = ColorPalleteSystem.Instance.GetLevelColor(level);

        _outline.effectColor = ColorPalleteSystem.Instance.GetLevelOulineColor(level);

        Dictionary<Stats, float> calculatedDictionary = WeaponCalculator.GetCalculatedWeaponStats(weaponDataSO, level);
        ConfigureStats(calculatedDictionary);
    }

    public void Configure(ObjectDataSO objectDataSO)
    {
        _itemNameText.text = objectDataSO.name;
        _itemNameText.color = ColorPalleteSystem.Instance.GetLevelColor(objectDataSO.Rality);
        _itemIcon.sprite = objectDataSO.Icon;
        _priceText.text = objectDataSO.Price.ToString();

        foreach (Image background in _backgroundColorContainer)
            background.color = ColorPalleteSystem.Instance.GetLevelColor(objectDataSO.Rality);

        _outline.effectColor = ColorPalleteSystem.Instance.GetLevelOulineColor(objectDataSO.Rality);

        Dictionary<Stats, float> objectBaseStats = objectDataSO.BaseStats;
        ConfigureStats(objectBaseStats);
    }

    private void ConfigureStats(Dictionary<Stats, float> stats)
    {
        StatContainerManager.GenerateStatContainerWithFrame(stats, _statsValueFrameUI, _statContainerTransform);
    }
}
