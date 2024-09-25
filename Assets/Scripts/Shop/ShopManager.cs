using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour, IGameStateListener
{
    public Action OnUpdateReroll;

    public static ShopManager Instance { get; private set; }

    [Header(" Elements ")]
    [SerializeField] private Transform _spawnItemContainerPosition;
    [SerializeField] private ShopItemContainerUI _shopItemPrefab;
    [SerializeField] private PlayerWeapons _playerWeapons;
    [SerializeField] private PlayerObjects playerObjects;

    [Header(" Settings ")]
    [SerializeField] private int _amoutOfSpawnItem;

    [field: SerializeField] public int RerollPrice { get; private set; }

    [Header(" Debugs ")]
    [SerializeField] private bool isNotRandomWeapon;
    [MinValue(0), MaxValue(6)]
    [SerializeField] private int _minWeaponToAdd;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ShopItemContainerUI.OnAnyPurchaseItem += OnAnyPurchaseItemCallback;
    }

    private void OnDestroy()
    {
        ShopItemContainerUI.OnAnyPurchaseItem -= OnAnyPurchaseItemCallback;
    }

    private void OnAnyPurchaseItemCallback(ShopItemContainerUI itemContainer, int level)
    {
        bool isPurchaseWeapon = itemContainer.WeaponDataSO != null;

        if (isPurchaseWeapon)
            TryPurchaseWeapon(itemContainer, level);
        else
            PurchaseObject(itemContainer);
    }

    private void PurchaseObject(ShopItemContainerUI itemContainer)
    {
        ObjectDataSO objectDataSO = itemContainer.ObjectDataSO;
        playerObjects.AddObject(objectDataSO);

        CurrencyManager.Instance.UseCurrency(objectDataSO.Price);

        Destroy(itemContainer.gameObject);
    }

    private void TryPurchaseWeapon(ShopItemContainerUI itemContainer, int level)
    {
        if (_playerWeapons.TryAddWeapon(itemContainer.WeaponDataSO, level))
        {
            int price = WeaponCalculator.GetCalculatedWeaponPrice(itemContainer.WeaponDataSO, level);
            CurrencyManager.Instance.UseCurrency(price);

            Destroy(itemContainer.gameObject);
        }
    }

    public void GameStateChangedCallback(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.SHOP:
                Configure();
                OnUpdateReroll?.Invoke();
                break;
        }
    }

    [Button]
    private void Configure()
    {
        List<GameObject> itemsToDestroy = new List<GameObject>();

        foreach (Transform child in _spawnItemContainerPosition)
        {
            ShopItemContainerUI shopItem = child.GetComponent<ShopItemContainerUI>();

            if (!shopItem.IsLock)
                itemsToDestroy.Add(shopItem.gameObject);
        }

        while (itemsToDestroy.Count > 0)
        {
            Transform itemTransform = itemsToDestroy[0].transform;
            itemTransform.SetParent(null);
            Destroy(itemTransform.gameObject);
            itemsToDestroy.RemoveAt(0);
        }

        int _amoutOfSpawnItemToAdd = _amoutOfSpawnItem - _spawnItemContainerPosition.childCount;

        int weaponItemToAdd = Random.Range(Mathf.Min(_minWeaponToAdd, _amoutOfSpawnItemToAdd), _amoutOfSpawnItemToAdd);

        if (isNotRandomWeapon)
            weaponItemToAdd = Mathf.Min(_minWeaponToAdd, _amoutOfSpawnItemToAdd);

        int objectItemToAdd = _amoutOfSpawnItemToAdd - weaponItemToAdd;

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

    public void RerollShopItem()
    {
        CurrencyManager.Instance.UseCurrency(RerollPrice);
        Configure();
    }
}
