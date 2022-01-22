using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    UI_Option OptionSystem;                                 //�I�v�V�����̃V�X�e�����Q��
    UI_FadeImage FadeSystemToOption;                        //�I�v�V������ʂ։�ʑJ�ڗp�̉摜
    UI_FadeImage FadeSystemOptionScreen;                    //�I�v�V������ʂ̃t�F�[�h�p�̉摜
    UI_FadeImage FadeSystemFadeInOption;                    //���C�����j���[�։�ʑJ�ڗp�̉摜
    UI_FadeImage FadeSystemFadeOutMainMenu;                 //���C�����j���[�̃t�F�[�h�A�E�g�p�̉摜

    [SerializeField] private int CurrentSelect;             //���C�����j���[�̌��ݑI��ł��鍀�ڗp��int

    [SerializeField] private bool PushEsc;                  //ESC�L�[��������Ă��邩�A���Ȃ����̊m�F�p��bool
    [SerializeField] private bool FadeAnimtionEnd;          //�J���̃t�F�[�h�A�j���[�V�����̏I���m�F�p bool
    public bool CantSelectMenu;                             //�t�F�[�h����A���������s���Ă���Ƃ��Ƀ��C�����j���[��G�点�Ȃ��悤�ɂ���bool

    [SerializeField] private Image FadeImage;               //�J���̃��C�����j���[�t�F�[�h�C���[�W
    [SerializeField] private Image GameStartSelect_Button;  //�Q�[���X�^�[�g��I��ł���Ƃ��̃{�^���̉摜
    [SerializeField] private Image OptionSelect_Button;     //�I�v�V������I��ł���Ƃ��̃{�^���̉摜
    [SerializeField] private Image GameStart_Button;        //�Q�[���X�^�[�g��I��ł��Ȃ��Ƃ��̃{�^���̉摜
    [SerializeField] private Image Option_Button;           //�I�v�V������I��ł��Ȃ����̃{�^���̉摜

    [SerializeField] private Image TimingFadeOutToOption;   //�I�v�V������ʂ։�ʑJ�ڂ���Ƃ��̃^�C�~���O�t�F�[�h�A�E�g
    [SerializeField] private Image TimingFadeOutOption;     //�I�v�V������ʂ̃^�C�~���O�t�F�[�h�A�E�g
    [SerializeField] private Image TimingFadeInOption;      //�I�v�V������ʂ̃^�C�~���O�t�F�[�h�C��
    [SerializeField] private Image TimingFadeInMainMenu;    //���C�����j���[�̃^�C�~���O�t�F�[�h�C��

    [SerializeField] private GameObject MainMenuHUD;        //���C�����j���[HUD �p�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject ExitHUD;            //�I�����HUD �p�̃Q�[���I�u�W�F�N�g
    [SerializeField] private GameObject OptionHUD;          //�I�v�V�������HUD�@�p�̃Q�[���I�u�W�F�N�g

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

    void CheckStateAnimation()  //���C�����j���[�A�j���[�V�����Ǘ��֐�
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
        //�t�F�[�h�A�j���[�V�������s���́AESC�L�[�����s�����Ȃ�
        if (FadeImage.fillAmount <= 0.1f && !FadeSystemFadeInOption.StartFadeImage && !FadeSystemFadeOutMainMenu.StartFadeImage && !FadeSystemOptionScreen.StartFadeImage && !FadeSystemToOption.StartFadeImage) 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PushEsc = !PushEsc;
            }
        }

        //�J���̃t�F�[�h�A�j���[�V�������I����Ă��āACantSelectMenu��False�Ȃ烁�C�����j���[�𑀍�ł���
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
        //ESC�̓��͂����菮�����j���[�����ݐG���󋵂������ꍇ
        if (PushEsc && !CantSelectMenu)
        {
            if (MainMenuHUD.activeSelf == true) //���C�����j���[��ESC���������ꍇ
            {
                ExitHUD.SetActive(true);
                MainMenuHUD.SetActive(false);
            }
            if (OptionHUD.activeSelf == true && !OptionSystem.isEnterMode) //�I�v�V������ʂ�ESC���������ꍇ
            {
                FadeSystemFadeInOption.StartFadeImage = true;
            }
        }
        else
        {
            if (ExitHUD.activeSelf == true) //�Q�[���I����ʂ�ESC���������ꍇ
            {
                ExitHUD.SetActive(false);
                MainMenuHUD.SetActive(true);
            }
        }

    }
}
