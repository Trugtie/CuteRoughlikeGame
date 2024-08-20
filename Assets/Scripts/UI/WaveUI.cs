using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _timerText;

    private void Start()
    {
        _waveManager.OnStartWave += UpdateWave;
        _waveManager.OnTimerCountDown += UpdateTimer;
        _waveManager.OnWaveComplete += WaveComplete;
    }

    private void OnDestroy()
    {
        _waveManager.OnStartWave -= UpdateWave;
        _waveManager.OnTimerCountDown -= UpdateTimer;
        _waveManager.OnWaveComplete -= WaveComplete;
    }

    private void UpdateWave(int waveIndex, int waveMaxLenght)
    {
        _waveText.text = $"Wave {waveIndex + 1}";
    }

    private void WaveComplete()
    {
        _waveText.text = "Stage Completed!";
        _timerText.text = "";
    }

    private void UpdateTimer(int timer)
    {
        _timerText.text = timer.ToString();
    }
}
