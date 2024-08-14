using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private Wave[] _waves;
}

[Serializable]
public struct Wave
{
    public string name;
    public WaveSegment[] segments;
}

[Serializable]
public struct WaveSegment
{
    public GameObject enemyPrefabs;
}
