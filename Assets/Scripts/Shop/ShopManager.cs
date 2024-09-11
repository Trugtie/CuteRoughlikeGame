using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour, IGameStateListener
{
    [Header(" Elements ")]
    [SerializeField] private Transform _spawnItemContainerPosition;
    [SerializeField] private ShopItemContainerUI _shopItemPrefab;

    [Header(" Settings ")]
    [SerializeField] private int _amoutOfSpawnItem;

    public void GameStateChangedCallback(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.SHOP:
                Configure();
                break;
        }
    }

    [Button]
    private void Configure()
    {
        ClearItemShop();

        int weaponItemToAdd = Random.Range(Mathf.Min(2, _amoutOfSpawnItem), _amoutOfSpawnItem);
        int objectItemToAdd = _amoutOfSpawnItem - weaponItemToAdd;

        for (int i = 0; i < weaponItemToAdd; i++)
        {
            ShopItemContainerUI weaponItem = Instantiate(_shopItemPrefab, _spawnItemContainerPosition);

            WeaponDataSO weaponDataSO = ResourceManager.GetRandomWeaponData();
            int level = Random.Range(0, 4);

            weaponItem.Configure(weaponDataSO, level);
        }

        for (int i = 0; i < objectItemToAdd; i++)
        {
            ShopItemContainerUI objectItem = Instantiate(_shopItemPrefab, _spawnItemContainerPosition);

            ObjectDataSO objectDataSO = ResourceManager.GetRandomObjectData();

            objectItem.Configure(objectDataSO);
        }

    }

    private void ClearItemShop()
    {
        foreach (Transform child in _spawnItemContainerPosition)
        {
            Destroy(child.gameObject);
        }
    }
}
