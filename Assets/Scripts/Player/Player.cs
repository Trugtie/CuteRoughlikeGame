using UnityEngine;

[RequireComponent(typeof(PlayerHealth), typeof(PlayerExp))]
public class Player : MonoBehaviour, IDamgedable
{
    public static Player Instance { get; private set; }

    [Header("Components")]
    private PlayerHealth _playerHealth;
    private PlayerExp _playerExp;

    [Header("Elements")]
    [SerializeField] private Transform _centerPoint;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _playerHealth = GetComponent<PlayerHealth>();
        _playerExp = GetComponent<PlayerExp>();
    }

    public void TakeDamge(int damge, bool isCriticalHit = false)
    {
        _playerHealth.TakeDamge(damge);
    }

    public Vector3 GetCenterPoint()
    {
        return _centerPoint.position;
    }

    public bool HasLevelUp()
    {
        return _playerExp.HasLevelUp();
    }

}
