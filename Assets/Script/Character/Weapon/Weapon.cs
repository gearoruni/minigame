using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;

    public WeaponData Init(int weaponid)
    {
        weaponData = GetComponent<WeaponData>();
        weaponData.Init(weaponid);
        return weaponData;
    }
}
