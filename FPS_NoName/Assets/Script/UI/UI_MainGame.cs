using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainGameManage {
    public class UI_MainGame : MonoBehaviour
    {
        [SerializeField] private GameObject PlayerIcon1;
        [SerializeField] private GameObject PlayerIcon2;
        [SerializeField] private GameObject PlayerIcon3;
        [SerializeField] private Image Health_Bar;
        [SerializeField] private Image Health_Bar_Low1;
        [SerializeField] private Image Health_Bar_Low2;
        [SerializeField] private Image Armor_Bar;
        [SerializeField] private Text Weapon_CurrentAmmoText;
        [SerializeField] private Text Weapon_CurrentMagazineText;
        Character_Info CharacterInfo;
        MainGameManager MainGame_Manager;
        Weapon_State Weapon_Stats;

        //ゲームオーバー用
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private Image BG;
        [SerializeField] private Image GameOverBG;
        [SerializeField] private Image GameOverLOGO;
        [SerializeField] private Image GameOver_RetrySelect;
        [SerializeField] private Image GameOver_Retry;
        [SerializeField] private Image GameOver_ExitSelect;
        [SerializeField] private Image GameOver_Exit;
        [SerializeField] private float FadeSpeed;
        [SerializeField] private float MoveSpeed;
        [SerializeField] private RectTransform GameOverLogo_POS;
        Vector2 GameOverLogo_Vec;
        [SerializeField] private bool GameOver_FinishAnim;
        [SerializeField] private int CurrentSelect;
        void Awake()
        {
            //MainGame_Manager = GameObject.Find("ManagerObject").GetComponent<MainGameManager>();

            Health_Bar_Low1.enabled = false;
            Health_Bar_Low2.enabled = false;

            GameOverUI.SetActive(false);

            GameOverLogo_Vec = new Vector2(0,-125);
            GameOverLogo_POS.anchoredPosition = GameOverLogo_Vec;

            BG.enabled = false;
            GameOverBG.enabled = false;
            GameOverLOGO.enabled = false;
            GameOver_RetrySelect.enabled = false;
            GameOver_Retry.enabled = false;
            GameOver_ExitSelect.enabled = false;
            GameOver_Exit.enabled = false;

            BG.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            GameOverBG.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            GameOverLOGO.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            GameOver_RetrySelect.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            GameOver_Retry.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            GameOver_ExitSelect.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            GameOver_Exit.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

            CharacterInfo = GameObject.Find("Player").GetComponent<Character_Info>();
            Weapon_Stats = GameObject.Find("Weapon").GetComponent<Weapon_State>();
        }

        void Update()
        {
            HealthValueChangeIcon();
            RefreshUIText();
            InputDir();
            GameOver_System();
        }

        void RefreshUIText()
        {
            Health_Bar.fillAmount = CharacterInfo.Character_CurrentHP / CharacterInfo.Character_MaxHP;
            Health_Bar_Low1.fillAmount = CharacterInfo.Character_CurrentHP / CharacterInfo.Character_MaxHP;
            Health_Bar_Low2.fillAmount = CharacterInfo.Character_CurrentHP / CharacterInfo.Character_MaxHP;
            Armor_Bar.fillAmount = CharacterInfo.Character_CurrentArmor / CharacterInfo.Character_MaxArmor;
            Weapon_CurrentAmmoText.text = Weapon_Stats.Weapon_CurrentAmmo.ToString("00");
            Weapon_CurrentMagazineText.text = Weapon_Stats.Weapon_CurrentMagazine.ToString("000");
        }

        void HealthValueChangeIcon()
        {
            if (Health_Bar.fillAmount >= 0.5f)
            {
                Health_Bar.enabled = true;
                Health_Bar_Low1.enabled = false;
                Health_Bar_Low2.enabled = false;
                PlayerIcon1.SetActive(true);
                PlayerIcon2.SetActive(false);
                PlayerIcon3.SetActive(false);
            }
            else if (Health_Bar.fillAmount >= 0.25f)
            {
                Health_Bar.enabled = false;
                Health_Bar_Low1.enabled = true;
                Health_Bar_Low2.enabled = false;
                PlayerIcon1.SetActive(false);
                PlayerIcon2.SetActive(true);
                PlayerIcon3.SetActive(false);
            }
            else if (Health_Bar.fillAmount >= 0.0f)
            {
                Health_Bar.enabled = false;
                Health_Bar_Low1.enabled = false;
                Health_Bar_Low2.enabled = true;
                PlayerIcon1.SetActive(false);
                PlayerIcon2.SetActive(false);
                PlayerIcon3.SetActive(true);
            }
        }

        void GameOver_System()
        {
            if (CharacterInfo.Character_CurrentHP <= 0)
            {
                GameOverUI.SetActive(true);
                BG.enabled = true;
                GameOverBG.enabled = true;
                GameOverLOGO.enabled = true;
                if (BG.color.a <= 0.8f)
                {
                    BG.color = new Color(0.0f, 0.0f, 0.0f, BG.color.a + FadeSpeed * Time.deltaTime);
                }
                GameOverBG.color = new Color(1.0f, 1.0f, 1.0f, GameOverBG.color.a + FadeSpeed * Time.deltaTime);
                GameOverLOGO.color = new Color(1.0f, 1.0f, 1.0f, GameOverLOGO.color.a + FadeSpeed * Time.deltaTime);

                if (GameOverLOGO.color.a >= 1.0f)
                {
                    GameOver_RetrySelect.enabled = true;
                    GameOver_Exit.enabled = true;

                    if (GameOverLogo_POS.anchoredPosition.y >= 0.0f)
                    {
                        GameOverLogo_POS.anchoredPosition = new Vector2(0.0f, 0.0f);
                        GameOver_FinishAnim = true;
                    }
                    else
                    {
                        GameOverLogo_Vec = new Vector2(0, GameOverLogo_Vec.y + MoveSpeed * Time.deltaTime);
                        GameOver_RetrySelect.color = new Color(1.0f, 1.0f, 1.0f, GameOver_RetrySelect.color.a + 1.0f / (125.0f / MoveSpeed) * Time.deltaTime);
                        GameOver_Exit.color = new Color(1.0f, 1.0f, 1.0f, GameOver_Exit.color.a + 1.0f / (125.0f / MoveSpeed) * Time.deltaTime);
                        GameOverLogo_POS.anchoredPosition = GameOverLogo_Vec;
                    }
                }
            }

            if (GameOver_FinishAnim)
            {
                switch (CurrentSelect)
                {
                    case 0:
                        GameOver_RetrySelect.enabled = true;
                        GameOver_Exit.enabled = true;
                        GameOver_ExitSelect.enabled = false;
                        GameOver_Retry.enabled = false;
                        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            CurrentSelect++;
                        }
                        break;
                    case 1:
                        GameOver_ExitSelect.enabled = true;
                        GameOver_Retry.enabled = true;
                        GameOver_RetrySelect.enabled = false;
                        GameOver_Exit.enabled = false;
                        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            CurrentSelect--;
                        }
                        break;
                }

            }
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
