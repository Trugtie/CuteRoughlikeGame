using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IPlayerStatsDependency
{
    [Header("Elements")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;

    [Header("Settings")]
    [SerializeField] private int _baseMaxHealth;
    private int _maxHealth;
    private int _health;

    public void TakeDamge(int damge)
    {
        int realDamge = Mathf.Min(damge, _health);

        _health -= realDamge;

        UpdateVisual();

        if (_health <= 0)
        {
            PassAway();
        }
    }

    private void PassAway()
    {
        Debug.Log("Player Pass Away");
        GameManager.Instance.SetState(GameStates.GAMEOVER);
    }

    private void UpdateVisual()
    {

        float healthValue = (float)_health / _maxHealth;
        _healthBar.value = healthValue;

        _healthText.SetText($"{_health} / {_maxHealth}");
    }

    public void UpdatePlayerStats(PlayerStatsManager playerStatsManager)
    {
        float addendHealth = playerStatsManager.GetAddendStatValue(Stats.MaxHealth);
        _maxHealth = _baseMaxHealth + (int)addendHealth;
        _maxHealth = Mathf.Max(_maxHealth, 1);

        _health = _maxHealth;
        UpdateVisual();
    }
}
