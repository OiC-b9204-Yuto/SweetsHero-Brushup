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
    private Ray ManualItem_Ray;  //�A�C�e���Ƀ��C���΂�
    private RaycastHit manualItem_Hit; //���C�ɃA�C�e�����������Ă�����
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
        PickSystem();
    }


    void PickSystem()
    {
        ManualItem_Ray = PlayerCamera.ScreenPointToRay(CenterPos);
        Debug.DrawRay(ManualItem_Ray.origin, ManualItem_Ray.direction * ManualPickDistance, Color.red, 0.5f, false);
        LayerMask layerMask = 1;
        if (Physics.Raycast(ManualItem_Ray, out manualItem_Hit, ManualPickDistance, layerMask) && ManualItem_Hit.collider.tag == "Item")
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
    }
}

