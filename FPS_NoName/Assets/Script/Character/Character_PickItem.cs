using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_PickItem : MonoBehaviour
{
    public enum ItemPickMode //アイテムピックモード
    {
        AutoPick,
        ManualPick,
    }

    private int ScreenCenterX; //画面の中央 X 座標を取得
    private int ScreenCenterY; //画面の中央 Y 座標を取得
    private Vector3 CenterPos; //画面の中央のベクトルを取得
    public ItemPickMode itemPickMode; //アイテムピックモードの参照
    [SerializeField] private Camera PlayerCamera; //プレイヤーカメラ
    [SerializeField] private float ManualPickDistance; //マニュアルピックのアイテムを取得できる距離
    private float changeCoolDown; //アイテムモード切り替えのクールタイム
    Ray ManualItem_Ray; 
    RaycastHit ManualItem_Hit;

    public float ChangeCoolDown { get { return changeCoolDown; } } //クールタイムを取得

    private void Awake()
    {
        ScreenCenterX = Screen.width / 2;
        ScreenCenterY = Screen.height / 2;
        CenterPos = new Vector3(ScreenCenterX, ScreenCenterY, 1.0f);
        ManualItem_Ray = PlayerCamera.ScreenPointToRay(CenterPos);
    }

    void Update()
    {
        if(changeCoolDown > 0.0f)
        {
            changeCoolDown -= Time.deltaTime;
        }
        ModeChange();
        ManualPickMode();
    }

    void ModeChange()
    {
        if (changeCoolDown > 0.0f)
        {
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

    void ManualPickMode() //手動ピック
    {
        if (itemPickMode == ItemPickMode.ManualPick) 
        {
            if (Physics.Raycast(ManualItem_Ray, out ManualItem_Hit, ManualPickDistance))
            {
                if (ManualItem_Hit.collider.tag == "Item")
                {
                    Debug.Log(ManualItem_Hit.collider.gameObject.name + "が見つかりました");
                }

            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Item")
        {
            if (itemPickMode == ItemPickMode.AutoPick) //自動ピックの場合
            {
                col.GetComponent<DropItem_Setting>().GetItem(); //ドロップアイテムのゲットitemを実行する
            }
        }
    }


}
