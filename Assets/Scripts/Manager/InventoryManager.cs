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
    [SerializeField] private Transform _pauseItemsContainer;

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

    private void Start()
    {
        ShopItemContainerUI.OnAnyPurchaseItem += OnAnyPurchaseItemCallback;
        ItemInfoSlideUI.OnAnyRecycleObject += OnAnyRecycleObjectCallback;
        ItemInfoSlideUI.OnAnyRecycleWeapon += OnAnyRecycleWeaponCallback;
        WeaponMerger.Instance.OnWeaponMerge += OnWeaponMergeCallback;
        GameManager.Instance.OnPauseGame += OnPauseGameCallback;
    }

    private void OnDestroy()
    {
        ShopItemContainerUI.OnAnyPurchaseItem -= OnAnyPurchaseItemCallback;
        ItemInfoSlideUI.OnAnyRecycleObject -= OnAnyRecycleObjectCallback;
        ItemInfoSlideUI.OnAnyRecycleWeapon -= OnAnyRecycleWeaponCallback;
        WeaponMerger.Instance.OnWeaponMerge -= OnWeaponMergeCallback;
        GameManager.Instance.OnPauseGame += OnPauseGameCallback;
    }

    private void OnPauseGameCallback()
    {
        Configure();
    }

    private void OnWeaponMergeCallback(Weapon weapon)
    {
        Configure();
    }


    private void OnAnyPurchaseItemCallback(ShopItemContainerUI uI, int arg2)
    {
        Configure();
    }

    private void OnAnyRecycleWeaponCallback(Weapon weapon)
    {
        _playerWeapons.RemovePlayerWeapon(weapon.WeaponAssignIndexPosition);
        CurrencyManager.Instance.AddCurrency(weapon.GetRecycleCurrencyWeapon());
        Configure();
    }

    private void OnAnyRecycleObjectCallback(ObjectDataSO objectDataSO)
    {
        _playerObjects.RemoveObject(objectDataSO);
        CurrencyManager.Instance.AddCurrency(objectDataSO.RecyclePrice);
        Configure();
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
        ClearItemsChildTransform(_itemsContainer);
        ClearItemsChildTransform(_pauseItemsContainer);

        Weapon[] weapons = _playerWeapons.GetWeapons();
        ObjectDataSO[] objects = _playerObjects.Objects.ToArray();

        foreach (Weapon weapon in weapons)
        {
            ItemContainerUI instance = Instantiate(_itemContainerUIPrefab, _itemsContainer);
            instance.Configure(weapon, () => ShowItemInfoContainer(weapon));

            ItemContainerUI pauseInstance = Instantiate(_itemContainerUIPrefab, _pauseItemsContainer);
            pauseInstance.Configure(weapon, null);
        }

        foreach (ObjectDataSO objectData in objects)
        {
            ItemContainerUI instance = Instantiate(_itemContainerUIPrefab, _itemsContainer);
            instance.Configure(objectData, () => ShowItemInfoContainer(objectData));

            ItemContainerUI pauseInstance = Instantiate(_itemContainerUIPrefab, _pauseItemsContainer);
            pauseInstance.Configure(objectData, null);
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

    private void ClearItemsChildTransform(Transform containerTransform)
    {
        foreach (Transform child in containerTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
