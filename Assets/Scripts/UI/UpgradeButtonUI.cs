using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _upgradeStatText;
    [SerializeField] private TextMeshProUGUI _valueText;
    [field: SerializeField] public Button Button { get; private set; }

    public void ConfigueUpgradeButton(Sprite icon, string upgradeStatName, string valueString)
    {
        _icon.sprite = icon;
        _upgradeStatText.SetText(upgradeStatName);
        _valueText.SetText(valueString);
    }
}
