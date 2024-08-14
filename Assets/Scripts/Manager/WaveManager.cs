using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private int _waveDuration;

    [Header(" Waves ")]
    [SerializeField] private Wave[] _waves;
}

[Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;
}

[Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0, 100)] public Vector2 tStartEndPercent;
    public int spawnFrequence;
    public GameObject enemyPrefabs;
}
