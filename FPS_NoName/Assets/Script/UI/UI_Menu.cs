using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum CurrentScreen{
    Menu,
    StageSelect,
    Option,
    Exit,
}
public class UI_Menu : MonoBehaviour
{
    public CurrentScreen currentScreen;
    UI_Option OptionSystem;                                 //オプションのシステムを参照
    OR_SceneManager or_Scene_Manager;                       //シーン遷移用

    [SerializeField] private int CurrentSelect;             //メインメニューの現在選んでいる項目用のint
    [SerializeField] private int CurrentSelectExit;         //終了メニューの現在選んでる項目用のint
    [SerializeField] private Image Exit_SelectYes;
    [SerializeField] private Image Exit_SelectNo;
    [SerializeField] private Image Exit_Yes;
    [SerializeField] private Image Exit_No;
    [SerializeField] private bool PushEsc;                  //ESCキーが押されているか、いないかの確認用のbool
    [SerializeField] private bool FadeAnimtionEnd;          //開幕のフェードアニメーションの終了確認用 bool
    public bool CantSelectMenu;                             //フェード中や、何かを実行しているときにメインメニューを触らせないようにするbool

    [SerializeField] private Image GameStartSelect_Button;  //ゲームスタートを選んでいるときのボタンの画像
    [SerializeField] private Image OptionSelect_Button;     //オプションを選んでいるときのボタンの画像
    [SerializeField] private Image GameStart_Button;        //ゲームスタートを選んでいないときのボタンの画像
    [SerializeField] private Image Option_Button;           //オプションを選んでいない時のボタンの画像

    [SerializeField] private Image Fade_FirstInMenu;               //開幕のメインメニューフェードイメージ
    [SerializeField] private Image Fade_MenuToOption;   //オプション画面へ画面遷移するときのタイミングフェードアウト
    [SerializeField] private Image Fade_InMenu;    //メインメニューのタイミングフェードイン
    [SerializeField] private Image Fade_OutOpion;     //オプション画面のタイミングフェードアウト
    [SerializeField] private Image Fade_InOption;      //オプション画面のタイミングフェードイン

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
        GameStartSelect_Button.enabled = true;
        OptionSelect_Button.enabled = false;
        GameStart_Button.enabled = false;
        Option_Button.enabled = true;
        OptionHUD.SetActive(false);
    }
    private void Update()
    {
        UpdateScreenInfo();
        CursorSystem();
        ExitCheckScreen();
        CheckStateAnimation();
        InputDir();
    }

    void UpdateScreenInfo()
    {
        if (MainMenuHUD.activeSelf)
        {
            currentScreen = CurrentScreen.Menu;
            Fade_InOption.GetComponent<UI_FadeImage>().animationState = AnimationState.FadeWait;
            Fade_OutOpion.GetComponent<UI_FadeImage>().animationState = AnimationState.FadeWait;
        }
        /*else if (StageSelectHUD.activeSelf)
        {
            currentScreen = CurrentScreen.StageSelect;
        }*/
        else if (OptionHUD.activeSelf)
        {
            currentScreen = CurrentScreen.Option;
            Fade_InMenu.GetComponent<UI_FadeImage>().animationState = AnimationState.FadeWait;
            Fade_MenuToOption.GetComponent<UI_FadeImage>().animationState = AnimationState.FadeWait;
        }
        else if (ExitHUD.activeSelf)
        {
            currentScreen = CurrentScreen.Exit;
        }

    }

    void CheckStateAnimation()  //メインメニューアニメーション管理関数
    {
        if (Fade_FirstInMenu.GetComponent<UI_FadeImage>().animationState == AnimationState.FadeFinish) //最初のフェードアニメーションが終了したとき
        {
            FadeAnimtionEnd = true;
        }

        if (Fade_MenuToOption.GetComponent<UI_FadeImage>().animationState == AnimationState.FadeFinish && 
            Fade_InOption.GetComponent<UI_FadeImage>().animationState != AnimationState.FadeFinish) //メインメニューからオプション画面へのフェードが終了した時
        {
            Fade_InOption.enabled = true;
            Fade_InOption.GetComponent<UI_FadeImage>().animationState = AnimationState.FadeStart;
            OptionHUD.SetActive(true);
            MainMenuHUD.SetActive(false);
        }

        if (Fade_InOption.GetComponent<UI_FadeImage>().animationState == AnimationState.FadeFinish)
        {
            CantSelectMenu = false;
        }

        if (Fade_InOption.GetComponent<UI_FadeImage>().animationState == AnimationState.FadeFinish 
            && PushEsc && currentScreen == CurrentScreen.Option) //オプション画面からメインメニューへ遷移するフェード処理が終わったとき
        {
            MainMenuHUD.SetActive(true);
            OptionHUD.SetActive(false);
            PushEsc = false;
        }
    }
    void CursorSystem()
    {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
    }

    void InputDir()
    {
        switch (currentScreen)
        {
            case CurrentScreen.Menu:
                if (Fade_FirstInMenu.GetComponent<UI_FadeImage>().animationState != AnimationState.FadeWait)
                {
                    if (Fade_FirstInMenu.GetComponent<UI_FadeImage>().animationState != AnimationState.FadeFinish)
                    {
                        return;
                    }
                }
                break;
            case CurrentScreen.Option:
                break;
        }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AudioManager.Instance.SE.PlayOneShot(MainMenuEnterSE);
                PushEsc = !PushEsc;
            }


        //開幕のフェードアニメーションが終わっていて、CantSelectMenuがFalseならメインメニューを操作できる
        if (FadeAnimtionEnd && currentScreen == CurrentScreen.Menu && (!CantSelectMenu))
        {
            switch (CurrentSelect)
            {
                case 0: //ゲームスタート選択時
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
                case 1: //設定選択時
                    GameStartSelect_Button.enabled = false;
                    OptionSelect_Button.enabled = true;
                    GameStart_Button.enabled = true;
                    Option_Button.enabled = false;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                       AudioManager.Instance.SE.PlayOneShot(MainMenuEnterSE);
                       CantSelectMenu = true;
                       Fade_MenuToOption.GetComponent<UI_FadeImage>().animationState = AnimationState.FadeStart;
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

        if (currentScreen == CurrentScreen.Exit)
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
            if (currentScreen == CurrentScreen.Menu) //メインメニューでESCを押した場合
            {
                ExitHUD.SetActive(true);
                MainMenuHUD.SetActive(false);
            }
            if (currentScreen == CurrentScreen.Option && !OptionSystem.isEnterMode) //オプション画面でESCを押した場合
            {
                Fade_OutOpion.GetComponent<UI_FadeImage>().animationState = AnimationState.FadeStart;
            }
        }
        else
        {
            if (currentScreen == CurrentScreen.Exit) //ゲーム終了画面でESCを押した場合
            {
                ExitHUD.SetActive(false);
                MainMenuHUD.SetActive(true);
            }
        }

    }
}
