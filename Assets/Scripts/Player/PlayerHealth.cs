using System;
using System.Threading;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour, IPlayerStatsDependency
{
    public static Action<Vector3> OnDodged;

    [Header("Elements")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;

    [Header("Settings")]
    [SerializeField] private float _minHealth;
    private float _maxHealth;
    private float _health;
    private float _armor;
    private float _lifeSteal;
    private float _dodge;
    private float _healthRecoveryDuration;
    private float _healthRecoveryTimer;
    private float _healthRecoverySpeed;

    private void Start()
    {
        Enemy.OnAnyHit += EnemyHitCallBack;
    }


    private void OnDestroy()
    {
        Enemy.OnAnyHit -= EnemyHitCallBack;
    }

    private void Update()
    {
        if (_health > _maxHealth)
            return;

        RecoveryHealth();
    }

    private void RecoveryHealth()
    {
        _healthRecoveryTimer += Time.deltaTime;

        if (_healthRecoveryTimer > _healthRecoveryDuration)
        {
            _healthRecoveryTimer = 0;

            float healthRecoveryValue = Mathf.Min(0.1f, _maxHealth - _health);
            _health += healthRecoveryValue;
            UpdateVisual();
        }
    }

    private void EnemyHitCallBack(Vector2 positionDamgeText, int damge, bool isCriticalHit)
    {
        if (_health >= _maxHealth)
            return;

        float lifeStealValue = damge * _lifeSteal / 100;
        float healthToAdd = Mathf.Min(lifeStealValue, _maxHealth - _health);

        _health += healthToAdd;

        UpdateVisual();
    }

    public void TakeDamge(int damge)
    {
        if (ShouldDodged())
            return;

        float realDamge = damge * Mathf.Clamp(1 - (_armor / 100), 0, 10000);

        realDamge = Mathf.Min(realDamge, _health);

        _health -= realDamge;

        UpdateVisual();

        if (_health <= 0)
        {
            PassAway();
        }
    }

    private bool ShouldDodged()
    {
        OnDodged?.Invoke(transform.position);
        return Random.Range(0, 100) < Mathf.Clamp(_dodge, 0, 100);
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
        _dodge = playerStatsManager.GetStatValue(Stats.Dodge);
        _healthRecoverySpeed = Mathf.Max(0.0001f, playerStatsManager.GetStatValue(Stats.HealthRecoverySpeed));
        _healthRecoveryDuration = 1f / _healthRecoverySpeed;

        UpdateVisual();
    }
}
