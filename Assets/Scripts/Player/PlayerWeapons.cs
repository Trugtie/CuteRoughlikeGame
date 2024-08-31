using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private WeaponPosition[] _weaponPositions;

    public void AddWeapon(WeaponDataSO weaponDataSO, int weaponLevel)
    {
        WeaponPosition weaponPosition = _weaponPositions[Random.Range(0, _weaponPositions.Length)];

        weaponPosition.AssignWeapon(weaponDataSO.Prefab);
    }
}
