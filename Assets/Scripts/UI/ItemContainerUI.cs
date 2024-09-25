using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainerUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Button _itemButton;

    public void Configure(Weapon weapon, Action onButtonClickAction)
    {
        _backgroundImage.color = ColorPalleteSystem.Instance.GetLevelColor(weapon.Level);
        _itemIcon.sprite = weapon.WeaponDataSO.WeaponSprite;

        _itemButton.onClick.RemoveAllListeners();
        _itemButton.onClick.AddListener(() => onButtonClickAction?.Invoke());
    }
    public void Configure(ObjectDataSO objectData, Action onButtonClickAction)
    {
        _backgroundImage.color = ColorPalleteSystem.Instance.GetLevelColor(objectData.Rality);
        _itemIcon.sprite = objectData.Icon;

        _itemButton.onClick.RemoveAllListeners();
        _itemButton.onClick.AddListener(() => onButtonClickAction?.Invoke());
    }
}
