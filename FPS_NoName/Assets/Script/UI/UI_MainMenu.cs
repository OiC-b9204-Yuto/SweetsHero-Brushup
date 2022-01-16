using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    UI_FadeImage FadeSystemToOption;
    UI_FadeImage FadeSystemOptionScreen;

    [SerializeField] private int CurrentSelect;

    [SerializeField] private bool PushEsc;
    [SerializeField] private bool FadeAnimtionEnd;
    [SerializeField] private bool CantSelectMenu;

    [SerializeField] private Image FadeImage;
    [SerializeField] private Image GameStartSelect_Button;
    [SerializeField] private Image OptionSelect_Button;
    [SerializeField] private Image GameStart_Button;
    [SerializeField] private Image Option_Button;
    [SerializeField] private Image TimingFadeOutToOption;
    [SerializeField] private Image TimingFadeInOption;

    [SerializeField] private GameObject MainMenuHUD;
    [SerializeField] private GameObject ExitHUD;
    [SerializeField] private GameObject OptionHUD;

    private void Awake()
    {
        CursorSystem();
        FadeSystemToOption = TimingFadeOutToOption.GetComponent<UI_FadeImage>();
        FadeSystemOptionScreen = TimingFadeInOption.GetComponent<UI_FadeImage>();
        GameStartSelect_Button.enabled = true;
        OptionSelect_Button.enabled = false;
        GameStart_Button.enabled = false;
        Option_Button.enabled = true;
        OptionHUD.SetActive(false);
    }
    private void Update()
    {
        CursorSystem();
        ExitCheckScreen();
        CheckStateAnimation();
        InputDir();
    }

    void CheckStateAnimation()
    {
        if (FadeSystemToOption.FinishFadeOUT)
        {
            CantSelectMenu = false;
            OptionHUD.SetActive(true);
            MainMenuHUD.SetActive(false);
            FadeSystemToOption.FinishFadeOUT = false;
        }

        if (FadeImage.fillAmount <= 0.0f)
        {
            FadeAnimtionEnd = true;
        }

        if (OptionHUD.activeSelf == true && !FadeSystemOptionScreen.FinishFadeIN)
        {
            FadeSystemOptionScreen.StartFadeImage = true;
        }
        else if (FadeSystemOptionScreen.FinishFadeIN || MainMenuHUD.activeSelf == true)
        {
            FadeSystemOptionScreen.StartFadeImage = false;
            FadeSystemOptionScreen.FinishFadeIN = false;
        }
    }
    void CursorSystem()
    {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
    }

    void InputDir()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PushEsc = !PushEsc;
        }

        if (FadeAnimtionEnd && MainMenuHUD.activeSelf == true && (!CantSelectMenu))
        {
            switch (CurrentSelect)
            {
                case 0:
                    GameStartSelect_Button.enabled = true;
                    OptionSelect_Button.enabled = false;
                    GameStart_Button.enabled = false;
                    Option_Button.enabled = true;
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        CurrentSelect = 1;
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        CurrentSelect = 1;
                    }
                    break;
                case 1:
                    GameStartSelect_Button.enabled = false;
                    OptionSelect_Button.enabled = true;
                    GameStart_Button.enabled = true;
                    Option_Button.enabled = false;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        CantSelectMenu = true;
                        FadeSystemToOption.StartFadeImage = true;
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        CurrentSelect = 0;
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        CurrentSelect = 0;
                    }
                    break;

            }
        }
    }

    void ExitCheckScreen()
    {
        if (PushEsc)
        {
            if (MainMenuHUD.activeSelf == true)
            {
                ExitHUD.SetActive(true);
                MainMenuHUD.SetActive(false);
            }
            if (OptionHUD.activeSelf == true)
            {
                MainMenuHUD.SetActive(true);
                OptionHUD.SetActive(false);
                PushEsc = false;
            }
        }
        else
        {
            if (ExitHUD.activeSelf == true)
            {
                ExitHUD.SetActive(false);
                MainMenuHUD.SetActive(true);
            }
        }

    }
}
