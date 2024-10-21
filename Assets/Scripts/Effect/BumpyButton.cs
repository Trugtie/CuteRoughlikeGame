using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BumpyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [Header(" Elements ")]
    private Button _button;

    [Header(" Settings ")]
    [SerializeField] private Vector2 _scaleVector;
    [SerializeField] private float _durationTime;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_button.interactable)
            return;

        LeanTween.cancel(_button.gameObject);
        LeanTween.scale(_button.gameObject, _scaleVector, _durationTime)
            .setEase(LeanTweenType.easeOutElastic)
            .setIgnoreTimeScale(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(_button.gameObject);
        LeanTween.scale(_button.gameObject, Vector2.one, _durationTime)
            .setEase(LeanTweenType.easeOutElastic)
            .setIgnoreTimeScale(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        LeanTween.cancel(_button.gameObject);
        LeanTween.scale(_button.gameObject, Vector2.one, _durationTime)
            .setEase(LeanTweenType.easeOutElastic)
            .setIgnoreTimeScale(true);
    }
}
