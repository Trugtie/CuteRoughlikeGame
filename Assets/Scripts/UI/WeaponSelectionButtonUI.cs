using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionButtonUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI _weaponNameText;
    [SerializeField] private Image _weaponIcon;

    [Header(" Stats ")]
    [SerializeField] private Transform _statContainerTransform;

    [field: SerializeField] public Button WeaponSelectButton { get; private set; }

    [Header("Colorable Containers")]
    [SerializeField] private Image[] _backgroundColorContainer;
    [SerializeField] private Outline _outline;

    public void Configure(WeaponDataSO weaponDataSO, int level)
    {
        _weaponNameText.text = weaponDataSO.name + " (lv " + (level + 1) + ")";
        _weaponNameText.color = ColorPalleteSystem.Instance.GetLevelColor(level);
        _weaponIcon.sprite = weaponDataSO.WeaponSprite;

        foreach (Image background in _backgroundColorContainer)
            background.color = ColorPalleteSystem.Instance.GetLevelColor(level);

        _outline.effectColor = ColorPalleteSystem.Instance.GetLevelOulineColor(level);

        Dictionary<Stats, float> calculatedDictionary = WeaponCalculator.GetCalculatedWeaponStats(weaponDataSO, level);
        ConfigureStats(calculatedDictionary);
    }

    private void ConfigureStats(Dictionary<Stats, float> calculatedDictionary)
    {
        StatContainerManager.GenerateStatContainer(calculatedDictionary, _statContainerTransform);
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
