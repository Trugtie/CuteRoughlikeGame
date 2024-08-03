using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DropManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Candy _candyPrefab;

    private void Start()
    {
        Enemy.OnAnyPassAway += EnemyPassAwayCallBack;
    }

    private void OnDestroy()
    {
        Enemy.OnAnyPassAway -= EnemyPassAwayCallBack;
    }

    private void EnemyPassAwayCallBack(Vector2 vector)
    {
        Instantiate(_candyPrefab, vector, Quaternion.identity, transform);
    }
}
