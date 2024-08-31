using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public void AddWeapon(WeaponDataSO weaponDataSO, int weaponLevel)
    {
        Debug.Log($"Added:  {weaponDataSO.WeaponName}, level: {weaponLevel}");
    }
}
