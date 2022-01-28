using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    UI_Option OptionSystem;                                 //オプションのシステムを参照
    OR_SceneManager or_Scene_Manager;                       //シーン遷移用
    UI_FadeImage FadeSystemToOption;                        //オプション画面へ画面遷移用の画像
    UI_FadeImage FadeSystemOptionScreen;                    //オプション画面のフェード用の画像
    UI_FadeImage FadeSystemFadeInOption;                    //メインメニューへ画面遷移用の画像
    UI_FadeImage FadeSystemFadeOutMainMenu;                 //メインメニューのフェードアウト用の画像

    [SerializeField] private int CurrentSelect;             //メインメニューの現在選んでいる項目用のint
    [SerializeField] private int CurrentSelectExit;         //終了メニューの現在選んでる項目用のint
    [SerializeField] private Image Exit_SelectYes;
    [SerializeField] private Image Exit_SelectNo;
    [SerializeField] private Image Exit_Yes;
    [SerializeField] private Image Exit_No;
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

    [SerializeField] private AudioClip MainMenuMusic;
    [SerializeField] private AudioClip MainMenuChangeColumnSE;
    [SerializeField] private AudioClip MainMenuEnterSE;

    private void Awake()
    {
        CursorSystem();
        AudioManager.Instance.Load();
        AudioManager.Instance.BGM.clip = MainMenuMusic;
        AudioManager.Instance.BGM.Play();
        or_Scene_Manager = this.GetComponent<OR_SceneManager>();
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
        if (FadeImage.fillAmount <= 0.1f && !FadeSystemFadeInOption.StartFadeImage && !FadeSystemFadeOutMainMenu.StartFadeImage
            && !FadeSystemOptionScreen.StartFadeImage && !FadeSystemToOption.StartFadeImage && !OptionSystem.isApplyShown)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AudioManager.Instance.SE.PlayOneShot(MainMenuEnterSE);
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
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        AudioManager.Instance.SE.PlayOneShot(MainMenuEnterSE);
                        or_Scene_Manager.SceneName = "MainGame";
                        or_Scene_Manager.NextSceneLoad();
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
                        CurrentSelect = 1;
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
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
                        AudioManager.Instance.SE.PlayOneShot(MainMenuEnterSE);
                        CantSelectMenu = true;
                        FadeSystemToOption.StartFadeImage = true;
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
                        CurrentSelect = 0;
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
                        CurrentSelect = 0;
                    }
                    break;

            }
        }

        if (ExitHUD.activeSelf)
        {
            switch (CurrentSelectExit)
            {
                case 0:
                    Exit_SelectNo.enabled = false;
                    Exit_Yes.enabled = false;
                    Exit_SelectYes.enabled = true;
                    Exit_No.enabled = true;
                    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        CurrentSelectExit = 1;
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                        #endif
                        #if UNITY_STANDALONE
                        Application.Quit();
                        #endif
                    }
                    break;
                case 1:
                    Exit_SelectNo.enabled = true;
                    Exit_Yes.enabled = true;
                    Exit_SelectYes.enabled = false;
                    Exit_No.enabled = false;
                    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        CurrentSelectExit = 0;
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        MainMenuHUD.SetActive(true);
                        PushEsc = false;
                        ExitHUD.SetActive(false);
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
