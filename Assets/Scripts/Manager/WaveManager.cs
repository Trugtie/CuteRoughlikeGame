using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Player _player;

    [Header(" Settings ")]
    [SerializeField] private int _waveDuration;
    private float _timer;
    private List<float> _localCounters = new List<float>();
    private bool _isTimerOn;

    [Header(" Waves ")]
    [SerializeField] private Wave[] _waves;

    private void Start()
    {
        StartWave(0);
    }

    private void Update()
    {
        if (!_isTimerOn) return;

        if (_timer < _waveDuration)
            ManageCurrentWave();
    }

    private void StartWave(int waveIndex)
    {
        _localCounters.Clear();
        _timer = 0;

        for (int i = 0; i < _waves[waveIndex].segments.Count; i++)
            _localCounters.Add(1);

        _isTimerOn = true;
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

            float spawnDelay = 1.0f / currentSegment.spawnFrequence;

            if (timeSinceSegmentStart / spawnDelay > _localCounters[i])
            {
                Instantiate(currentSegment.enemyPrefab, GetSpawnPosition(), Quaternion.identity, transform);
                _localCounters[i]++;
            }
        }

        _timer += Time.deltaTime;
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 spawnDirection = UnityEngine.Random.onUnitSphere;
        Vector2 offset = spawnDirection.normalized * UnityEngine.Random.Range(6, 10);
        Vector2 targetPosition = (Vector2)_player.transform.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -19, 19);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -9, 16);

        return targetPosition;
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
