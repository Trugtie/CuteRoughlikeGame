using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class HopToTap : MonoBehaviour, IPointerDownHandler
{
    [Header(" Elements ")]
    private RectTransform _rectTransform;

    [Header(" Settings ")]
    private Vector2 _initPosition;
    [SerializeField] private float _delayTime;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _initPosition = _rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Hop();
    }

    private void Hop()
    {
        float targetY = _initPosition.y + Screen.height / 50;

        LeanTween.cancel(gameObject);
        LeanTween.moveY(_rectTransform, targetY, _delayTime)
            .setEase(LeanTweenType.punch)
            .setIgnoreTimeScale(true);
    }
}
