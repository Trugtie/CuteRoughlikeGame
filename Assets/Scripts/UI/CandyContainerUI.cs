using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CandyContainerUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI _candyText;

    private void Start()
    {
        UpdateVisual(CurrencyManager.Instance.Currency);
        CurrencyManager.Instance.OnUpdatedCurrency += OnUpdatedCurrencyCallback;
    }

    private void OnDestroy()
    {
        CurrencyManager.Instance.OnUpdatedCurrency += OnUpdatedCurrencyCallback;
    }

    private void OnUpdatedCurrencyCallback()
    {
        UpdateVisual(CurrencyManager.Instance.Currency);
    }


    private void UpdateVisual(int candyCurrency)
    {
        _candyText.SetText(candyCurrency.ToString());
    }
}
