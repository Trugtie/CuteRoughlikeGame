using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveTransitionManager : MonoBehaviour, IGameStateListener
{
    [Header(" Elements ")]
    [SerializeField] private Button[] _upgradeButtons;

    public void GameStateChangedCallback(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.WAVETRANSITION:
                ConfigueUpgradeButtons();
                break;
        }
    }

    private void ConfigueUpgradeButtons()
    {
        for (int i = 0; i < _upgradeButtons.Length; i++)
        {
            _upgradeButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Upgrade " + i;
        }
    }
}
