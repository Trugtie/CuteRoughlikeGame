using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameStateListener
{
    [Header(" Elements ")]
    [SerializeField] private Transform _itemsContainer;
    [SerializeField] private ItemContainerUI _itemContainerUIPrefab;

    [SerializeField] private PlayerWeapons _playerWeapons;
    [SerializeField] private PlayerObjects _playerObjects;

    public void GameStateChangedCallback(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.SHOP:
                Configure();
                break;
        }
    }

    private void Configure()
    {
        ClearItemsChildTransform();

        Weapon[] weapons = _playerWeapons.GetWeapons();
        ObjectDataSO[] objects = _playerObjects.Objects.ToArray();

        foreach (Weapon weapon in weapons)
        {
            ItemContainerUI instance = Instantiate(_itemContainerUIPrefab, _itemsContainer);
            instance.Configure(ColorPalleteSystem.Instance.GetLevelColor(weapon.Level), weapon.WeaponDataSO.WeaponSprite);
        }

        foreach (ObjectDataSO objectData in objects)
        {
            ItemContainerUI instance = Instantiate(_itemContainerUIPrefab, _itemsContainer);
            instance.Configure(ColorPalleteSystem.Instance.GetLevelColor(objectData.Rality), objectData.Icon);
        }

    }

    private void ClearItemsChildTransform()
    {
        foreach (Transform child in _itemsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
