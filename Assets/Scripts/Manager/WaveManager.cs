using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour, IGameStateListener
{
    public Action<int, int> OnStartWave;
    public Action<int> OnTimerCountDown;
    public Action OnWaveComplete;

    [Header(" Elements ")]
    [SerializeField] private Player _player;

    [Header(" Settings ")]
    [SerializeField] private int _waveDuration;
    private float _timer;
    private List<float> _localCounters = new List<float>();
    private bool _isTimerOn;
    private int _currentWaveIndex;

    [Header(" Waves ")]
    [SerializeField] private Wave[] _waves;

    private void Update()
    {
        if (!_isTimerOn) return;

        if (_timer < _waveDuration)
        {
            ManageCurrentWave();

            int countDownTimer = (int)(_waveDuration - _timer);
            OnTimerCountDown?.Invoke(countDownTimer);
        }
        else
            StartWaveTransition();
    }

    private void StartWave(int waveIndex)
    {
        _localCounters.Clear();
        _timer = 0;

        for (int i = 0; i < _waves[waveIndex].segments.Count; i++)
            _localCounters.Add(0);

        _isTimerOn = true;

        OnStartWave?.Invoke(waveIndex, _waves.Length);
    }

    private void ManageCurrentWave()
    {
        Wave wave = _waves[_currentWaveIndex];

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

                if (currentSegment.spawnOnlyOne)
                    _localCounters[i] += Mathf.Infinity;
            }
        }

        _timer += Time.deltaTime;
    }

    private void StartWaveTransition()
    {
        _isTimerOn = false;

        DestroyAllEnemies();

        _currentWaveIndex++;
        if (_currentWaveIndex >= _waves.Length)
        {
            GameManager.Instance.SetState(GameStates.STAGECOMPLETE);
            OnWaveComplete?.Invoke();
        }
        else
            GameManager.Instance.WaveTransitionCallback();
    }

    private void StartNextWave()
    {
        StartWave(_currentWaveIndex);
    }

    private void DestroyAllEnemies()
    {
        foreach (Enemy enemy in transform.GetComponentsInChildren<Enemy>())
        {
            enemy.PassAwayAfterWave();
        }
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 spawnDirection = UnityEngine.Random.onUnitSphere;
        Vector2 offset = spawnDirection.normalized * UnityEngine.Random.Range(6, 10);
        Vector2 targetPosition = (Vector2)_player.transform.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -Constants.arenaSize.x, Constants.arenaSize.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -Constants.arenaSize.y, Constants.arenaSize.y);

        return targetPosition;
    }

    public void GameStateChangedCallback(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.GAMEPLAY:
                StartNextWave();
                break;
            case GameStates.GAMEOVER:
                _isTimerOn = false;
                DestroyAllEnemies();
                break;
        }
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
    public bool spawnOnlyOne;
}
