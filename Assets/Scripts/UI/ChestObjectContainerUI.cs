using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestObjectContainerUI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI _objectNameText;
    [SerializeField] private Image _objectIcon;

    [Header(" Stats ")]
    [SerializeField] private Transform _statContainerTransform;

    [field: SerializeField] public Button TakeButton { get; private set; }
    [field: SerializeField] public Button RecycleButton { get; private set; }

    [Header("Colorable Containers")]
    [SerializeField] private Image[] _backgroundColorContainer;
    [SerializeField] private Outline _outline;

    public void Configure(ObjectDataSO objectDataSO)
    {
        _objectNameText.text = objectDataSO.name;
        _objectNameText.color = ColorPalleteSystem.Instance.GetLevelColor(objectDataSO.Rality);
        _objectIcon.sprite = objectDataSO.Icon;

        foreach (Image background in _backgroundColorContainer)
            background.color = ColorPalleteSystem.Instance.GetLevelColor(objectDataSO.Rality);

        _outline.effectColor = ColorPalleteSystem.Instance.GetLevelOulineColor(objectDataSO.Rality);

        ConfigureStats(objectDataSO.BaseStats);
    }

    private void ConfigureStats(Dictionary<Stats, float> objectStatsDictionary)
    {
        StatContainerManager.GenerateStatContainer(objectStatsDictionary, _statContainerTransform);
    }
}
