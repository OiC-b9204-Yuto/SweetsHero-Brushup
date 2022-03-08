using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType //�A�C�e���̃^�C�v
{
    Bullet,
    Heal
}

public class DropItem_Setting : MonoBehaviour
{
    private bool isItemSpawn; //�A�C�e�����o�����ǂ���
    private GameObject Character; //�v���C���[�̎Q��
    public ItemType itemType; //�A�C�e���̃^�C�v
    [SerializeField] private float itemDeleteTime; //�A�C�e���̍폜����
    [SerializeField] private float itemDropChance; //�A�C�e���̃h���b�v�m��

    public float ItemDropChance { get { return itemDropChance; } } //�A�C�e���̃h���b�v�m����Get�o����悤��

    void Awake()
    {   
        Character = GameObject.FindGameObjectWithTag("Player");
        isItemSpawn = true; //�X�N���v�g���ǂݍ��܂ꂽ�Ƃ��ɁA�A�C�e���X�|�[����True�ɂ���
    }

    void Update()
    {
        DeleteTimer();
    }

    void DeleteTimer() //�A�C�e���̍폜
    {
        if (!isItemSpawn)
        {
            Destroy(this.gameObject);
            return;
        }
        if (isItemSpawn)
        {
            if (itemDeleteTime > 0.0f)
            {
                itemDeleteTime -= Time.deltaTime;
            }
            else
            {
                isItemSpawn = false;
            }
        }
    }

    public void GetItem()
    {
        switch (itemType)
        {
            case ItemType.Bullet:
                Character.GetComponent<Character_State>().RecovAmmo(20);
                break;
            case ItemType.Heal:
                IHeal character = Character.GetComponent<IHeal>();
                character.TakeHeal(20);
                break;
        }
    }
}
