using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Option : MonoBehaviour
{

    UI_MainMenu MainMenuSystem;
    [SerializeField] private GameObject MainMenuObject; //���C�����j���̃X�N���v�g�Q�Ɨp�̃I�u�W�F�N�g
    [SerializeField] private Image CurrentSelect;       //���ݑI��ł鍀�ڂ�\��������
    [SerializeField] private int CurrentColumn;         //���ݑI��ł鍀��
    private int OptionColumns = 5;                      //�I�v�V�����̍��ڐ� (0: BGM�ݒ� / 1:SE�ݒ� / 2: �}�E�X���x / 3:�p�t�H�[�}���X�\���ݒ� / 4: �𑜓x�ݒ� / 5: OK�{�^��)
    public bool isEnterMode;                            //�I���̎��́A�I��ł��鍀�ڂ̐ݒ��ύX�ł���
    private RectTransform CurrentSelectPos;             //CurrentSelect�̃|�W�V�����p
    private Vector2 SelectPos;                          //RectTransform�ɔ��f������p
    [SerializeField] private int performance_Enable;    //�p�t�H�[�}���X�\����ON/OFF (0:Disable / 1:Enable)
    public int Performance_Enable { get { return performance_Enable; } }//�p�t�H�[�}���X�\����ON/OFF Getter/Setter
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SESlider;
    [SerializeField] private Slider Mouse_Sensi;
    [SerializeField] private Text BGMValue;
    [SerializeField] private Text SEValue;
    [SerializeField] private Text MouseSensiValue;

    [SerializeField] private Image StartNoSelect_Button;
    [SerializeField] private Image StartSelect_Button;

    [SerializeField] private Image MasterVolume_SelectImage;
    [SerializeField] private Image SEVolume_SelectImage;
    [SerializeField] private Image MouseSensi_SelectImage;
    [SerializeField] private Image FPS_SelectImage;
    [SerializeField] private Image Resolution_SelectImage;

    [SerializeField] private GameObject Performance_ON_Object;
    [SerializeField] private GameObject Performance_OFF_Object;
    private void Awake()
    {
        CurrentSelectPos = CurrentSelect.GetComponent<RectTransform>();
        MainMenuSystem = MainMenuObject.GetComponent<UI_MainMenu>();
    }
    private void Start()
    {
        CurrentColumn = 0;
    }
    void Update()
    {
        InputDir();
        EnterChangeOptionMode();
        RefreshUIValue();
    }

    void InputDir()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && (!MainMenuSystem.CantSelectMenu && !isEnterMode))
        {
            if (CurrentColumn == 0)
            {
                CurrentColumn = OptionColumns;
            }
            else
            {
                CurrentColumn--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (!MainMenuSystem.CantSelectMenu && !isEnterMode))
        {
            if (CurrentColumn == OptionColumns)
            {
                CurrentColumn = 0;
            }
            else
            {
                CurrentColumn++;
            }
        }
    }

    void EnterChangeOptionMode()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !MainMenuSystem.CantSelectMenu)
        {
            isEnterMode = !isEnterMode;
        }


        switch (CurrentColumn)
        {
            case 0:         //BGM�ݒ�
                CurrentSelect.enabled = true;
                StartSelect_Button.enabled = false;
                StartNoSelect_Button.enabled = true;
                SelectPos = new Vector2(-243, 238);
                if (isEnterMode)
                {
                    MasterVolume_SelectImage.enabled = true;
                    SEVolume_SelectImage.enabled = false;
                    MouseSensi_SelectImage.enabled = false;
                    FPS_SelectImage.enabled = false;
                    Resolution_SelectImage.enabled = false;
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && BGMSlider.value > 0.0f)
                    {
                        BGMSlider.value -= 0.1f;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow) && BGMSlider.value < 1.0f)
                    {
                        BGMSlider.value += 0.1f;
                    }
                }
                else
                {
                    MasterVolume_SelectImage.enabled = false;
                    SEVolume_SelectImage.enabled = false;
                    MouseSensi_SelectImage.enabled = false;
                    FPS_SelectImage.enabled = false;
                    Resolution_SelectImage.enabled = false;
                }
                break;
            case 1:         //SE�ݒ�
                CurrentSelect.enabled = true;
                StartSelect_Button.enabled = false;
                StartNoSelect_Button.enabled = true;
                SelectPos = new Vector2(-243, 158);
                if (isEnterMode)
                {
                    MasterVolume_SelectImage.enabled = false;
                    SEVolume_SelectImage.enabled = true;
                    MouseSensi_SelectImage.enabled = false;
                    FPS_SelectImage.enabled = false;
                    Resolution_SelectImage.enabled = false;
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && SESlider.value > 0.0f)
                    {
                        SESlider.value -= 0.1f;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow) && SESlider.value < 1.0f)
                    {
                        SESlider.value += 0.1f;
                    }
                }
                else
                {
                    MasterVolume_SelectImage.enabled = false;
                    SEVolume_SelectImage.enabled = false;
                    MouseSensi_SelectImage.enabled = false;
                    FPS_SelectImage.enabled = false;
                    Resolution_SelectImage.enabled = false;
                }
                break;
            case 2:         //�}�E�X���x
                CurrentSelect.enabled = true;
                StartSelect_Button.enabled = false;
                StartNoSelect_Button.enabled = true;
                SelectPos = new Vector2(-243, 28);
                if (isEnterMode)
                {
                    MasterVolume_SelectImage.enabled = false;
                    SEVolume_SelectImage.enabled = false;
                    MouseSensi_SelectImage.enabled = true;
                    FPS_SelectImage.enabled = false;
                    Resolution_SelectImage.enabled = false;
                }
                else
                {
                    MasterVolume_SelectImage.enabled = false;
                    SEVolume_SelectImage.enabled = false;
                    MouseSensi_SelectImage.enabled = false;
                    FPS_SelectImage.enabled = false;
                    Resolution_SelectImage.enabled = false;
                }
                break;
            case 3:         //�p�t�H�[�}���X�\���ݒ�
                CurrentSelect.enabled = true;
                StartSelect_Button.enabled = false;
                StartNoSelect_Button.enabled = true;
                SelectPos = new Vector2(-243, -52);
                if (isEnterMode)
                {
                    MasterVolume_SelectImage.enabled = false;
                    SEVolume_SelectImage.enabled = false;
                    MouseSensi_SelectImage.enabled = false;
                    FPS_SelectImage.enabled = true;
                    Resolution_SelectImage.enabled = false;
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && (!Performance_OFF_Object.activeSelf) && Performance_ON_Object.activeSelf)
                    {
                        performance_Enable = 0;
                        Performance_ON_Object.SetActive(false);
                        Performance_OFF_Object.SetActive(true);
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow) && Performance_OFF_Object.activeSelf && (!Performance_ON_Object.activeSelf))
                    {
                        performance_Enable = 1;
                        Performance_OFF_Object.SetActive(false);
                        Performance_ON_Object.SetActive(true);
                    }
                }
                else
                {
                    MasterVolume_SelectImage.enabled = false;
                    SEVolume_SelectImage.enabled = false;
                    MouseSensi_SelectImage.enabled = false;
                    FPS_SelectImage.enabled = false;
                    Resolution_SelectImage.enabled = false;
                }
                break;
            case 4:         //�𑜓x�ݒ�
                CurrentSelect.enabled = true;
                StartSelect_Button.enabled = false;
                StartNoSelect_Button.enabled = true;
                SelectPos = new Vector2(-243, -138);
                if (isEnterMode)
                {
                    MasterVolume_SelectImage.enabled = false;
                    SEVolume_SelectImage.enabled = false;
                    MouseSensi_SelectImage.enabled = false;
                    FPS_SelectImage.enabled = false;
                    Resolution_SelectImage.enabled = true;
                }
                else
                {
                    MasterVolume_SelectImage.enabled = false;
                    SEVolume_SelectImage.enabled = false;
                    MouseSensi_SelectImage.enabled = false;
                    FPS_SelectImage.enabled = false;
                    Resolution_SelectImage.enabled = false;
                }
                break;
            case 5:         //OK�{�^��
                CurrentSelect.enabled = false;
                StartNoSelect_Button.enabled = false;
                StartSelect_Button.enabled = true;
                if (isEnterMode)
                {

                }
                else
                {

                }
                break;
        }
        CurrentSelectPos.anchoredPosition = SelectPos;//���݂̑I�΂�Ă��鍀�ڂ̏ꏊ�֖���u��
    }

    void RefreshUIValue()
    {
        float FinalBGMValue = 0.0f;
        float FinalSEValue = 0.0f;
        FinalBGMValue = BGMSlider.value * 100;
        FinalSEValue = SESlider.value * 100;
        BGMValue.text = FinalBGMValue.ToString("0") + "%";
        SEValue.text = FinalSEValue.ToString("0") + "%";
    }
}
