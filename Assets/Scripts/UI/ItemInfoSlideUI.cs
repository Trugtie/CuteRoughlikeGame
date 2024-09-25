using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoSlideUI : MonoBehaviour
{
    public static Action<ObjectDataSO> OnAnyRecycleObject;
    public static Action<Weapon> OnAnyRecycleWeapon;

    [Header(" Elements ")]
    [SerializeField] private Image _icon;
    [SerializeField] private Image _backgroundColor;

    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _recylePrice;

    [SerializeField] private Transform _statsValueContainerParentTransform;
    [SerializeField] private StatsValueContainerUI _statsValueContainerTemplate;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _mergeButton;
    [SerializeField] private Button _recycleButton;

    public void Configure(Weapon weapon, Action onCloseButtonAction)
    {
        _icon.sprite = weapon.WeaponDataSO.WeaponSprite;
        _backgroundColor.color = ColorPalleteSystem.Instance.GetLevelColor(weapon.Level);

        _itemName.SetText(weapon.WeaponDataSO.WeaponName);
        _itemName.color = ColorPalleteSystem.Instance.GetLevelColor(weapon.Level);

        float recyclePrice = WeaponCalculator.GetCalculatedWeaponRecylePrice(weapon.WeaponDataSO, weapon.Level);
        _recylePrice.SetText(recyclePrice.ToString());

        StatContainerManager.GenerateStatContainerWithFrame(
            WeaponCalculator.GetCalculatedWeaponStats(weapon.WeaponDataSO, weapon.Level),
            _statsValueContainerTemplate,
            _statsValueContainerParentTransform
            );

        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(() => onCloseButtonAction?.Invoke());

        _recycleButton.onClick.RemoveAllListeners();
        _recycleButton.onClick.AddListener(() => OnAnyRecycleWeapon?.Invoke(weapon));

        _mergeButton.gameObject.SetActive(true);
    }

    public void Configure(ObjectDataSO objectDataSO, Action onCloseButtonAction)
    {
        _icon.sprite = objectDataSO.Icon;
        _backgroundColor.color = ColorPalleteSystem.Instance.GetLevelColor(objectDataSO.Rality);

        _itemName.SetText(objectDataSO.Name);
        _itemName.color = ColorPalleteSystem.Instance.GetLevelColor(objectDataSO.Rality);
        _recylePrice.SetText(objectDataSO.RecyclePrice.ToString());

        StatContainerManager.GenerateStatContainerWithFrame(objectDataSO.BaseStats, _statsValueContainerTemplate, _statsValueContainerParentTransform);

        _closeButton.onClick.RemoveAllListeners();
        _closeButton.onClick.AddListener(() => onCloseButtonAction?.Invoke());

        _recycleButton.onClick.RemoveAllListeners();
        _recycleButton.onClick.AddListener(() => OnAnyRecycleObject?.Invoke(objectDataSO));

        _mergeButton.gameObject.SetActive(false);
    }
}
