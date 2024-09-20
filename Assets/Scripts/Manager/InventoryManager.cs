using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameStateListener
{
    public static InventoryManager Instance { get; private set; }

    public Action<Weapon> OnShowWeaponItemInfo;
    public Action<ObjectDataSO> OnShowObjectItemInfo;

    [Header(" Elements ")]
    [SerializeField] private Transform _itemsContainer;
    [SerializeField] private ItemContainerUI _itemContainerUIPrefab;

    [SerializeField] private PlayerWeapons _playerWeapons;
    [SerializeField] private PlayerObjects _playerObjects;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

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
            instance.Configure(weapon, () => ShowItemInfoContainer(weapon));
        }

        foreach (ObjectDataSO objectData in objects)
        {
            ItemContainerUI instance = Instantiate(_itemContainerUIPrefab, _itemsContainer);
            instance.Configure(objectData, () => ShowItemInfoContainer(objectData));
        }
    }

    private void ShowItemInfoContainer(Weapon weapon)
    {
        OnShowWeaponItemInfo?.Invoke(weapon);
    }

    private void ShowItemInfoContainer(ObjectDataSO objectDataSO)
    {
        OnShowObjectItemInfo?.Invoke(objectDataSO);
    }

    private void ClearItemsChildTransform()
    {
        foreach (Transform child in _itemsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
