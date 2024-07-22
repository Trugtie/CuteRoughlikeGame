using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _maxHealth;
    private int _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamge(int damge)
    {
        int realDamge = Mathf.Min(damge, _health);

        _health -= realDamge;

        if (_health <= 0)
        {
            PassAway();
        }
    }

    private void PassAway()
    {
        Debug.Log("Player Pass Away");
    }
}
