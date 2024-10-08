using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashContainerUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI _cashText;

    private void Start()
    {
        CurrencyManager.Instance.OnUpdatedCurrency += CashUpdateCallback;
        UpdateVisual(CurrencyManager.Instance.PremiumCurrency);
    }

    private void OnDestroy()
    {
        CurrencyManager.Instance.OnUpdatedCurrency -= CashUpdateCallback;
    }

    private void CashUpdateCallback()
    {
        UpdateVisual(CurrencyManager.Instance.PremiumCurrency);
    }

    private void UpdateVisual(int amount)
    {
        _cashText.SetText(amount.ToString());
    }
}
