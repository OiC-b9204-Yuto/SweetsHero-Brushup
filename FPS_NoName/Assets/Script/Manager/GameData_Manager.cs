using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData_Manager : SingletonMonoBehaviour<GameData_Manager>
{
    public const string DataFileName = "GameData.save";    //�L�[�z�u�ۑ��p�̃t�@�C����
    public GameData gameData { get; private set; }

    private void Start()
    {
        Load();
    }

    public void Save() //�ۑ�
    {
        string data = JsonUtility.ToJson(gameData, true);
        FileManager.Save(DataFileName, data);
    }

    public void Load() //�ǂݍ���
    {
        bool result;
        string data = FileManager.Load(DataFileName, out result);
        if (!result)
        {
            gameData = new GameData();
            Save();
            return;
        }
        gameData = JsonUtility.FromJson<GameData>(data);
    }

}
