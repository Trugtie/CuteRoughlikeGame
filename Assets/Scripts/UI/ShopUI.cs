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
    [SerializeField] private RectTransform _statsContainerRect;

    private Vector2 _statsContainerShowPos;
    private Vector2 _statsContainerHidePos;

    [Header(" Inventory Container ")]
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private RectTransform _inventoryContainerRect;

    private Vector2 _inventoryContainerShowPos;
    private Vector2 _inventoryContainerHidePos;

    [Header(" Sliding Overlay UI")]
    [SerializeField] private EventTrigger _overlayTrigger;
    [SerializeField] private RectTransform _overlayRectTransform;
    private float _overlayAlpha;

    private void OnEnable()
    {
        StartCoroutine(ConfigureSlideContainerRoutine());
    }

    private void Start()
    {
        _startGameButton.onClick.AddListener(() => GameManager.Instance.SetState(GameStates.GAMEPLAY));

        _rerollButton.onClick.AddListener(() => ShopManager.Instance.RerollShopItem());

        _statsButton.onClick.AddListener(() => ShowSlideContainer(_statsContainerRect, _statsContainerShowPos));
        _inventoryButton.onClick.AddListener(() => ShowSlideContainer(_inventoryContainerRect, _inventoryContainerShowPos));

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnOverlayButtonClick((PointerEventData)data); });
        _overlayTrigger.triggers.Add(entry);
        _overlayAlpha = _overlayRectTransform.GetComponent<Image>().color.a;

        ShopManager.Instance.OnUpdateReroll += UpdateRerollVisual;
        CurrencyManager.Instance.OnUpdatedCurrency += UpdateRerollVisual;
    }

    private IEnumerator ConfigureSlideContainerRoutine()
    {
        yield return null;
        ConfigureStatsContainerUI(_statsContainerRect, ref _statsContainerShowPos, ref _statsContainerHidePos, true);
        ConfigureStatsContainerUI(_inventoryContainerRect, ref _inventoryContainerShowPos, ref _inventoryContainerHidePos, false);
    }

    private void OnDestroy()
    {
        ShopManager.Instance.OnUpdateReroll -= UpdateRerollVisual;
        CurrencyManager.Instance.OnUpdatedCurrency -= UpdateRerollVisual;
    }

    private void ConfigureStatsContainerUI(RectTransform containerRect, ref Vector2 showPos, ref Vector2 hidePos, bool isSlideLeft)
    {
        float width = Screen.width / (4 * containerRect.lossyScale.x);

        containerRect.sizeDelta = new Vector2(width, containerRect.offsetMax.y);

        showPos = containerRect.anchoredPosition;

        if (isSlideLeft)
            hidePos = showPos + Vector2.left * width;
        else
            hidePos = showPos + Vector2.right * width;

        containerRect.anchoredPosition = hidePos;
        containerRect.gameObject.SetActive(false);

        _overlayRectTransform.gameObject.SetActive(false);
    }

    public void OnOverlayButtonClick(PointerEventData data)
    {
        HideSlideContainer(_statsContainerRect, _statsContainerHidePos);
        HideSlideContainer(_inventoryContainerRect, _inventoryContainerHidePos);
    }

    private void UpdateRerollVisual()
    {
        int rerollPrice = ShopManager.Instance.RerollPrice;

        _rerollPriceText.text = rerollPrice.ToString();
        _rerollButton.interactable = CurrencyManager.Instance.HasEnoughCurrency(rerollPrice);
    }

    private void ShowSlideContainer(RectTransform containerRect, Vector2 showPos)
    {
        containerRect.gameObject.SetActive(true);
        _overlayRectTransform.gameObject.SetActive(true);

        _overlayRectTransform.GetComponent<Image>().raycastTarget = true;

        LeanTween.cancel(containerRect);
        LeanTween.move(containerRect, showPos, .3f)
            .setEase(LeanTweenType.easeInCubic);

        LeanTween.cancel(_overlayRectTransform);
        LeanTween.alpha(_overlayRectTransform, _overlayAlpha, 0.3f).setRecursive(false);
    }

    private void HideSlideContainer(RectTransform containerRect, Vector2 hidePos)
    {
        if (!containerRect.gameObject.activeSelf) return;

        _overlayRectTransform.GetComponent<Image>().raycastTarget = false;

        LeanTween.cancel(containerRect);
        LeanTween.move(containerRect, hidePos, .3f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(() => containerRect.gameObject.SetActive(false));

        LeanTween.cancel(_overlayRectTransform);
        LeanTween.alpha(_overlayRectTransform, 0f, 0.3f)
            .setRecursive(false)
            .setOnComplete(() => _overlayRectTransform.gameObject.SetActive(false));
    }
}
