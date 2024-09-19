using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainerUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _itemIcon;

    public void Configure(Color backgroundColor, Sprite icon)
    {
        _backgroundImage.color = backgroundColor;
        _itemIcon.sprite = icon;
    }
}
