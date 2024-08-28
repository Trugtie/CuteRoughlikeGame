using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IPlayerStatsDependency
{
    [Header("Elements")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;

    [Header("Settings")]
    [SerializeField] private float _minHealth;
    private float _maxHealth;
    private float _health;
    private float _armor;
    private float _lifeSteal;

    private void Start()
    {
        Enemy.OnAnyHit += EnemyHitCallBack;
    }

    private void OnDestroy()
    {
        Enemy.OnAnyHit -= EnemyHitCallBack;
    }

    private void EnemyHitCallBack(Vector2 positionDamgeText, int damge, bool isCriticalHit)
    {
        if (_health >= _maxHealth)
            return;

        float lifeStealValue = damge * _lifeSteal / 100;
        float healthToAdd = Mathf.Min(lifeStealValue, _maxHealth - _health);

        _health += healthToAdd;

        Debug.Log("healtoadd: " + healthToAdd);

        UpdateVisual();
    }

    public void TakeDamge(int damge)
    {
        float realDamge = damge * Mathf.Clamp(1 - (_armor / 100), 0, 10000);

        realDamge = Mathf.Min(realDamge, _health);

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

        float healthValue = _health / _maxHealth;
        _healthBar.value = healthValue;

        _healthText.SetText($"{(int)_health} / {_maxHealth}");
    }

    public void UpdatePlayerStats(PlayerStatsManager playerStatsManager)
    {
        float healthStatValue = playerStatsManager.GetStatValue(Stats.MaxHealth);
        _maxHealth = (int)healthStatValue;
        _maxHealth = Mathf.Max(_maxHealth, _minHealth);

        _health = _maxHealth;
        _armor = playerStatsManager.GetStatValue(Stats.Armor);
        _lifeSteal = playerStatsManager.GetStatValue(Stats.Lifesteal);

        UpdateVisual();
    }
}
