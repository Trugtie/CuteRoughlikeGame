using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _backButton;
    [SerializeField] private Transform _characterScrollContentParent;
    [SerializeField] private CharacterButtonUI _characterButtonUIPrefab;
    [SerializeField] private Image _middleCharacterIcon;
    [SerializeField] private CharacterInfoUI _characterInfoUI;

    private CharacterDataSO[] _charactersDataSO;
    private int _selectedIndex;
    private List<bool> _unlockedList = new List<bool>();

    private void Awake()
    {
        _charactersDataSO = ResourceManager.Characters;

        for (int i = 0; i < _charactersDataSO.Length; i++)
            _unlockedList.Add(i == 0);

        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        InitializeCharacterScrollContent();

        _characterInfoUI.Configure(_charactersDataSO[0], true);
        _characterInfoUI.PurchaseButton.onClick.RemoveAllListeners();
        _characterInfoUI.PurchaseButton.onClick.AddListener(() => PurchaseCallback());
    }

    private void InitializeCharacterScrollContent()
    {
        if (_charactersDataSO.Length <= 0)
        {
            Debug.LogError("None characters!");
            return;
        }

        ClearCharacterButtonScroll();

        for (int i = 0; i < _charactersDataSO.Length; i++)
        {
            CharacterButtonUI characterButtonInstance = Instantiate(_characterButtonUIPrefab, _characterScrollContentParent);
            characterButtonInstance.Configure(_charactersDataSO[i], i, _unlockedList[i]);

            characterButtonInstance.CharacterButton.onClick.RemoveAllListeners();
            characterButtonInstance.CharacterButton.onClick.AddListener(() => SelectCharacterCallback(characterButtonInstance.Index));
        }
    }

    private void SelectCharacterCallback(int index)
    {
        _selectedIndex = index;
        _characterInfoUI.PurchaseButton.interactable = true;
        _middleCharacterIcon.sprite = _charactersDataSO[index].CharacterSprite;
        _characterInfoUI.Configure(_charactersDataSO[index], _unlockedList[_selectedIndex]);

        if (_unlockedList[_selectedIndex])
            return;

        bool canBuy = CurrencyManager.Instance.PremiumCurrency >= _charactersDataSO[index].PurchasePrice;
        _characterInfoUI.PurchaseButton.interactable = canBuy;
    }

    private void PurchaseCallback()
    {
        int price = _charactersDataSO[_selectedIndex].PurchasePrice;
        CurrencyManager.Instance.UsePremiumCurrency(price);
        _unlockedList[_selectedIndex] = true;
        _characterInfoUI.Configure(_charactersDataSO[_selectedIndex], true);
        _characterScrollContentParent.GetChild(_selectedIndex).GetComponent<CharacterButtonUI>().Unlock();
    }

    private void ClearCharacterButtonScroll()
    {
        foreach (Transform child in _characterScrollContentParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
