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
    [SerializeField] private SphereCollider AutoPickArea; //自動ピックエリア
    private float changeCoolDown; //アイテムモード切り替えのクールタイム
    private Ray ManualItem_Ray;  //アイテムにレイを飛ばす
    private RaycastHit manualItem_Hit; //レイにアイテムが当たっている情報
    public float ChangeCoolDown { get { return changeCoolDown; } } //クールタイム取得用
    public RaycastHit ManualItem_Hit { get { return manualItem_Hit; } } //アイテム情報取得用
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
        ModeChange();
        PickSystem();
    }

    void ModeChange()
    {
        if (changeCoolDown > 0.0f)
        {
            changeCoolDown -= Time.deltaTime;
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

    void PickSystem()
    {
        switch (itemPickMode)
        {
            case ItemPickMode.AutoPick: //自動ピックの場合
                AutoPickArea.enabled = true;
                break;
            case ItemPickMode.ManualPick: //手動ピックの場合
                ManualItem_Ray = PlayerCamera.ScreenPointToRay(CenterPos);
                Debug.DrawRay(ManualItem_Ray.origin, ManualItem_Ray.direction * ManualPickDistance, Color.red, 0.5f, false);
                AutoPickArea.enabled = false;
                if (Physics.Raycast(ManualItem_Ray, out manualItem_Hit, ManualPickDistance) && ManualItem_Hit.collider.tag == "Item")
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
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Item")
        {
            col.GetComponent<DropItem_Setting>().GetItem();
        }
    }
}
