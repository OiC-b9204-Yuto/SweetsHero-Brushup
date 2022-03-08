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
    [SerializeField] private SphereCollider AutoPickArea; //�����s�b�N�G���A
    private float changeCoolDown; //�A�C�e�����[�h�؂�ւ��̃N�[���^�C��
    private Ray ManualItem_Ray;  //�A�C�e���Ƀ��C���΂�
    private RaycastHit manualItem_Hit; //���C�ɃA�C�e�����������Ă�����
    public float ChangeCoolDown { get { return changeCoolDown; } } //�N�[���^�C���擾�p
    public RaycastHit ManualItem_Hit { get { return manualItem_Hit; } } //�A�C�e�����擾�p
    public bool IsHitItem;
    private void Awake()
    {
        itemPickMode = ItemPickMode.ManualPick;
        ScreenCenterX = Screen.width / 2;
        ScreenCenterY = Screen.height / 2;
        CenterPos = new Vector3(ScreenCenterX, ScreenCenterY, 1.0f);
    }

    void Update()
    {
        ModeChange();
        PickSystem();
    }

    void ModeChange()
    {
        if (changeCoolDown > 0.0f)
        {
            changeCoolDown -= Time.deltaTime;
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

    void PickSystem()
    {
        switch (itemPickMode)
        {
            case ItemPickMode.AutoPick: //�����s�b�N�̏ꍇ
                AutoPickArea.enabled = true;
                break;
            case ItemPickMode.ManualPick: //�蓮�s�b�N�̏ꍇ
                ManualItem_Ray = PlayerCamera.ScreenPointToRay(CenterPos);
                Debug.DrawRay(ManualItem_Ray.origin, ManualItem_Ray.direction * ManualPickDistance, Color.red, 0.5f, false);
                AutoPickArea.enabled = false;
                if (Physics.Raycast(ManualItem_Ray, out manualItem_Hit, ManualPickDistance) && ManualItem_Hit.collider.tag == "Item")
                {
                    IsHitItem = true;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        manualItem_Hit.collider.GetComponent<DropItem_Setting>().GetItem();
                    }
                }
                else
                {
                    IsHitItem = false;
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Item")
        {
            col.GetComponent<DropItem_Setting>().GetItem();
        }
    }
}
