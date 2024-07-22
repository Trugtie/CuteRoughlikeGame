using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;

    [Header("Settings")]
    [SerializeField] private int _maxHealth;
    private int _health;

    private void Awake()
    {
        _health = _maxHealth;
        UpdateVisual();
    }

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
        SceneManager.LoadScene(0);
    }

    private void UpdateVisual()
    {

        float healthValue = (float)_health / _maxHealth;
        _healthBar.value = healthValue;

        _healthText.SetText($"{_health} / {_maxHealth}");
    }
}
