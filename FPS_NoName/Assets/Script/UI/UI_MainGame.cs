using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainGameManage {
    public class UI_MainGame : MonoBehaviour
    {
        [SerializeField] private Image Health_Bar;
        [SerializeField] private Image Armor_Bar;
        [SerializeField] private Text Weapon_CurrentAmmoText;
        [SerializeField] private Text Weapon_CurrentMagazineText;
        Character_Info CharacterInfo;
        MainGameManager MainGame_Manager;
        Weapon_State Weapon_Stats;
        void Start()
        {
            //MainGame_Manager = GameObject.Find("ManagerObject").GetComponent<MainGameManager>();
            CharacterInfo = GameObject.Find("TestCharacter").GetComponent<Character_Info>();
            Weapon_Stats = GameObject.Find("Weapon").GetComponent<Weapon_State>();
        }

        void Update()
        {
            RefreshUIText();
            InputDir();
        }

        void RefreshUIText()
        {
            Health_Bar.fillAmount = CharacterInfo.Character_CurrentHP / CharacterInfo.Character_MaxHP;
            Armor_Bar.fillAmount = CharacterInfo.Character_CurrentArmor / CharacterInfo.Character_MaxArmor;
            Weapon_CurrentAmmoText.text = Weapon_Stats.Weapon_CurrentAmmo.ToString("00");
            Weapon_CurrentMagazineText.text = Weapon_Stats.Weapon_CurrentMagazine.ToString("000");
        }

        void InputDir()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //MainGame_Manager.MainGame_IsPause = !MainGame_Manager.MainGame_IsPause;
            }
        }
    }
}
