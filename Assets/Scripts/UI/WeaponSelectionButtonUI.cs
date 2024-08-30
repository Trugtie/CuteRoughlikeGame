using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionButtonUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI _weaponNameText;
    [SerializeField] private Image _weaponIcon;
    [field: SerializeField] public Button WeaponSelectButton { get; private set; }

    public void Configure(WeaponDataSO weaponDataSO)
    {
        _weaponNameText.text = weaponDataSO.name;
        _weaponIcon.sprite = weaponDataSO.WeaponSprite;
    }

    public void Select()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector2.one * 1.05f, 0.1f).setEase(LeanTweenType.easeInOutSine);
    }

    public void DeSelect()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector2.one, 0.1f);
    }
}
