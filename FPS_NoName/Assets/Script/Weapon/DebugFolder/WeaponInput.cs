using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    [SerializeField] private WeaponAct weaponAct;

    public bool Shot()
    {
        return weaponAct.Shot();
    }

    public bool Reload()
    {
        return weaponAct.Reload();
    }

    public bool RecovAmmo(int value)
    {
        weaponAct.RecovAmmo(value);
        return true;
    }
}
