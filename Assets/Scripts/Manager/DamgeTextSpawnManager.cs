using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeTextSpawnManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private DamgeText _damgeText;

    private void Start()
    {
        Enemy.OnAnyHit += SpawnText;
    }

    private void SpawnText(Vector2 targetPosition, int damge)
    {
        DamgeText damgeTextInstance = Instantiate(_damgeText, targetPosition, Quaternion.identity, transform);
        damgeTextInstance.PlayAnim(damge);
    }
}
