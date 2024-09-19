using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private WeaponPosition[] _weaponPositions;

    public bool TryAddWeapon(WeaponDataSO weaponDataSO, int weaponLevel)
    {
        foreach (WeaponPosition weaponPosition in _weaponPositions)
        {
            if (weaponPosition.Weapon != null)
                continue;

            weaponPosition.AssignWeapon(weaponDataSO.Prefab, weaponLevel);
            return true;
        }

        return false;
    }

    public Weapon[] GetWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();

        foreach (WeaponPosition weaponPos in _weaponPositions)
        {
            if (weaponPos.Weapon == null)
                continue;
            weapons.Add(weaponPos.Weapon);
        }

        return weapons.ToArray();
    }
}
