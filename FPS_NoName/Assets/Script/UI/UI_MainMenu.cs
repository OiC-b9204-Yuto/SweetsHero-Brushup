using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    UI_Option OptionSystem;                                 //オプションのシステムを参照
    UI_FadeImage FadeSystemToOption;                        //オプション画面へ画面遷移用の画像
    UI_FadeImage FadeSystemOptionScreen;                    //オプション画面のフェード用の画像
    UI_FadeImage FadeSystemFadeInOption;                    //メインメニューへ画面遷移用の画像
    UI_FadeImage FadeSystemFadeOutMainMenu;                 //メインメニューのフェードアウト用の画像

    [SerializeField] private int CurrentSelect;             //メインメニューの現在選んでいる項目用のint

    [SerializeField] private bool PushEsc;                  //ESCキーが押されているか、いないかの確認用のbool
    [SerializeField] private bool FadeAnimtionEnd;          //開幕のフェードアニメーションの終了確認用 bool
    public bool CantSelectMenu;                             //フェード中や、何かを実行しているときにメインメニューを触らせないようにするbool

    [SerializeField] private Image FadeImage;               //開幕のメインメニューフェードイメージ
    [SerializeField] private Image GameStartSelect_Button;  //ゲームスタートを選んでいるときのボタンの画像
    [SerializeField] private Image OptionSelect_Button;     //オプションを選んでいるときのボタンの画像
    [SerializeField] private Image GameStart_Button;        //ゲームスタートを選んでいないときのボタンの画像
    [SerializeField] private Image Option_Button;           //オプションを選んでいない時のボタンの画像

    [SerializeField] private Image TimingFadeOutToOption;   //オプション画面へ画面遷移するときのタイミングフェードアウト
    [SerializeField] private Image TimingFadeOutOption;     //オプション画面のタイミングフェードアウト
    [SerializeField] private Image TimingFadeInOption;      //オプション画面のタイミングフェードイン
    [SerializeField] private Image TimingFadeInMainMenu;    //メインメニューのタイミングフェードイン

    [SerializeField] private GameObject MainMenuHUD;        //メインメニューHUD 用のゲームオブジェクト
    [SerializeField] private GameObject ExitHUD;            //終了画面HUD 用のゲームオブジェクト
    [SerializeField] private GameObject OptionHUD;          //オプション画面HUD　用のゲームオブジェクト

    private void Awake()
    {
        CursorSystem();
        OptionSystem = OptionHUD.GetComponent<UI_Option>();
        FadeSystemToOption = TimingFadeOutToOption.GetComponent<UI_FadeImage>();
        FadeSystemOptionScreen = TimingFadeOutOption.GetComponent<UI_FadeImage>();
        FadeSystemFadeInOption = TimingFadeInOption.GetComponent<UI_FadeImage>();
        FadeSystemFadeOutMainMenu = TimingFadeInMainMenu.GetComponent<UI_FadeImage>();
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

    void CheckStateAnimation()  //メインメニューアニメーション管理関数
    {

        if (FadeSystemToOption.FinishFadeOUT)
        {
            OptionHUD.SetActive(true);
            MainMenuHUD.SetActive(false);
            FadeSystemToOption.FinishFadeOUT = false;
        }

        if (FadeImage.fillAmount <= 0.0f)
        {
            FadeAnimtionEnd = true;
        }

        if (TimingFadeInOption.fillAmount >= 0.98f && PushEsc && OptionHUD.activeSelf == true)
        {
            MainMenuHUD.SetActive(true);
            OptionHUD.SetActive(false);
            PushEsc = false;
        }

        if (OptionHUD.activeSelf == true && !FadeSystemOptionScreen.FinishFadeIN)
        {
            FadeSystemOptionScreen.StartFadeImage = true;
            FadeSystemFadeOutMainMenu.FinishFadeIN = false;
        }
        else if (MainMenuHUD.activeSelf == true)
        {
            if (TimingFadeInMainMenu.enabled|| TimingFadeOutToOption.enabled) 
            {
                    CantSelectMenu = true;
                    
            }
            else if (!TimingFadeInMainMenu.enabled || !TimingFadeOutToOption.enabled)
            {
                CantSelectMenu = false;
            }
            if (FadeSystemFadeInOption.FinishFadeOUT)
            {
                FadeSystemFadeOutMainMenu.StartFadeImage = true;
                FadeSystemFadeInOption.FinishFadeOUT = false;
            }
            FadeSystemOptionScreen.StartFadeImage = false;
            FadeSystemOptionScreen.FinishFadeIN = false;
            FadeSystemFadeInOption.FinishFadeOUT = false;
            FadeSystemFadeInOption.StartFadeImage = false;
        }
        else if (OptionHUD.activeSelf == true)
        {
            if (TimingFadeInOption.enabled)
            {
                CantSelectMenu = true;

            }
            else if (!TimingFadeInOption.enabled || !TimingFadeOutToOption.enabled)
            {
                CantSelectMenu = false;
            }
        }
    }
    void CursorSystem()
    {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
    }

    void InputDir()
    {
        //フェードアニメーション実行中は、ESCキーを実行させない
        if (FadeImage.fillAmount <= 0.1f && !FadeSystemFadeInOption.StartFadeImage && !FadeSystemFadeOutMainMenu.StartFadeImage && !FadeSystemOptionScreen.StartFadeImage && !FadeSystemToOption.StartFadeImage) 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PushEsc = !PushEsc;
            }
        }

        //開幕のフェードアニメーションが終わっていて、CantSelectMenuがFalseならメインメニューを操作できる
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
        //ESCの入力があり尚且つメニューが現在触れる状況だった場合
        if (PushEsc && !CantSelectMenu)
        {
            if (MainMenuHUD.activeSelf == true) //メインメニューでESCを押した場合
            {
                ExitHUD.SetActive(true);
                MainMenuHUD.SetActive(false);
            }
            if (OptionHUD.activeSelf == true && !OptionSystem.isEnterMode) //オプション画面でESCを押した場合
            {
                FadeSystemFadeInOption.StartFadeImage = true;
            }
        }
        else
        {
            if (ExitHUD.activeSelf == true) //ゲーム終了画面でESCを押した場合
            {
                ExitHUD.SetActive(false);
                MainMenuHUD.SetActive(true);
            }
        }

    }
}
