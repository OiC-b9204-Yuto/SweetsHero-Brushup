using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Custom_InputManager : SingletonMonoBehaviour<Custom_InputManager>
{
    public const string DataFileName = "keyconfig.ini";    //キー配置保存用のファイル名
    public InputData inputData { get; private set; }

    private void Start()
    {
        Load();
    }

    public void AssignKeyConfig(string TargetKey, KeyCode ChangeKey) //キー設定のキーを変更する
    {
        
    }

    public void Save() //保存
    {
        string data = JsonUtility.ToJson(inputData,true);
        FileManager.Save(DataFileName, data);
    }

    public void Load() //読み込み
    {
        bool result;
        string data = FileManager.Load(DataFileName, out result);
        if (!result)
        {
            inputData = new InputData();
            Save();
            return;
        }
        inputData = JsonUtility.FromJson<InputData>(data);
    }
}


//キー保存データ用のクラス

[Serializable]
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
    public KeyCode Game_Map = KeyCode.M;                //全体マップを表示するキー
}
