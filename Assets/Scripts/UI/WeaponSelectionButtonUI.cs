using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionButtonUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI _weaponNameText;
    [SerializeField] private Image _weaponIcon;
    [field: SerializeField] public Button WeaponSelectButton { get; private set; }

    [Header("Colorable Containers")]
    [SerializeField] private Image[] _backgroundColorContainer;

    public void Configure(WeaponDataSO weaponDataSO, int level)
    {
        _weaponNameText.text = weaponDataSO.name;
        _weaponNameText.color = ColorPalleteSystem.Instance.GetLevelColor(level);
        _weaponIcon.sprite = weaponDataSO.WeaponSprite;

        foreach (Image background in _backgroundColorContainer)
            background.color = ColorPalleteSystem.Instance.GetLevelColor(level);
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
