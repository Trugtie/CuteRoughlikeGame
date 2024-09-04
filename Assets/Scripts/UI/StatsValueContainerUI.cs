using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsValueContainerUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _valueText;

    public void Configure(Sprite icon, string statName, string value)
    {
        _icon.sprite = icon;
        _nameText.SetText(statName);
        _valueText.SetText(value);
    }

    public float GetFontSize()
    {
        return _nameText.fontSize;
    }

    public void SetFontSize(float size)
    {
        _nameText.fontSizeMax = size;
        _valueText.fontSizeMax = size;
    }
}
