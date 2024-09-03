using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    private const string PATH = "Data/StatsIconDataSO";

    [Header(" Data ")]
    private static StatsIconSO _statsIconSO;

    public static Sprite GetStatIcon(Stats stat)
    {
        if (_statsIconSO == null)
            _statsIconSO = Resources.Load<StatsIconSO>(PATH);

        foreach (StatIconData data in _statsIconSO.StatsIconDatas)
        {
            if (data.Stats == stat)
                return data.Icon;
        }

        Debug.LogError("Not set icon yet at Stat: " + stat.ToString());

        return null;
    }
}
