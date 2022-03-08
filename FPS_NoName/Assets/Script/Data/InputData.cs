using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//キー保存データ用のクラス
[System.Serializable]
public class InputData
{
    //設定出来るキー
    public KeyCode Move_Forward = KeyCode.W;            //前へ移動するキー
    public KeyCode Move_Back = KeyCode.S;               //後ろへ移動するキー
    public KeyCode Move_Left = KeyCode.A;               //左へ移動するキー
    public KeyCode Move_Right = KeyCode.D;              //右へ移動するキー
    public KeyCode Chara_Fire = KeyCode.Mouse0;         //銃を射撃するキー
    public KeyCode Chara_Grenade = KeyCode.G;           //グレネードを投げるキー
    public KeyCode Chara_Reload = KeyCode.R;            //リロードするキー
    public KeyCode Chara_Interact = KeyCode.E;          //オブジェクトにインタラクトするキー
    public KeyCode Chara_PickModeSwitch = KeyCode.V;    //ピックモードをチェンジするキー
    public KeyCode Game_Map = KeyCode.M;                //全体マップを表示するキー
}
