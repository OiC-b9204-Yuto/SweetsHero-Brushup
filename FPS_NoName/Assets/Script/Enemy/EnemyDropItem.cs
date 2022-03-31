using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    [SerializeField] private GameObject[] DropItemList;
    EnemyBase Enemy;
    [SerializeField] private int RandomItemValue;
    [SerializeField] private int DropPersentValue;
    [SerializeField] private bool IsItemDrop;
    private void Awake()
    {
        Enemy = this.GetComponent<EnemyBase>();
        //アイテムがドロップするのか、何のアイテムがドロップするのかの確定
        RandomItemValue = Random.Range(0, DropItemList.Length);
        DropPersentValue = Random.Range(0, 100);
        if(DropPersentValue <= DropItemList[RandomItemValue].GetComponent<DropItem_Setting>().ItemDropChance) //ランダムで出た数値よりも アイテムドロップチャンス率の方が高ければドロップ
        {
            Enemy.OnEnemyDead += () => DropItem();
            IsItemDrop = true;
        }
        else
        {
            IsItemDrop = false;
        }

    }

    void DropItem()
    {
        if (Enemy.CurrentHealth <= 0.0f && IsItemDrop)
        {
            GameObject DropInstanceObject = (GameObject)Instantiate(DropItemList[RandomItemValue], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z), Quaternion.identity);
            DropItemList[RandomItemValue].GetComponent<DropItem_Setting>().ItemSpawnEffect.Play();
            this.enabled = false;
        }
    }

}
