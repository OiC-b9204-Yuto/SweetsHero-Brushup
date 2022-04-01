using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponAct : MonoBehaviour
{
    //WeaponInfoÇ…Ç‹Ç∆ÇﬂÇÈÇ◊Ç´?<-CharacterÇ∆å`ÇçáÇÌÇπÇÈë_Ç¢
    //ïêäÌÇÃèÓïÒ
    [SerializeField] private int    currentAmmo;
    [SerializeField] private int    maxAmmo;
    [SerializeField] private int    useAmmoPerShot;
    [SerializeField] private int    currentRemainingAmmo;
    
    [SerializeField] private int    damage;
    [SerializeField] private float  shotRange;
    [SerializeField] private float  fireRate;
    [SerializeField] private float  reloadTime;

    private float  shotCoolTimer;
    private float  reloadTimer;

    [SerializeField] private ParticleSystem muzzleFlash;

    private bool isReload;
    private bool isNoAmmo;

    //íeä€èÓïÒ
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private float bulletSpeed;

    //âπëfçﬁ
    [SerializeField] private AudioClip sound_Shot;
    [SerializeField] private AudioClip sound_Reload;

    //get.set
    public int CurrentAmmo { get { return currentAmmo; } }
    public int CurrentRemainingAmmo { get { return currentRemainingAmmo; } set { currentRemainingAmmo = value; } }
    public int Damage { get { return damage; } }
    public bool IsReload { get { return isReload; } }
    public bool IsNoAmmo { get { return currentRemainingAmmo == 0; } }

    // Start is called before the first frame update
    void Awake()
    {
        muzzleFlash.Stop();
        reloadTimer = reloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        ShotUpdate();
        ReloadUpdate();
    }

    public bool Shot()
    {
        if (isReload) return false;
        if (shotCoolTimer > 0) return false;
        if(currentAmmo > 0)
        {
            currentAmmo -= useAmmoPerShot;
            muzzleFlash.Play();
            RaycastHit[] hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, shotRange).
                Where(hit => hit.collider.isTrigger == false).
                OrderBy(hit => hit.distance).ToArray();
            //íeä€ê∂ê¨èàóù
            GameObject obj = (GameObject)Instantiate(bulletObject, shotPoint.transform.position, Quaternion.identity);

            if (hits != null)
            {
                obj.transform.LookAt(hits[0].point);
            }
            else
            {
                //ìñÇΩÇ¡ÇƒÇ»Ç¢èÍçáÇÕê≥ñ âìÇ≠Çë_Ç¡Çƒ
                obj.transform.LookAt(transform.position + transform.parent.forward * 1000);
            }

            obj.GetComponent<Bullet>().Bullet_Damage = damage;
            obj.GetComponent<Bullet>().Owner = transform.parent.parent.parent.gameObject;
            Rigidbody rig = obj.GetComponent<Rigidbody>();
            rig.AddForce(obj.transform.forward * bulletSpeed);

            shotCoolTimer = fireRate;
            AudioManager.Instance.SE.PlayOneShot(sound_Shot);

            return true;
        }
        else
        {
            Reload();
        }

        return false;
    }

    public bool Reload()
    {
        if (currentAmmo == maxAmmo) return false;
        if (isNoAmmo || isReload) return false;
        isReload = true;
        return true;
    }

    void ShotUpdate()
    {
        if (shotCoolTimer >= 0)
        {
            shotCoolTimer -= Time.deltaTime;
        }
    }

    void ReloadUpdate()
    {
        if (!isReload) return;

        if(reloadTimer <= 0)
        {
            if(currentRemainingAmmo >= maxAmmo)
            {
                currentRemainingAmmo -= maxAmmo - currentAmmo;
                currentAmmo = maxAmmo;
            }
            else if(currentRemainingAmmo > 0)
            {
                currentAmmo += currentRemainingAmmo;
                if(currentAmmo > maxAmmo)
                {
                    currentRemainingAmmo = currentAmmo - maxAmmo;
                    currentAmmo = maxAmmo;
                }
            }
            reloadTimer = reloadTime;
            isReload = false;
        }
        else
        {
            reloadTimer -= Time.deltaTime;
        }
    }

    public void PlayReloadSound()
    {
        AudioManager.Instance.SE.PlayOneShot(sound_Reload);
    }
}
