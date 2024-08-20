using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        if (Player.Instance.HasLevelUp())
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

    public void StartGame()
    {
        SetGameState(GameStates.GAMEPLAY);
    }
}
