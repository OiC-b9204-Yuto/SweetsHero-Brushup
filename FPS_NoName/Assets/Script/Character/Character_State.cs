using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_State : MonoBehaviour
{
    //��������ւ̑Ή��̂��߃��X�g������\��
    [SerializeField] Weapon_State weapon_state;
    [SerializeField] Character_Info character_Info;

    [SerializeField] private GameObject GrenadeObject;
    [SerializeField] private float ThrowPower;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        //Input�ł܂Ƃ߂�̂��厖������
        //�A�j���[�V�����ǂ������œ����ɋN����Ȃ��悤�ɂ���
        if (Input.GetKeyDown(KeyCode.G))
        {
            //�O�܂�����+�d�͂Ȃ̂ŗv����
            GameObject obj =�@Instantiate(GrenadeObject, transform.position + transform.forward * 2 + transform.up * 1, transform.rotation);
            Rigidbody rig = obj.GetComponent<Rigidbody>();
            rig.AddForce(transform.forward * ThrowPower);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            weapon_state.Reload();
        }

        if (Input.GetButton("Fire1"))
        {
            weapon_state.Shot();
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
