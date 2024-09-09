using System;
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

    [Header(" Settings ")]
    private float _baseValue = -1;

    public void Configure(Sprite icon, string statName, float value, bool isColorizeValue = false)
    {
        _icon.sprite = icon;
        _nameText.SetText(statName);

        if (_baseValue == -1)
            _baseValue = value;

        if (isColorizeValue)
            ColorizeValue(value);

        _valueText.SetText(value.ToString("F0"));
    }

    private void ColorizeValue(float value)
    {
        Color baseColor = Color.white;

        if (value < _baseValue)
            baseColor = Color.red;
        else if (value > _baseValue)
            baseColor = Color.green;

        _valueText.color = baseColor;
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
