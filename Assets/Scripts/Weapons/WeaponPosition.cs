using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{
    public Weapon Weapon { get; private set; }

    public void AssignWeapon(Weapon weapon, int level)
    {
        Weapon = Instantiate(weapon, transform);
        Weapon.transform.localPosition = Vector3.zero;

        Weapon.UpdateToLevel(level);
    }
}
