using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private WeaponPosition[] _weaponPositions;

    public bool TryAddWeapon(WeaponDataSO weaponDataSO, int weaponLevel)
    {
        for (int i = 0; i < _weaponPositions.Length; i++)
        {
            if (_weaponPositions[i].Weapon != null)
                continue;

            _weaponPositions[i].AssignWeapon(weaponDataSO.Prefab, weaponLevel, i);

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

    public void RemovePlayerWeapon(int weaponPositionIndex)
    {
        for (int i = 0; i < _weaponPositions.Length; i++)
        {
            if (i != weaponPositionIndex)
                continue;

            _weaponPositions[i].UnAssignWeapon();

            return;
        }
    }
}
