using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    [SerializeField] private GameObject[] DropItemList;
    EnemyInfo Enemy;
    int RandomItemValue;
    int DropPersentValue;
    bool IsItemDrop;
    private void Awake()
    {
        Enemy = this.GetComponent<EnemyInfo>();
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
            return;
        }

        if (Enemy.Enemy_Health <= 0.0f && IsItemDrop)
        {
            GameObject DropInstanceObject = (GameObject)Instantiate(DropItemList[RandomItemValue], new Vector3(this.transform.position.x,this.transform.position.y+16,this.transform.position.z),Quaternion.identity);
        }
    }

}
