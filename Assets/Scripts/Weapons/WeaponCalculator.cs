using System.Collections.Generic;
using UnityEngine;

public static class WeaponCalculator
{
    public static Dictionary<Stats, float> GetCalculatedWeaponStats(WeaponDataSO weaponDataSO, int level)
    {
        float multiplier = 1 + (float)level / 3;

        Dictionary<Stats, float> calculatedDictionary = new Dictionary<Stats, float>();

        foreach (KeyValuePair<Stats, float> kvp in weaponDataSO.BaseStats)
        {
            if (kvp.Key == Stats.Range && weaponDataSO.Prefab.GetType() != typeof(RangeWeapon))
            {
                calculatedDictionary.Add(kvp.Key, kvp.Value);
                continue;
            }
            calculatedDictionary.Add(kvp.Key, kvp.Value * multiplier);
        }

        return calculatedDictionary;
    }

    public static int GetCalculatedWeaponPrice(WeaponDataSO weaponDataSO, int level)
    {
        float multiplier = 1 + (float)level / 3;
        return (int)(weaponDataSO.PurchasePrice * multiplier);
    }

    public static int GetCalculatedWeaponRecylePrice(WeaponDataSO weaponDataSO, int level)
    {
        float multiplier = 1 + (float)level / 3;
        return (int)(weaponDataSO.PurchasePrice * multiplier) * 75 / 100;
    }
}
