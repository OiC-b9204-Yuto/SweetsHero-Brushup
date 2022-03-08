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
    [SerializeField] private GameObject Player; //プレイヤーの参照
    private string itemName; //アイテムの名称
    private bool isItemSpawn; //アイテムが出たかどうか
    public ItemType itemType; //アイテムのタイプ
    [SerializeField] private float itemDeleteTime; //アイテムの削除時間
    [SerializeField] private float itemDropChance; //アイテムのドロップ確率

    public float ItemDropChance { get { return itemDropChance; } } //アイテムのドロップ確率をGet出来るように
    public string ItemName { get { return itemName; } } //アイテムの名前を共有
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        isItemSpawn = true; //スクリプトが読み込まれたときに、アイテムスポーンをTrueにする
        switch (itemType) //アイテム名の指定
        {
            case ItemType.Bullet:
                itemName = "お菓子の弾";
                break;
            case ItemType.Heal:
                itemName = "体力回復剤";
                break;
        }
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

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PickArea")
        {
            switch (itemType)
            {
                case ItemType.Bullet:
                    Player.GetComponent<Character_State>().RecovAmmo(20);
                    Destroy(this.gameObject);
                    break;
                case ItemType.Heal:
                    IHeal character = Player.GetComponent<IHeal>();
                    character.TakeHeal(20);
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
