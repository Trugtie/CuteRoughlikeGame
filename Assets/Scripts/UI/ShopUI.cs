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
    [SerializeField] private RectTransform _overlayRectTransform;
    [SerializeField] private RectTransform _statsContainerRect;

    private float _overlayAlpha;
    private Vector2 _statsContainerShowPos;
    private Vector2 _statsContainerHidePos;

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
        _overlayAlpha = _overlayRectTransform.GetComponent<Image>().color.a;

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

        _statsContainerShowPos = _statsContainerRect.anchoredPosition;
        _statsContainerHidePos = _statsContainerShowPos + Vector2.left * width;

        _statsContainerRect.anchoredPosition = _statsContainerHidePos;

        _statsContainerRect.gameObject.SetActive(false);
        _overlayRectTransform.gameObject.SetActive(false);
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
        if (isShow)
        {
            _statsContainerRect.gameObject.SetActive(isShow);
            _overlayRectTransform.gameObject.SetActive(isShow);

            _overlayRectTransform.GetComponent<Image>().raycastTarget = true;

            LeanTween.cancel(_statsContainerRect);
            LeanTween.move(_statsContainerRect, _statsContainerShowPos, .3f)
                .setEase(LeanTweenType.easeInCubic);

            LeanTween.cancel(_overlayRectTransform);
            LeanTween.alpha(_overlayRectTransform, _overlayAlpha, 0.3f).setRecursive(false);
        }
        else
        {
            _overlayRectTransform.GetComponent<Image>().raycastTarget = false;

            LeanTween.cancel(_statsContainerRect);
            LeanTween.move(_statsContainerRect, _statsContainerHidePos, .3f)
                .setEase(LeanTweenType.easeOutCubic)
                .setOnComplete(() => _statsContainerRect.gameObject.SetActive(isShow));

            LeanTween.cancel(_overlayRectTransform);
            LeanTween.alpha(_overlayRectTransform, 0f, 0.3f)
                .setRecursive(false)
                .setOnComplete(() => _overlayRectTransform.gameObject.SetActive(isShow));
        }
    }
}
