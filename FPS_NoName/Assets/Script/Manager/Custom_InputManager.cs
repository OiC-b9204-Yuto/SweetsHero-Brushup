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
