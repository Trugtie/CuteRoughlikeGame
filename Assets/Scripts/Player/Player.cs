using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour, IDamgedable
{
    [Header("Components")]
    private PlayerHealth _playerHealth;

    [Header("Elements")]
    [SerializeField] private Transform _centerPoint;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    public void TakeDamge(int damge)
    {
        _playerHealth.TakeDamge(damge);
    }

    public Vector3 GetCenterPoint()
    {
        return _centerPoint.position;
    }
}
