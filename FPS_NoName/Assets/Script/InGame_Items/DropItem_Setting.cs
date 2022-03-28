using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType //�A�C�e���̃^�C�v
{
    Bullet,
    Heal,
    Key,
}

public class DropItem_Setting : MonoBehaviour
{
    [SerializeField] private GameObject Player; //�v���C���[�̎Q��
    private string itemName; //�A�C�e���̖���
    private bool isItemSpawn; //�A�C�e�����o�����ǂ���
    public ItemType itemType; //�A�C�e���̃^�C�v
    public ParticleSystem ItemSpawnEffect; //�A�C�e���o�����̃G�t�F�N�g
    [SerializeField] private float itemDeleteTime; //�A�C�e���̍폜����
    [SerializeField] private float itemDropChance; //�A�C�e���̃h���b�v�m��
    [SerializeField] private AudioClip itemSoundEffect; //�A�C�e���̊l�����̉����z��

    public float ItemDropChance { get { return itemDropChance; } } //�A�C�e���̃h���b�v�m����Get�o����悤��
    public string ItemName { get { return itemName; } } //�A�C�e���̖��O�����L
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        isItemSpawn = true; //�X�N���v�g���ǂݍ��܂ꂽ�Ƃ��ɁA�A�C�e���X�|�[����True�ɂ���
        switch (itemType) //�A�C�e�����̎w��
        {
            case ItemType.Bullet:
                itemName = "���َq�̒e";
                break;
            case ItemType.Heal:
                itemName = "�̗͉񕜍�";
                break;
            case ItemType.Key:
                itemName = "��";
                break;
        }
    }

    void Update()
    {
        DeleteTimer();
    }

    void DeleteTimer() //�A�C�e���̍폜
    {
        if (itemType == ItemType.Key)
        {
            return;
        }
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
                AudioManager.Instance.SE.PlayOneShot(itemSoundEffect);
                Player.GetComponent<Character_State>().RecovAmmo(20);
                Destroy(this.gameObject);
                break;
            case ItemType.Heal:
                AudioManager.Instance.SE.PlayOneShot(itemSoundEffect);
                IHeal character = Player.GetComponent<IHeal>();
                character.TakeHeal(20);
                Destroy(this.gameObject);
                break;
            case ItemType.Key:
                AudioManager.Instance.SE.PlayOneShot(itemSoundEffect);
                Player.GetComponent<Character_Info>().Character_GetKeys++;
                Destroy(this.gameObject);
                break;
        }
    }
}
