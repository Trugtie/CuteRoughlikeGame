using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WeaponSelectionManager : MonoBehaviour, IGameStateListener
{
    [Header(" Elements ")]
    [SerializeField] private Transform _weaponSelectionContainerTransform;
    [SerializeField] private WeaponSelectionButtonUI _weaponSelectionButtonUI;
    [SerializeField] private PlayerWeapons _playerWeapons;
    private WeaponDataSO _seletectedWeapon;
    private int _seletectedWeaponLevel;

    [Header(" Data ")]
    [SerializeField] private WeaponDataSO[] _weaponDatasSO;

    public void GameStateChangedCallback(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.GAMEPLAY:

                if (_seletectedWeapon == null)
                    return;

                _playerWeapons.AddWeapon(_seletectedWeapon, _seletectedWeaponLevel);

                _seletectedWeapon = null;
                _seletectedWeaponLevel = 0;

                break;
            case GameStates.WEAPONSELECTION:
                ConfigueWeaponSelection();
                break;
        }
    }

    [Button]
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

        int level = Random.Range(0, 2);

        weaponSelectionButtonInstance.Configure(weaponData, level);

        weaponSelectionButtonInstance.WeaponSelectButton.onClick.RemoveAllListeners();
        weaponSelectionButtonInstance.WeaponSelectButton.onClick.AddListener(() => SelectionWeaponCallback(weaponSelectionButtonInstance, weaponData, level));
    }

    private void SelectionWeaponCallback(WeaponSelectionButtonUI selectedButton, WeaponDataSO weaponDataSO, int level)
    {
        _seletectedWeapon = weaponDataSO;
        _seletectedWeaponLevel = level;

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
