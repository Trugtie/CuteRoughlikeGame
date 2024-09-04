using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatContainerManager : MonoBehaviour
{
    public static StatContainerManager Instance { get; private set; }

    [Header(" Elements ")]
    [SerializeField] private StatsValueContainerUI _statsValueContainerPrefab;

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
            string statValue = kvp.Value.ToString();

            instanceStatValueUI.Configure(statIcon, statName, statValue);
        }
    }

    public static void GenerateStatContainer(Dictionary<Stats, float> dictionary, Transform parentTransform)
    {
        Instance.GenerateContainer(dictionary, parentTransform);
    }

    private void ClearStats(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
