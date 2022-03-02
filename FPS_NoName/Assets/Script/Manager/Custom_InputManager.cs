using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Custom_InputManager : SingletonMonoBehaviour<Custom_InputManager>
{
    public const string DataFileName = "keyconfig.ini";    //�L�[�z�u�ۑ��p�̃t�@�C����
    public InputData inputData { get; private set; }

    private void Start()
    {
        Load();
    }

    public void Save() //�ۑ�
    {
        string data = JsonUtility.ToJson(inputData,true);
        FileManager.Save(DataFileName, data);
    }

    public void Load() //�ǂݍ���
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
