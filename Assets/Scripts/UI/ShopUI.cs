using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class ShopUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _startGameBtn;

    [Header(" Reroll ")]
    [SerializeField] private Button _rerollButton;
    [SerializeField] private TextMeshProUGUI _rerollPriceText;

    private void Start()
    {
        ShopManager.Instance.OnUpdateReroll += UpdateRerollVisual;
        CurrencyManager.Instance.OnUpdatedCurrency += UpdateRerollVisual;

        _startGameBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SetState(GameStates.GAMEPLAY);
        });

        _rerollButton.onClick.AddListener(() =>
        {
            ShopManager.Instance.RerollShopItem();
        });
    }

    private void OnDestroy()
    {
        ShopManager.Instance.OnUpdateReroll -= UpdateRerollVisual;
        CurrencyManager.Instance.OnUpdatedCurrency -= UpdateRerollVisual;
    }

    private void UpdateRerollVisual()
    {
        int rerollPrice = ShopManager.Instance.RerollPrice;

        _rerollPriceText.text = rerollPrice.ToString();
        _rerollButton.interactable = CurrencyManager.Instance.HasEnoughCurrency(rerollPrice);
    }

}
