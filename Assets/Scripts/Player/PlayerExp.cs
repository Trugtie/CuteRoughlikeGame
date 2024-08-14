using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    public Action OnLevelUp;
    public Action OnGainExp;

    [Header("Settings")]
    [SerializeField] private int _requiredExp;
    private int _currentExp;
    private int _level = 1;

    private void Start()
    {
        Candy.OnAnyCandyCollected += CandyCollectedCallback;
    }

    private void CandyCollectedCallback(Candy candy)
    {
        GainExp(1);

        if (_currentExp >= _requiredExp)
        {
            LevelUp();
            UpdateRequiredExp();
        }
    }

    private void UpdateRequiredExp()
    {
        _requiredExp = (_level + 1) * 5;
    }

    private void GainExp(int exp)
    {
        _currentExp += exp;

        OnGainExp?.Invoke();
    }

    private void LevelUp()
    {
        _currentExp = 0;
        _level++;

        OnLevelUp?.Invoke();
    }

    public float GetExpValue()
    {
        return (float)_currentExp / _requiredExp;
    }

    public int GetCurrentLevel()
    {
        return _level;
    }
}
