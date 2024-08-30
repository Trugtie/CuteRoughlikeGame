using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WeaponSelectionManager : MonoBehaviour, IGameStateListener
{
    [Header(" Elements ")]
    [SerializeField] private Transform _weaponSelectionContainerTransform;
    [SerializeField] private WeaponSelectionButtonUI _weaponSelectionButtonUI;

    [Header(" Data ")]
    [SerializeField] private WeaponDataSO[] _weaponDatasSO;

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

        WeaponDataSO weaponData = _weaponDatasSO[Random.Range(0, _weaponDatasSO.Length)];

        weaponSelectionButtonInstance.Configure(weaponData);

        weaponSelectionButtonInstance.WeaponSelectButton.onClick.RemoveAllListeners();
        weaponSelectionButtonInstance.WeaponSelectButton.onClick.AddListener(() => SelectionWeaponCallback(weaponSelectionButtonInstance, weaponData));
    }

    private void SelectionWeaponCallback(WeaponSelectionButtonUI selectedButton, WeaponDataSO weaponDataSO)
    {
        foreach (WeaponSelectionButtonUI weaponSelectButton in _weaponSelectionContainerTransform.GetComponentsInChildren<WeaponSelectionButtonUI>())
        {
            if (selectedButton == weaponSelectButton)
                weaponSelectButton.Select();
            else
                weaponSelectButton.DeSelect();

        }

        Debug.Log("Select " + weaponDataSO.name);
    }

    private void ClearWeaponSelectionContainer()
    {
        foreach (Transform child in _weaponSelectionContainerTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
