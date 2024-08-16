using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private int _waveDuration;
    private float _timer;
    private List<float> _localCounters = new List<float>();

    [Header(" Waves ")]
    [SerializeField] private Wave[] _waves;

    private void Start()
    {
        _localCounters.Add(1);
    }

    private void Update()
    {
        if (_timer < _waveDuration)
            ManageCurrentWave();
    }

    private void ManageCurrentWave()
    {
        Wave wave = _waves[0];

        for (int i = 0; i < wave.segments.Count; i++)
        {
            WaveSegment currentSegment = wave.segments[i];

            float tStart = currentSegment.tStartEndPercent.x / 100 * _waveDuration;
            float tEnd = currentSegment.tStartEndPercent.y / 100 * _waveDuration;

            bool isOutSizeTRange = _timer < tStart || _timer > tEnd;

            if (isOutSizeTRange)
                continue;

            float timeSinceSegmentStart = _timer - tStart;

            float spawmDelay = 1 / currentSegment.spawnFrequence;

            if (timeSinceSegmentStart / spawmDelay > _localCounters[i])
            {
                Instantiate(currentSegment.enemyPrefab, Vector3.zero, Quaternion.identity, transform);
                _localCounters[i]++;
            }
        }

        _timer += Time.deltaTime;
    }
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
    public GameObject enemyPrefab;
}
