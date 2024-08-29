using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour, IGameStateListener
{
    [Header(" Elements ")]
    [SerializeField] private Transform _weaponSelectionContainerTransform;
    [SerializeField] private WeaponSelectionButtonUI _weaponSelectionButtonUI;

    public void GameStateChangedCallback(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.WEAPONSELECTION:
                ConfigueWeaponSelection();
                break;
        }
    }

    private void ConfigueWeaponSelection()
    {
        ClearWeaponSelectionContainer();

        for (int i = 0; i < 3; i++)
        {
            GenerateWeaponSelectionButtonUI();
        }
    }

    private void GenerateWeaponSelectionButtonUI()
    {
        WeaponSelectionButtonUI weaponSelectionButtonInstance = Instantiate(_weaponSelectionButtonUI, _weaponSelectionContainerTransform);
    }

    private void ClearWeaponSelectionContainer()
    {
        foreach (Transform child in _weaponSelectionContainerTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
