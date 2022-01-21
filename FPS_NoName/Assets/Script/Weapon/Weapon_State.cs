using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_State : MonoBehaviour
{
    Character_Info _CharacterInfo;
    [SerializeField] private int weapon_id;
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
    [SerializeField] private Transform FocusPoint;

    [SerializeField] private GameObject BulletObject;

    [SerializeField] private float BulletSpead;

    [SerializeField] private GameObject GrenadeObject;

    public int Weapon_ID { get { return weapon_id; } }
    public int Weapon_CurrentAmmo { get { return weapon_CurrentAmmo; } }
    public int Weapon_CurrentMagazine { get { return weapon_CurrentMagazine; } set {  weapon_CurrentMagazine = value; } }
    public int Weapon_Damage { get { return weapon_Damage; } }
    public bool IsReload { get { return isReload; } }
    public bool IsNoAmmo { get { return weapon_CurrentMagazine == 0; } }

    private void Awake()
    {
        _CharacterInfo = GameObject.Find("Player").GetComponent<Character_Info>();
        Weapon_DefaultAmmo = weapon_CurrentAmmo;
        Weapon_DefaultReloadTime = ReloadTime; 
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

        //グレネードテスト用
        if (Input.GetKeyDown(KeyCode.G) && (_CharacterInfo.Character_CurrentGrenades > 0))
        {
            _CharacterInfo.Character_CurrentGrenades --;
            Instantiate(GrenadeObject, ShootPoint.transform.position, Quaternion.identity);
        }
    }

    public void Shot()
    {
        if (isReload) return;
        if (NextFireTime > 0) return;

        if(weapon_CurrentAmmo > 0)
        {
            weapon_CurrentAmmo -= Weapon_UsePerShot_Ammo;
            RaycastHit Hit;   
            NextFireTime = FireRate;
            //弾丸生成処理
            GameObject obj = (GameObject)Instantiate(BulletObject, ShootPoint.transform.position,Quaternion.identity);
            obj.transform.LookAt(FocusPoint);
            obj.GetComponent<Bullet>().Bullet_Damage = Weapon_Damage;
            Rigidbody rig = obj.GetComponent<Rigidbody>();
            rig.AddForce(transform.forward * BulletSpead);

            if (Physics.Raycast(ShootPoint.position,ShootPoint.transform.forward,out Hit,ShotRange))
            {
                if (Hit.collider.gameObject.tag == "Enemy")
                {
                    Debug.Log(Hit.transform.name + "に当たりました");
                }
            }
        }
        else
        {
            Reload();
        }
    }

    public void Reload()
    {
        if (weapon_CurrentAmmo == Weapon_DefaultAmmo) return;
        isReload = true;
    }

    void ReloadUpdate()
    {
        if (!isReload) return;
        if (IsNoAmmo) return;

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
}
