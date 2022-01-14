using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_State : MonoBehaviour
{
    [SerializeField] private int weapon_id;
    [SerializeField] private int weapon_CurrentAmmo;
    private int Weapon_DefaultAmmo;
    [SerializeField] private int weapon_CurrentMagazine;

    [SerializeField] private float FireRate;
    [SerializeField] private float ReloadTime;
    private float Weapon_DefaultReloadTime;
    [SerializeField] private float NextFireTime;

    private bool isReload;
    private bool isNoAmmo;

    public int Weapon_ID { get { return weapon_id; } }

    public int Weapon_CurrentAmmo { get { return weapon_CurrentAmmo; } }
    public int Weapon_CurrentMagazine { get { return weapon_CurrentMagazine; } set { value = weapon_CurrentMagazine; } }
    public bool IsReload { get { return isReload; } }
    public bool IsNoAmmo { get { return isNoAmmo; } }

    private void Awake()
    {
        Weapon_DefaultAmmo = weapon_CurrentAmmo;
        Weapon_DefaultReloadTime = ReloadTime;
        
    }
    void Update()
    {
        CheckState();
    }

    void CheckState()
    {

        if (isReload)
        {
            Reload();
        }

        if (NextFireTime >= 0)
        {
            NextFireTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReload)
        {
            Reload();
        }

        if (Input.GetButton("Fire1") && NextFireTime <= 0 && !isReload)
        {
            Shot();
        }
    }

    void Shot()
    {
        if(weapon_CurrentAmmo > 0)
        {
            NextFireTime = FireRate;
            weapon_CurrentAmmo--;
            Debug.Log("うちました");
        }
        else
        {
            Reload();
        }
    }

    void Reload()
    {
        if(weapon_CurrentMagazine == 0)
        {
            isNoAmmo = true;
            return;
        }
        else
        {
            isNoAmmo = false;
        }

        if (ReloadTime <= 0)
        {
            if (weapon_CurrentMagazine >= Weapon_DefaultAmmo)
            {
                weapon_CurrentMagazine -= Weapon_DefaultAmmo - weapon_CurrentAmmo;
                weapon_CurrentAmmo = Weapon_DefaultAmmo;

            }
            else if (weapon_CurrentMagazine > 0)
            {
                weapon_CurrentAmmo = weapon_CurrentMagazine;
                weapon_CurrentMagazine = 0;
            }

            ReloadTime = Weapon_DefaultReloadTime;
            Debug.Log("リロード完了");
            isReload = false;
        }
        else
        {
            isReload = true;
            Debug.Log("リロード中");
            ReloadTime -= Time.deltaTime;
        }
    }
}
