using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [Header("Components")]
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    public void TakeDamge(int damge)
    {
        _playerHealth.TakeDamge(damge);
    }
}
