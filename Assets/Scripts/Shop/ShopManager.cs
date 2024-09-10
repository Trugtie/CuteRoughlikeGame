using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour, IGameStateListener
{
    [Header(" Elements ")]
    [SerializeField] private Transform _spawnItemContainerPosition;
    [SerializeField] private GameObject _shopItemPrefab;

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

    private void Configure()
    {
        ClearItemShop();

        for (int i = 0; i < _amoutOfSpawnItem; i++)
        {
            Instantiate(_shopItemPrefab, _spawnItemContainerPosition);
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
