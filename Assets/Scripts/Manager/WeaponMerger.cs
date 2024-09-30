using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMerger : MonoBehaviour
{
    public static WeaponMerger Instance { get; private set; }

    [Header(" Elements ")]
    [SerializeField] private PlayerWeapons _playerWeapons;

    private List<Weapon> _weaponMergeList = new List<Weapon>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool CanMerge(Weapon weapon)
    {
        if (weapon.Level >= 3)
            return false;

        _weaponMergeList.Clear();
        _weaponMergeList.Add(weapon);

        Weapon[] playerWeapons = _playerWeapons.GetWeapons();

        foreach (Weapon playerWeapon in playerWeapons)
        {
            if (playerWeapon == null)
                continue;

            if (playerWeapon == weapon)
                continue;

            if (playerWeapon.WeaponDataSO.WeaponName != weapon.WeaponDataSO.WeaponName)
                continue;

            if (playerWeapon.Level != weapon.Level)
                continue;

            _weaponMergeList.Add(playerWeapon);

            return true;
        }

        return false;
    }

    public void Merge()
    {
        Debug.Log("Merge");
    }
}
