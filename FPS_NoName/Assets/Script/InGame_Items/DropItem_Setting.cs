using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType //アイテムのタイプ
{
    Bullet,
    Heal
}

public class DropItem_Setting : MonoBehaviour
{
    private bool isItemSpawn; //アイテムが出たかどうか
    private GameObject Character; //プレイヤーの参照
    public ItemType itemType; //アイテムのタイプ
    [SerializeField] private float itemDeleteTime; //アイテムの削除時間
    [SerializeField] private float itemDropChance; //アイテムのドロップ確率

    public float ItemDropChance { get { return itemDropChance; } } //アイテムのドロップ確率をGet出来るように

    void Awake()
    {   
        Character = GameObject.FindGameObjectWithTag("Player");
        isItemSpawn = true; //スクリプトが読み込まれたときに、アイテムスポーンをTrueにする
    }

    void Update()
    {
        DeleteTimer();
    }

    void DeleteTimer() //アイテムの削除
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
