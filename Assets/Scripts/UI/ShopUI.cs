using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class ShopUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Button _startGameButton;

    [Header(" Reroll ")]
    [SerializeField] private Button _rerollButton;
    [SerializeField] private TextMeshProUGUI _rerollPriceText;

    [Header(" Stat Container ")]
    [SerializeField] private Button _statsButton;
    [SerializeField] private EventTrigger _overlayTrigger;
    [SerializeField] private RectTransform _statsContainerRect;

    private void OnEnable()
    {
        StartCoroutine(ConfigureStasContainerRoutine());
    }

    private void Start()
    {
        _startGameButton.onClick.AddListener(() => GameManager.Instance.SetState(GameStates.GAMEPLAY));

        _rerollButton.onClick.AddListener(() => ShopManager.Instance.RerollShopItem());

        _statsButton.onClick.AddListener(() => ToggleStatsContainer(true));

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnOverlayButtonClick((PointerEventData)data); });
        _overlayTrigger.triggers.Add(entry);

        ToggleStatsContainer(false);

        ShopManager.Instance.OnUpdateReroll += UpdateRerollVisual;
        CurrencyManager.Instance.OnUpdatedCurrency += UpdateRerollVisual;
    }

    private IEnumerator ConfigureStasContainerRoutine()
    {
        yield return null;
        ConfigureStatsContainerUI();
    }

    private void OnDestroy()
    {
        ShopManager.Instance.OnUpdateReroll -= UpdateRerollVisual;
        CurrencyManager.Instance.OnUpdatedCurrency -= UpdateRerollVisual;
    }

    private void ConfigureStatsContainerUI()
    {
        float width = Screen.width / (4 * _statsContainerRect.lossyScale.x);
        _statsContainerRect.offsetMax = new Vector2(width, _statsContainerRect.offsetMax.y);
    }

    public void OnOverlayButtonClick(PointerEventData data)
    {
        ToggleStatsContainer(false);
    }

    private void UpdateRerollVisual()
    {
        int rerollPrice = ShopManager.Instance.RerollPrice;

        _rerollPriceText.text = rerollPrice.ToString();
        _rerollButton.interactable = CurrencyManager.Instance.HasEnoughCurrency(rerollPrice);
    }

    private void ToggleStatsContainer(bool isShow)
    {
        _statsContainerRect.gameObject.SetActive(isShow);
        _overlayTrigger.gameObject.SetActive(isShow);
    }

}
