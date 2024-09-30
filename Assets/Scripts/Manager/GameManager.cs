using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Action OnPauseGame;
    public Action OnResumeGame;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        SetGameState(GameStates.MENU);
    }

    public void WaveTransitionCallback()
    {
        if (Player.Instance.HasLevelUp() || WaveTransitionManager.Instance.HasCollectedChest())
        {
            SetGameState(GameStates.WAVETRANSITION);
        }
        else
        {
            SetGameState(GameStates.SHOP);
        }
    }

    private void SetGameState(GameStates gameState)
    {
        IEnumerable<IGameStateListener> gameStateListeners = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();

        foreach (IGameStateListener gameStateListener in gameStateListeners)
        {
            gameStateListener.GameStateChangedCallback(gameState);
        }
    }

    public void SetState(GameStates gameState)
    {
        SetGameState(gameState);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        OnPauseGame?.Invoke();
        Debug.Log(OnPauseGame?.GetInvocationList().Length);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        OnResumeGame?.Invoke();
    }
}
