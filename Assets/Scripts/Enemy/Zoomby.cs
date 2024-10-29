using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Zoomby : Enemy
{
    [Header(" Health Bar ")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;

    private void Start()
    {
        base.Start();

        _healthBar.gameObject.SetActive(false);

        onSpawnSequenceCompleted += onSpawnSequenceCompletedCallBack;
        OnAnyHit += OnAnyHitCallback;
    }

    private void OnDestroy()
    {
        onSpawnSequenceCompleted += onSpawnSequenceCompletedCallBack;
        OnAnyHit -= OnAnyHitCallback;
    }

    private void OnAnyHitCallback(Vector2 position, int damge, bool isCriticalHit)
    {
        UpdateVisual();
    }

    private void onSpawnSequenceCompletedCallBack()
    {
        _healthBar.gameObject.SetActive(true);
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        float healthValue = (float)_health / _maxHealth;

        _healthBar.value = healthValue;

        _healthText.SetText($"{(int)_health} / {_maxHealth}");
    }

}
