using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character_State : MonoBehaviour
{
    //��������ւ̑Ή��̂��߃��X�g������\��
    [SerializeField] Weapon_State weapon_state;

    [SerializeField] Character_Info character_Info; 

    [SerializeField] private GameObject GrenadeObject;
    [SerializeField] private float ThrowPower;
    [SerializeField] private Animator weapon_animator;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        weapon_animator.SetBool("IsWalk", character_Info.Character_IsMove);

        //Input�ł܂Ƃ߂�̂��厖������
        //�A�j���[�V�����ǂ������œ����ɋN����Ȃ��悤�ɂ���
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (character_Info.Character_CurrentGrenades < 0) return;
            character_Info.Character_CurrentGrenades -= 1;
            //�O�܂�����+�d�͂Ȃ̂ŗv����
            GameObject obj =�@Instantiate(GrenadeObject, transform.position + transform.forward * 2 + transform.up * 1, transform.rotation);
            Rigidbody rig = obj.GetComponent<Rigidbody>();
            rig.AddForce(transform.forward * ThrowPower);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(weapon_state.Reload())
            {
                weapon_animator.SetTrigger("TriggerReload");
            }
        }

        if (Input.GetButton("Fire1") && !weapon_state.IsReload)
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

        float wheelroll = Input.GetAxis("Mouse ScrollWheel");
        if(wheelroll > 0)
        {
            weapon_state.ReloadCancel();
            character_Info.Character_CurrentWeapon--;
        }
        else if(wheelroll < 0)
        {
            weapon_state.ReloadCancel();
            character_Info.Character_CurrentWeapon++;
        }
    }
}
