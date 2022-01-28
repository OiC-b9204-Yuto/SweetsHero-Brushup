using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_State : MonoBehaviour
{
    UI_MainGame MainGameUI_System;
    [SerializeField] private GameObject MainGameUI;
    [SerializeField] private int weapon_CurrentAmmo;
    [SerializeField] private int Weapon_UsePerShot_Ammo;
    [SerializeField] private int weapon_CurrentMagazine;
    [SerializeField] private int weapon_Damage;

    [SerializeField] private float ShotRange;
    [SerializeField] private float FireRate;
    [SerializeField] private float ReloadTime;
    [SerializeField] private float NextFireTime;


    private int Weapon_DefaultAmmo;
    private float Weapon_DefaultReloadTime;

    private bool isReload;
    private bool isNoAmmo;

    [SerializeField] private Transform ShootPoint;

    [SerializeField] private GameObject BulletObject;

    [SerializeField] private float BulletSpeed;

    [SerializeField] private ParticleSystem MuzzleFlash;

    [SerializeField] private AudioClip Sound_Shot;
    [SerializeField] private AudioClip Sound_Reload;
    [SerializeField] private AudioClip Sound_Walk;

    public int Weapon_CurrentAmmo { get { return weapon_CurrentAmmo; } }
    public int Weapon_CurrentMagazine { get { return weapon_CurrentMagazine; } set {  weapon_CurrentMagazine = value; } }
    public int Weapon_Damage { get { return weapon_Damage; } }
    public bool IsReload { get { return isReload; } }
    public bool IsNoAmmo { get { return weapon_CurrentMagazine == 0; } }

    private void Awake()
    {
        MainGameUI_System = MainGameUI.GetComponent<UI_MainGame>(); 
        Weapon_DefaultAmmo = weapon_CurrentAmmo;
        Weapon_DefaultReloadTime = ReloadTime;
        MuzzleFlash.Stop();
    }
    void Update()
    {
        CheckState();
    }

    void CheckState()
    {
        ReloadUpdate();
        //実質的なShotUpdate **内容が増えたら関数化します**
        if (NextFireTime >= 0)
        {
            NextFireTime -= Time.deltaTime;
        }
    }

    public void Shot()
    {
        if (MainGameUI_System.isStartAnimation) return;
        if (MainGameUI_System.isGameClear || MainGameUI_System.isGameOver) return;
        if (isReload) return;
        if (NextFireTime > 0) return;

        if(weapon_CurrentAmmo > 0)
        {
            weapon_CurrentAmmo -= Weapon_UsePerShot_Ammo;
            MuzzleFlash.Play();
            RaycastHit Hit;
            bool rayCheck = Physics.Raycast(Camera.main.transform.position , Camera.main.transform.forward, out Hit, ShotRange);
            //弾丸生成処理
            GameObject obj = (GameObject)Instantiate(BulletObject, ShootPoint.transform.position,Quaternion.identity);
            if (rayCheck)
            {
                obj.transform.LookAt(Hit.point);
            }
            else
            {
                obj.transform.LookAt(transform.position + transform.parent.forward * 1000);
            }
            obj.GetComponent<Bullet>().Bullet_Damage = Weapon_Damage;
            obj.GetComponent<Bullet>().Owner = transform.parent.parent.parent.gameObject;
            Rigidbody rig = obj.GetComponent<Rigidbody>();
            rig.AddForce(obj.transform.forward * BulletSpeed);

            NextFireTime = FireRate;
            AudioManager.Instance.SE.PlayOneShot(Sound_Shot);
            return;
        }
    }

    public bool Reload()
    {
        if (weapon_CurrentAmmo == Weapon_DefaultAmmo) return false;
        if (IsNoAmmo) return false;
        if (isReload) return false;
        isReload = true;
        return true;
    }

    public void ReloadCancel()
    {
        isReload = false;
        ReloadTime = Weapon_DefaultReloadTime;
    }

    void ReloadUpdate()
    {
        if (!isReload) return;

        if (ReloadTime <= 0)
        {
            if (weapon_CurrentMagazine >= Weapon_DefaultAmmo)
            {
                weapon_CurrentMagazine -= Weapon_DefaultAmmo - weapon_CurrentAmmo;
                weapon_CurrentAmmo = Weapon_DefaultAmmo;

            }
            else if (weapon_CurrentMagazine > 0)
            {
                weapon_CurrentAmmo += weapon_CurrentMagazine;
                if(weapon_CurrentAmmo > Weapon_DefaultAmmo)
                {
                    weapon_CurrentMagazine = weapon_CurrentAmmo - Weapon_DefaultAmmo;
                    weapon_CurrentAmmo = Weapon_DefaultAmmo;
                }
                else
                {
                    weapon_CurrentMagazine = 0;
                }
            }

            ReloadTime = Weapon_DefaultReloadTime;
            Debug.Log("リロード完了");
            isReload = false;
        }
        else
        {
            Debug.Log("リロード中");
            ReloadTime -= Time.deltaTime;
        }
    }

    public void PlayReloadSound()
    {
        AudioManager.Instance.SE.PlayOneShot(Sound_Reload);
    }
    public void PlaySoundWalk()
    {
        AudioManager.Instance.SE.PlayOneShot(Sound_Walk,0.5f);
    }
}
