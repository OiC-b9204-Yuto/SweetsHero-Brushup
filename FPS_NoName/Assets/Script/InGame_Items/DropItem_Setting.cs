using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType //アイテムのタイプ
{
    Bullet,
    Heal,
    Key,
}

public class DropItem_Setting : MonoBehaviour
{
    [SerializeField] private GameObject Player; //プレイヤーの参照
    private string itemName; //アイテムの名称
    private bool isItemSpawn; //アイテムが出たかどうか
    public ItemType itemType; //アイテムのタイプ
    public ParticleSystem ItemSpawnEffect; //アイテム出現時のエフェクト
    [SerializeField] private float itemDeleteTime; //アイテムの削除時間
    [SerializeField] private float itemDropChance; //アイテムのドロップ確率
    [SerializeField] private AudioClip itemSoundEffect; //アイテムの獲得時の音源配列

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
            case ItemType.Key:
                itemName = "鍵";
                break;
        }
    }

    void Update()
    {
        DeleteTimer();
    }

    void DeleteTimer() //アイテムの削除
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
