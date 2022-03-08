using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character_State : MonoBehaviour
{
    //複数武器への対応のためリスト化する予定
    [SerializeField] GameObject[] weapon_object;
    [SerializeField] Weapon_State weapon_state;

    [SerializeField] Character_Info character_Info; 

    [SerializeField] private GameObject GrenadeObject;
    [SerializeField] private float ThrowPower;
    [SerializeField] private Animator weapon_animator;

    private bool IsChangeWeapon;
    private int BeforeWeaponNumber;


    // Start is called before the first frame update
    void Awake()
    {
        IsChangeWeapon = false;
        BeforeWeaponNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;


        if (IsChangeWeapon)
        {
            //30度まで腕を下げる
            if(weapon_object[BeforeWeaponNumber].transform.localEulerAngles.x < 30)
            {
                weapon_object[BeforeWeaponNumber].transform.Rotate(new Vector3(1,0,0));
            }
            else
            {
                Debug.Log("通った");
                //前の武器を無効に
                weapon_object[BeforeWeaponNumber].SetActive(false);
                //今の武器を有効に
                weapon_object[character_Info.Character_CurrentWeapon].SetActive(true);
                //0度まで腕を上げる
                if (weapon_object[character_Info.Character_CurrentWeapon].transform.rotation.x > 0)
                {
                    weapon_object[character_Info.Character_CurrentWeapon].transform.Rotate(new Vector3(-1, 0, 0));
                }
                else
                {
                    IsChangeWeapon = false;
                }
            }
        }
        else
        {
            /*float wheelroll = Input.GetAxis("Mouse ScrollWheel");
            if (wheelroll > 0)
            {
                weapon_state.ReloadCancel();
                IsChangeWeapon = true;
                BeforeWeaponNumber = character_Info.Character_CurrentWeapon;
                character_Info.Character_CurrentWeapon--;
            }
            else if (wheelroll < 0)
            {
                weapon_state.ReloadCancel();
                IsChangeWeapon = true;
                BeforeWeaponNumber = character_Info.Character_CurrentWeapon;
                character_Info.Character_CurrentWeapon++;
            }*/

            weapon_animator.SetBool("IsWalk", character_Info.Character_IsMove);

            //Inputでまとめるのも大事かもな
            //アニメーションどうこうで同時に起こらないようにしろ
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (character_Info.Character_CurrentGrenades <= 0) return;
                character_Info.Character_CurrentGrenades -= 1;
                //前まっすぐ+重力なので要調整
                GameObject obj =　Instantiate(GrenadeObject, transform.position + transform.forward * 2 + transform.up * 1, transform.rotation);
                Rigidbody rig = obj.GetComponent<Rigidbody>();
                rig.AddForce(transform.forward * ThrowPower);
            }

            if (Input.GetKeyDown(Custom_InputManager.Instance.inputData.Chara_Reload))
            {
                if(weapon_state.Reload())
                {
                    weapon_animator.SetTrigger("TriggerReload");
                }
            }

            character_Info.Character_IsReload = weapon_state.IsReload;

            if (Input.GetKey(Custom_InputManager.Instance.inputData.Chara_Fire) && !weapon_state.IsReload)
            {
                if(weapon_state.Weapon_CurrentAmmo >0)
                {
                    weapon_state.Shot();
                    weapon_animator.SetBool("IsShot", true);
                }
                else
                {
                    if (weapon_state.Reload())
                    {
                        weapon_animator.SetTrigger("TriggerReload");
                    }
                }     
            }
            else
            {
                weapon_animator.SetBool("IsShot", false);   
            }
        }
    }

    public void RecovAmmo(int value)
    {

            weapon_state.Weapon_CurrentMagazine += value;

    }

}
