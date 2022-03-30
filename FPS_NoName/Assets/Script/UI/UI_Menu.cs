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
    UI_Option OptionSystem;                                 //�I�v�V�����̃V�X�e�����Q��
    OR_SceneManager or_Scene_Manager;                       //�V�[���J�ڗp

    [SerializeField] private int CurrentSelect;             //���C�����j���[�̌��ݑI��ł��鍀�ڗp��int
    [SerializeField] private int CurrentSelectExit;         //�I�����j���[�̌��ݑI��ł鍀�ڗp��int
    [SerializeField] private Image Exit_SelectYes;
    [SerializeField] private Image Exit_SelectNo;
    [SerializeField] private Image Exit_Yes;
    [SerializeField] private Image Exit_No;
    [SerializeField] private bool PushEsc;                  //ESC�L�[��������Ă��邩�A���Ȃ����̊m�F�p��bool
    [SerializeField] private bool FadeAnimtionEnd;          //�J���̃t�F�[�h�A�j���[�V�����̏I���m�F�p bool
    public bool CantSelectMenu;                             //�t�F�[�h����A���������s���Ă���Ƃ��Ƀ��C�����j���[��G�点�Ȃ��悤�ɂ���bool

    [SerializeField] private Image GameStartSelect_Button;  //�Q�[���X�^�[�g��I��ł���Ƃ��̃{�^���̉摜
    [SerializeField] private Image OptionSelect_Button;     //�I�v�V������I��ł���Ƃ��̃{�^���̉摜
    [SerializeField] private Image GameStart_Button;        //�Q�[���X�^�[�g��I��ł��Ȃ��Ƃ��̃{�^���̉摜
    [SerializeField] private Image Option_Button;           //�I�v�V������I��ł��Ȃ����̃{�^���̉摜

    [SerializeField] private Image Fade_FirstInMenu;               //�J���̃��C�����j���[�t�F�[�h�C���[�W
    [SerializeField] private Image Fade_MenuToOption;   //�I�v�V������ʂ։�ʑJ�ڂ���Ƃ��̃^�C�~���O�t�F�[�h�A�E�g
    [SerializeField] private Image Fade_InMenu;    //���C�����j���[�̃^�C�~���O�t�F�[�h�C��
    [SerializeField] private Image Fade_OutOpion;     //�I�v�V������ʂ̃^�C�~���O�t�F�[�h�A�E�g
    [SerializeField] private Image Fade_InOption;      //�I�v�V������ʂ̃^�C�~���O�t�F�[�h�C��

    [SerializeField] private GameObject MainMenuHUD;        //���C�����j���[HUD �p�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject ExitHUD;            //�I�����HUD �p�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject OptionHUD;          //�I�v�V�������HUD�@�p�̃Q�[���I�u�W�F�N�g

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

    void CheckStateAnimation()  //���C�����j���[�A�j���[�V�����Ǘ��֐�
    {
        if (Fade_FirstInMenu.GetComponent<UI_FadeImage>().animationState == AnimationState.FadeFinish) //�ŏ��̃t�F�[�h�A�j���[�V�������I�������Ƃ�
        {
            FadeAnimtionEnd = true;
        }

        if (Fade_MenuToOption.GetComponent<UI_FadeImage>().animationState == AnimationState.FadeFinish && 
            Fade_InOption.GetComponent<UI_FadeImage>().animationState != AnimationState.FadeFinish) //���C�����j���[����I�v�V������ʂւ̃t�F�[�h���I��������
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
            && PushEsc && currentScreen == CurrentScreen.Option) //�I�v�V������ʂ��烁�C�����j���[�֑J�ڂ���t�F�[�h�������I������Ƃ�
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


        //�J���̃t�F�[�h�A�j���[�V�������I����Ă��āACantSelectMenu��False�Ȃ烁�C�����j���[�𑀍�ł���
        if (FadeAnimtionEnd && currentScreen == CurrentScreen.Menu && (!CantSelectMenu))
        {
            switch (CurrentSelect)
            {
                case 0: //�Q�[���X�^�[�g�I����
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
                case 1: //�ݒ�I����
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
        //ESC�̓��͂����菮�����j���[�����ݐG���󋵂������ꍇ
        if (PushEsc && !CantSelectMenu)
        {
            if (currentScreen == CurrentScreen.Menu) //���C�����j���[��ESC���������ꍇ
            {
                ExitHUD.SetActive(true);
                MainMenuHUD.SetActive(false);
            }
            if (currentScreen == CurrentScreen.Option && !OptionSystem.isEnterMode) //�I�v�V������ʂ�ESC���������ꍇ
            {
                Fade_OutOpion.GetComponent<UI_FadeImage>().animationState = AnimationState.FadeStart;
            }
        }
        else
        {
            if (currentScreen == CurrentScreen.Exit) //�Q�[���I����ʂ�ESC���������ꍇ
            {
                ExitHUD.SetActive(false);
                MainMenuHUD.SetActive(true);
            }
        }

    }
}
