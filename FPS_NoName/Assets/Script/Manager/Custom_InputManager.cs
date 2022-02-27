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

    public void AssignKeyConfig(string TargetKey, KeyCode ChangeKey) //�L�[�ݒ�̃L�[��ύX����
    {
        
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


//�L�[�ۑ��f�[�^�p�̃N���X

[Serializable]
public class InputData
{
    //�ݒ�o����L�[
    public KeyCode Move_Forward = KeyCode.W;            //�O�ֈړ�����L�[
    public KeyCode Move_Back = KeyCode.S;               //���ֈړ�����L�[
    public KeyCode Move_Left = KeyCode.A;               //���ֈړ�����L�[
    public KeyCode Move_Right = KeyCode.D;              //�E�ֈړ�����L�[
    public KeyCode Chara_Fire = KeyCode.Mouse0;         //�e���ˌ�����L�[
    public KeyCode Chara_Grenade = KeyCode.G;           //�O���l�[�h�𓊂���L�[
    public KeyCode Chara_Reload = KeyCode.R;            //�����[�h����L�[
    public KeyCode Game_Map = KeyCode.M;                //�S�̃}�b�v��\������L�[
}
