using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    private const string STATS_ICON_PATH = "Data/StatsIconDataSO";
    private const string OBJECTS_PATH = "Data/ObjectsData";
    private const string WEAPON_PATH = "Data/WeaponsData";

    [Header(" Data ")]
    private static StatsIconSO _statsIconSO;
    private static ObjectDataSO[] _objectDatasSO;
    private static WeaponDataSO[] _weaponDatasSO;

    public static Sprite GetStatIcon(Stats stat)
    {
        if (_statsIconSO == null)
            _statsIconSO = Resources.Load<StatsIconSO>(STATS_ICON_PATH);

        foreach (StatIconData data in _statsIconSO.StatsIconDatas)
        {
            if (data.Stats == stat)
                return data.Icon;
        }

        Debug.LogError("Not set icon yet at Stat: " + stat.ToString());

        return null;
    }

    public static ObjectDataSO[] Objects
    {
        get
        {
            if (_objectDatasSO == null)
                _objectDatasSO = Resources.LoadAll<ObjectDataSO>(OBJECTS_PATH);

            return _objectDatasSO;
        }

        private set { }
    }

    public static WeaponDataSO[] Weapons
    {
        get
        {
            if (_weaponDatasSO == null)
                _weaponDatasSO = Resources.LoadAll<WeaponDataSO>(WEAPON_PATH);

            return _weaponDatasSO;
        }

        private set { }
    }

    public static ObjectDataSO GetRandomObjectData()
    {
        return Objects[Random.Range(0, Objects.Length)];
    }

    public static WeaponDataSO GetRandomWeaponData()
    {
        return Weapons[Random.Range(0, Weapons.Length)];
    }
}
