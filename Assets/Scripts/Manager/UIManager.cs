using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameStateListener
{
    [Header(" Elements ")]
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _gamePlayUI;
    [SerializeField] private GameObject _shopUI;
    [SerializeField] private GameObject _waveTransitionUI;

    public void GameStateChangedCallback(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.GAMEPLAY:
                TriggerUI(_gamePlayUI);
                TriggerUI(_menuUI);
                break;
        }
    }

    private void TriggerUI(GameObject uiGameObject)
    {
        if (uiGameObject.active)
            uiGameObject.SetActive(false);
        else
            uiGameObject.SetActive(true);
    }
}
