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
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _weaponSelectionUI;
    [SerializeField] private GameObject _stageCompleteUI;

    [Header(" Settings ")]
    private List<GameObject> panels;

    private void Awake()
    {
        panels = new List<GameObject>
        {
            _menuUI,
            _gamePlayUI,
            _shopUI,
            _waveTransitionUI,
            _gameOverUI,
            _stageCompleteUI,
            _weaponSelectionUI,
        };
    }

    public void GameStateChangedCallback(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.MENU:
                TurnOnUI(_menuUI);
                break;
            case GameStates.WEAPONSELECTION:
                TurnOnUI(_weaponSelectionUI);
                break;
            case GameStates.GAMEPLAY:
                TurnOnUI(_gamePlayUI);
                break;
            case GameStates.GAMEOVER:
                TurnOnUI(_gameOverUI);
                break;
            case GameStates.STAGECOMPLETE:
                TurnOnUI(_stageCompleteUI);
                break;
            case GameStates.WAVETRANSITION:
                TurnOnUI(_waveTransitionUI);
                break;
            case GameStates.SHOP:
                TurnOnUI(_shopUI);
                break;
        }
    }

    private void TurnOnUI(GameObject uiGameObject, bool isHidePreviousPanel = true)
    {
        if (isHidePreviousPanel)
        {
            foreach (GameObject panel in panels)
            {
                panel.SetActive(panel == uiGameObject);
            }
        }
        else
        {
            uiGameObject.SetActive(true);
        }

    }
}
