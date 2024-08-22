using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    [Button]
    private void ConfigueUpgradeButtons()
    {
        for (int i = 0; i < _upgradeButtons.Length; i++)
        {
            int randomIndex = Random.Range(0, Enum.GetValues(typeof(Stats)).Length);
            Stats stat = (Stats)Enum.GetValues(typeof(Stats)).GetValue(randomIndex);

            string randomStatString = Enums.FormatEnumString(stat);

            _upgradeButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = randomStatString;
        }
    }
}
