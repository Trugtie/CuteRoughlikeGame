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
    }

    private void OnDestroy()
    {
        Candy.OnAnyCandyCollected -= CandyCollectedCallback;
    }

    private void CandyCollectedCallback(Candy candy)
    {
        AddCurrency(1);
    }

    [Button]
    private void Add500Coin()
    {
        AddCurrency(500);
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
        OnUpdatedCurrency?.Invoke();
    }

    public void UseCurrency(int amount)
    {
        AddCurrency(-amount);
    }

    public bool HasEnoughCurrency(int price)
    {
        return Currency >= price;
    }
}
