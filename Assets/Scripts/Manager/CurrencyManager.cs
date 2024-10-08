using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public Action OnUpdatedCurrency;

    public static CurrencyManager Instance { get; private set; }

    [field: SerializeField] public int Currency { get; private set; }
    [field: SerializeField] public int PremiumCurrency { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Candy.OnAnyCandyCollected += CandyCollectedCallback;
        Cash.OnAnyCashCollected += CashCollectedCallback;
    }

    private void OnDestroy()
    {
        Candy.OnAnyCandyCollected -= CandyCollectedCallback;
        Cash.OnAnyCashCollected -= CashCollectedCallback;
    }

    private void CashCollectedCallback(Cash cash)
    {
        AddPremiumCurrency(1);
    }

    private void CandyCollectedCallback(Candy candy)
    {
        AddCurrency(1);
    }

    [Button]
    private void Add500Candy()
    {
        AddCurrency(500);
    }

    [Button]
    private void Add500Cash()
    {
        AddPremiumCurrency(500);
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
        OnUpdatedCurrency?.Invoke();
    }

    public void AddPremiumCurrency(int amount)
    {
        PremiumCurrency += amount;
        OnUpdatedCurrency?.Invoke();
    }

    public void UseCurrency(int amount)
    {
        AddCurrency(-amount);
    }

    public void UsePremiumCurrency(int amount)
    {
        AddPremiumCurrency(-amount);
    }

    public bool HasEnoughCurrency(int price)
    {
        return Currency >= price;
    }

    public bool HasEnoughPremiumCurrency(int price)
    {
        return PremiumCurrency >= price;
    }
}
