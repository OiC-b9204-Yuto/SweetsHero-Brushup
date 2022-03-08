using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_PickItem : MonoBehaviour
{
    public enum ItemPickMode //�A�C�e���s�b�N���[�h
    {
        AutoPick,
        ManualPick,
    }

    private int ScreenCenterX; //��ʂ̒��� X ���W���擾
    private int ScreenCenterY; //��ʂ̒��� Y ���W���擾
    private Vector3 CenterPos; //��ʂ̒����̃x�N�g�����擾
    public ItemPickMode itemPickMode; //�A�C�e���s�b�N���[�h�̎Q��
    [SerializeField] private Camera PlayerCamera; //�v���C���[�J����
    [SerializeField] private float ManualPickDistance; //�}�j���A���s�b�N�̃A�C�e�����擾�ł��鋗��
    private float changeCoolDown; //�A�C�e�����[�h�؂�ւ��̃N�[���^�C��
    Ray ManualItem_Ray; 
    RaycastHit ManualItem_Hit;

    public float ChangeCoolDown { get { return changeCoolDown; } } //�N�[���^�C�����擾

    private void Awake()
    {
        ScreenCenterX = Screen.width / 2;
        ScreenCenterY = Screen.height / 2;
        CenterPos = new Vector3(ScreenCenterX, ScreenCenterY, 1.0f);
        ManualItem_Ray = PlayerCamera.ScreenPointToRay(CenterPos);
    }

    void Update()
    {
        if(changeCoolDown > 0.0f)
        {
            changeCoolDown -= Time.deltaTime;
        }
        ModeChange();
        ManualPickMode();
    }

    void ModeChange()
    {
        if (changeCoolDown > 0.0f)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            switch (itemPickMode)
            {
                case ItemPickMode.AutoPick:
                    itemPickMode = ItemPickMode.ManualPick;
                    changeCoolDown = 5.0f;
                    break;
                case ItemPickMode.ManualPick:
                    itemPickMode = ItemPickMode.AutoPick;
                    changeCoolDown = 5.0f;
                    break;
            }
        }
    }

    void ManualPickMode() //�蓮�s�b�N
    {
        if (itemPickMode == ItemPickMode.ManualPick) 
        {
            if (Physics.Raycast(ManualItem_Ray, out ManualItem_Hit, ManualPickDistance))
            {
                if (ManualItem_Hit.collider.tag == "Item")
                {
                    Debug.Log(ManualItem_Hit.collider.gameObject.name + "��������܂���");
                }

            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Item")
        {
            if (itemPickMode == ItemPickMode.AutoPick) //�����s�b�N�̏ꍇ
            {
                col.GetComponent<DropItem_Setting>().GetItem(); //�h���b�v�A�C�e���̃Q�b�gitem�����s����
            }
        }
    }


}
