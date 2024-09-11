using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatContainerManager : MonoBehaviour
{
    public static StatContainerManager Instance { get; private set; }

    [Header(" Elements ")]
    [SerializeField] private StatsValueContainerUI _statsValueContainerPrefab;

    private List<StatsValueContainerUI> _statsValueContainers = new List<StatsValueContainerUI>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void GenerateContainer(Dictionary<Stats, float> dictionary, Transform parentTransform)
    {
        ClearStats(parentTransform);

        foreach (KeyValuePair<Stats, float> kvp in dictionary)
        {
            StatsValueContainerUI instanceStatValueUI = Instantiate(_statsValueContainerPrefab, parentTransform);

            Sprite statIcon = ResourceManager.GetStatIcon(kvp.Key);
            string statName = Enums.FormatEnumString(kvp.Key);
            float statValue = kvp.Value;

            _statsValueContainers.Add(instanceStatValueUI);

            instanceStatValueUI.Configure(statIcon, statName, statValue);
        }

        LeanTween.delayedCall(Time.deltaTime * 2, () => { ResizeFontSize(); });
    }

    private void GenerateContainerWithFrame(Dictionary<Stats, float> dictionary, StatsValueContainerUI frame, Transform parentTransform)
    {
        ClearStatsHaveFrameUI(parentTransform);

        foreach (KeyValuePair<Stats, float> kvp in dictionary)
        {
            StatsValueContainerUI instanceStatValueUI = Instantiate(frame, parentTransform);

            Sprite statIcon = ResourceManager.GetStatIcon(kvp.Key);
            string statName = Enums.FormatEnumString(kvp.Key);
            float statValue = kvp.Value;

            _statsValueContainers.Add(instanceStatValueUI);

            instanceStatValueUI.Configure(statIcon, statName, statValue);
            instanceStatValueUI.gameObject.SetActive(true);
        }

        LeanTween.delayedCall(Time.deltaTime * 2, () => { ResizeFontSize(); });
    }

    private void ResizeFontSize()
    {
        if (_statsValueContainers.Count < 0) return;

        float minFontSize = 5000f;

        foreach (StatsValueContainerUI statValueUI in _statsValueContainers)
        {
            float fontSize = statValueUI.GetFontSize();

            if (fontSize < minFontSize)
                minFontSize = fontSize;
        }

        foreach (StatsValueContainerUI statValueUI in _statsValueContainers)
        {
            statValueUI.SetFontSize(minFontSize);
        }
    }

    public static void GenerateStatContainer(Dictionary<Stats, float> dictionary, Transform parentTransform)
    {
        Instance.GenerateContainer(dictionary, parentTransform);
    }

    public static void GenerateStatContainerWithFrame(Dictionary<Stats, float> dictionary, StatsValueContainerUI frame, Transform parentTransform)
    {
        Instance.GenerateContainerWithFrame(dictionary, frame, parentTransform);
    }

    private void ClearStats(Transform parentTransform)
    {
        _statsValueContainers.Clear();

        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ClearStatsHaveFrameUI(Transform parentTransform)
    {
        _statsValueContainers.Clear();

        foreach (Transform child in parentTransform)
        {
            if (child == parentTransform.GetChild(0))
            {
                if (child.gameObject.activeSelf)
                    child.gameObject.SetActive(false);

                continue;
            }
            Destroy(child.gameObject);
        }
    }
}
