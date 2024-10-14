using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI _characterNameText;
    [SerializeField] private TextMeshProUGUI _characterPriceText;
    [SerializeField] private Transform _priceContainer;
    [SerializeField] private Transform _statsParent;

    [field: SerializeField] public Button PurchaseButton { get; private set; }

    public void Configure(CharacterDataSO characterData, bool isUnLocked)
    {
        _characterNameText.SetText(characterData.CharacterName);
        _characterPriceText.SetText(characterData.PurchasePrice.ToString());
        _priceContainer.gameObject.SetActive(!isUnLocked);

        StatContainerManager.GenerateStatContainer(characterData.NonNeutralStats, _statsParent);
    }
}
