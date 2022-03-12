using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    [SerializeField] private GameObject[] DropItemList;
    BaseEnemy Enemy;
    [SerializeField] private int RandomItemValue;
    [SerializeField] private int DropPersentValue;
    [SerializeField] private bool IsItemDrop;
    private void Awake()
    {
        Enemy = this.GetComponent<BaseEnemy>();
        //�A�C�e�����h���b�v����̂��A���̃A�C�e�����h���b�v����̂��̊m��
        RandomItemValue = Random.Range(0, DropItemList.Length);
        DropPersentValue = Random.Range(0, 100);
        if(DropPersentValue <= DropItemList[RandomItemValue].GetComponent<DropItem_Setting>().ItemDropChance) //�����_���ŏo�����l���� �A�C�e���h���b�v�`�����X���̕���������΃h���b�v
        {
            IsItemDrop = true;
        }
        else
        {
            IsItemDrop = false;
        }
    }

    void Update()
    {
        DropItem();
    }
    void DropItem()
    {
        if (!IsItemDrop)
        {
            this.enabled = false;
        }

        if (Enemy.CurrentHealth <= 0.0f && IsItemDrop)
        {
            GameObject DropInstanceObject = (GameObject)Instantiate(DropItemList[RandomItemValue], this.gameObject.transform.position,Quaternion.identity);
            this.enabled = false;
        }
    }

}
