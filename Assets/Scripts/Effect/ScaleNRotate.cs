using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ScaleNRotate : MonoBehaviour, IPointerDownHandler
{
    [Header(" Elements ")]
    private RectTransform _rectTransform;

    [Header(" Settings ")]
    [SerializeField] private float _scaleUnit;
    [SerializeField] private float _rotateAngleUnit;
    [SerializeField] private float _delayTime;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DoScaleAndRotate();
    }


    private void DoScaleAndRotate()
    {
        LeanTween.cancel(gameObject);
        _rectTransform.localScale = Vector3.one;

        LeanTween.scale(_rectTransform, Vector3.one * _scaleUnit, _delayTime)
            .setEase(LeanTweenType.punch)
            .setIgnoreTimeScale(true);

        _rectTransform.rotation = Quaternion.identity;
        int sign = (int)Mathf.Sign(Random.Range(-1f, 1f));
        LeanTween.rotateAround(_rectTransform, Vector3.forward, _rotateAngleUnit * sign, _delayTime)
            .setEase(LeanTweenType.punch)
            .setIgnoreTimeScale(true);
    }
}
