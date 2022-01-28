using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_KeyInfo : MonoBehaviour
{
    Character_Info CharaInfo;
    [SerializeField] private GameObject PlayerObject;
    [SerializeField] private Image[] Key_ActiveBG;
    [SerializeField] private Image[] Key_Icon;
    private void Awake()
    {
        CharaInfo = PlayerObject.GetComponent<Character_Info>();
    }

    void Update()
    {
        for(int i = 0;i < CharaInfo.Character_GetKeys; i++)
        {
            Key_ActiveBG[i].enabled = true;
            Key_Icon[i].enabled = true;
        }
    }
}
