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
    private Ray ManualItem_Ray;  //アイテムにレイを飛ばす
    private RaycastHit manualItem_Hit; //レイにアイテムが当たっている情報
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
        PickSystem();
    }


    void PickSystem()
    {
        ManualItem_Ray = PlayerCamera.ScreenPointToRay(CenterPos);
        Debug.DrawRay(ManualItem_Ray.origin, ManualItem_Ray.direction * ManualPickDistance, Color.red, 0.5f, false);
        LayerMask layerMask = 1;
        if (Physics.Raycast(ManualItem_Ray, out manualItem_Hit, ManualPickDistance, layerMask) && ManualItem_Hit.collider.tag == "Item")
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
    }
}

