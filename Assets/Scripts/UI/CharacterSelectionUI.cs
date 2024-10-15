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

    private void Awake()
    {
        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        InitializeCharacterScrollContent();
        SelectCharacterCallback(CharacterSelectionManager.Instance.SelectedIndex);
        _characterInfoUI.PurchaseButton.onClick.RemoveAllListeners();
        _characterInfoUI.PurchaseButton.onClick.AddListener(() => PurchaseCallback());
    }

    private void InitializeCharacterScrollContent()
    {
        CharacterDataSO[] charactersDataSO = CharacterSelectionManager.Instance.CharactersDataSO;
        List<bool> unlockedList = CharacterSelectionManager.Instance.UnlockedList;

        if (charactersDataSO.Length <= 0)
        {
            Debug.LogError("None characters!");
            return;
        }

        ClearCharacterButtonScroll();

        for (int i = 0; i < charactersDataSO.Length; i++)
        {
            CharacterButtonUI characterButtonInstance = Instantiate(_characterButtonUIPrefab, _characterScrollContentParent);
            characterButtonInstance.Configure(charactersDataSO[i], i, unlockedList[i]);

            characterButtonInstance.CharacterButton.onClick.RemoveAllListeners();
            characterButtonInstance.CharacterButton.onClick.AddListener(() => SelectCharacterCallback(characterButtonInstance.Index));
        }
    }

    private void SelectCharacterCallback(int index)
    {
        CharacterDataSO characterSelectedDataSO = CharacterSelectionManager.Instance.SelectCharacter(index);

        _characterInfoUI.PurchaseButton.interactable = true;
        _middleCharacterIcon.sprite = CharacterSelectionManager.Instance.CharactersDataSO[index].CharacterSprite;

        bool isSelectedCharacterUnlocked = CharacterSelectionManager.Instance.UnlockedList[index];
        _characterInfoUI.Configure(characterSelectedDataSO, isSelectedCharacterUnlocked);

        if (isSelectedCharacterUnlocked)
            return;

        bool canBuy = CurrencyManager.Instance.PremiumCurrency >= characterSelectedDataSO.PurchasePrice;
        _characterInfoUI.PurchaseButton.interactable = canBuy;


    }

    private void PurchaseCallback()
    {
        CharacterDataSO characterPurchaseDataSO = CharacterSelectionManager.Instance.PurchaseCharacter();

        _characterInfoUI.Configure(characterPurchaseDataSO, true);
        _characterScrollContentParent.GetChild(CharacterSelectionManager.Instance.SelectedIndex).GetComponent<CharacterButtonUI>().Unlock();
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
