using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class UI_Option : MonoBehaviour
{

    UI_Menu MainMenuSystem;
    [SerializeField] private GameObject MainMenuObject; //���C�����j���̃X�N���v�g�Q�Ɨp�̃I�u�W�F�N�g
    [SerializeField] private GameObject ApplyMessageObject;
    [SerializeField] private Image CurrentSelect;       //���ݑI��ł鍀�ڂ�\��������
    [SerializeField] private int CurrentColumn;         //���ݑI��ł鍀��
    private int OptionColumns = 6;                      //�I�v�V�����̍��ڐ� (0: BGM�ݒ� / 1:SE�ݒ� / 2: �}�E�X���x / 3:�p�t�H�[�}���X�\���ݒ� / 4: �𑜓x�ݒ� / 5: ��ʃ��[�h / 6: OK�{�^��)
    public bool isEnterMode;                            //�I���̎��́A�I��ł��鍀�ڂ̐ݒ��ύX�ł���
    public bool isApplyShown;                           //�K�p���b�Z�[�W�\����
    private RectTransform CurrentSelectPos;             //CurrentSelect�̃|�W�V�����p
    private Vector2 SelectPos;                          //RectTransform�ɔ��f������p
    [SerializeField] private int CurrentSelectRes;      //���ݑI��ł���Res
    [SerializeField] private int performance_Enable;    //�p�t�H�[�}���X�\����ON/OFF (0:Disable / 1:Enable)
    public int Performance_Enable { get { return performance_Enable; } }//�p�t�H�[�}���X�\����ON/OFF Getter/Setter
    [SerializeField] private List<string> ResolutionList;       //�𑜓x��ێ����邽�߂̃��X�g
    [SerializeField] private string CurrentResolutionString;    //���݂̉𑜓x��String�ŕۑ�����
    [SerializeField] private string[] ResSplitStr;              //�𑜓x�𔽉f�����邽�߂ɐ���������ێ�����z��
    FullScreenMode fullScreenMode;                              //�t���X�N���[�����[�h��ύX������
    [SerializeField] private int ScreenModeType;                //�X�N���[���^�C�v��ۑ����� (0: �t���X�N���[�� / 1:�t���X�N���[�� �E�B���h�E / 2:�E�B���h�E���)
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SESlider;
    [SerializeField] private Slider Mouse_Sensi;
    [SerializeField] private Text BGMValue;
    [SerializeField] private Text SEValue;
    [SerializeField] private Text MouseSensiValue;

    [SerializeField] private Text ResolutionText;
    [SerializeField] private Text ScreenModeShowText;

    [SerializeField] private Image StartNoSelect_Button;
    [SerializeField] private Image StartSelect_Button;

    [SerializeField] private Image MasterVolume_SelectImage;
    [SerializeField] private Image SEVolume_SelectImage;
    [SerializeField] private Image MouseSensi_SelectImage;
    [SerializeField] private Image FPS_SelectImage;
    [SerializeField] private Image Resolution_SelectImage;

    [SerializeField] private GameObject ScreenModeText;
    [SerializeField] private GameObject ScreenModeSelectText;

    [SerializeField] private GameObject Performance_ON_Object;
    [SerializeField] private GameObject Performance_OFF_Object;

    [SerializeField] private AudioClip MainMenuChangeColumnSE;
    [SerializeField] private AudioClip MainMenuEnterSE;
    private void Awake()
    {
        AudioManager.Instance.Load();
        GameData_Manager.Instance.Load();
        ScreenModeType = GameData_Manager.Instance.gameData.ScreenMode;
        performance_Enable = GameData_Manager.Instance.gameData.FpsShown;
        Mouse_Sensi.value = GameData_Manager.Instance.gameData.MouseSensitivity;
        CurrentSelectPos = CurrentSelect.GetComponent<RectTransform>();
        MainMenuSystem = MainMenuObject.GetComponent<UI_Menu>();
        switch (ScreenModeType)
        {
            case 0:
                ScreenModeShowText.text = "�t���X�N���[��";
                break;
            case 1:
                ScreenModeShowText.text = "�t���X�N���[�� �E�B���h�E";
                break;
            case 2:
                ScreenModeShowText.text = "�E�B���h�E";
                break;
        }

    }
    private void Start()
    {
        CurrentColumn = 0;

        //�𑜓x�̎����擾

        ResolutionText.text = Screen.width + "*" + Screen.height + "@" + Screen.currentResolution.refreshRate + "hz";

        var resolutions = Screen.resolutions;
        List<Resolution> checkList = new List<Resolution>();

        foreach (var res in resolutions)
        {
            bool check = true;
            foreach (var listItem in checkList)
            {
                if (res.width == listItem.width)
                {
                    if (res.height == listItem.height)
                    {
                        check = false;
                        break;
                    }
                }
            }
            if (check)
            {
                ResolutionList.Add(res.width.ToString() + "*" + res.height.ToString() + "@" + res.refreshRate +"hz");
            }
            checkList.Add(res);
        }

        for (int i =0;i < ResolutionList.Count; i++)
        {
            if(ResolutionText.text == ResolutionList[i])
            {
                CurrentSelectRes = i;
            }
        }
    }
    void Update()
    {
        InputDir();
        EnterChangeOptionMode();
        RefreshUIValue();
        CheckPerfomanceSettingEnable();
    }

    void InputDir()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && (!MainMenuSystem.CantSelectMenu && !isEnterMode))
        {
            AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
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
            AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
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
        if (isApplyShown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.Instance.SE.PlayOneShot(MainMenuEnterSE);
                ApplyMessageObject.SetActive(false);
                isApplyShown = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !MainMenuSystem.CantSelectMenu && !isApplyShown)
        {
            AudioManager.Instance.SE.PlayOneShot(MainMenuEnterSE);
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
                        AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
                        BGMSlider.value -= 0.1f;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow) && BGMSlider.value < 1.0f)
                    {
                        AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
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
                        AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
                        SESlider.value -= 0.1f;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow) && SESlider.value < 1.0f)
                    {
                        AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
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
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        Mouse_Sensi.value -= 0.5f;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Mouse_Sensi.value += 0.5f;
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
                        AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
                        performance_Enable = 0;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow) && Performance_OFF_Object.activeSelf && (!Performance_ON_Object.activeSelf))
                    {
                        AudioManager.Instance.SE.PlayOneShot(MainMenuChangeColumnSE);
                        performance_Enable = 1;
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
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (CurrentSelectRes <= 0)
                        {
                            CurrentSelectRes = ResolutionList.Count-1;
                        }
                        else
                        {
                            CurrentSelectRes--;
                        }
                        ResolutionText.text = ResolutionList[CurrentSelectRes];
                        CurrentResolutionString = ResolutionText.text.Replace(@"\s", "");
                        ResSplitStr = CurrentResolutionString.Split('*','@');
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (CurrentSelectRes >= ResolutionList.Count-1)
                        {
                            CurrentSelectRes = 0;
                        }
                        else
                        {
                            CurrentSelectRes++;
                        }
                        ResolutionText.text = ResolutionList[CurrentSelectRes];
                        CurrentResolutionString = ResolutionText.text.Replace(@"\s", "");
                        ResSplitStr = CurrentResolutionString.Split('*', '@');
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
            case 5:
                CurrentSelect.enabled = true;
                SelectPos = new Vector2(-243, -225);
                StartNoSelect_Button.enabled = true;
                StartSelect_Button.enabled = false;
                if (isEnterMode)
                {
                    ScreenModeText.SetActive(false);
                    ScreenModeSelectText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (ScreenModeType <= 0)
                        {
                            ScreenModeType = 2;
                        }
                        else
                        {
                            ScreenModeType--;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (ScreenModeType >= 2)
                        {
                            ScreenModeType = 0;
                        }
                        else
                        {
                            ScreenModeType++;
                        }
                    }

                    switch (ScreenModeType)
                    {
                        case 0:
                            ScreenModeShowText.text = "�t���X�N���[��";
                            fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                            break;
                        case 1:
                            ScreenModeShowText.text = "�t���X�N���[�� �E�B���h�E";
                            fullScreenMode = FullScreenMode.FullScreenWindow;
                            break;
                        case 2:
                            ScreenModeShowText.text = "�E�B���h�E";
                            fullScreenMode = FullScreenMode.Windowed;
                            break;
                    }

                }
                else
                {
                    ScreenModeText.SetActive(true);
                    ScreenModeSelectText.SetActive(false);
                }
                break;
            case 6:         //OK�{�^��
                CurrentSelect.enabled = false;
                StartNoSelect_Button.enabled = false;
                StartSelect_Button.enabled = true;
                if (isEnterMode)
                {
                    ApplyMessageObject.SetActive(true);
                    isApplyShown = true;
                    GameData_Manager.Instance.gameData.MouseSensitivity = Mouse_Sensi.value;
                    GameData_Manager.Instance.gameData.FpsShown = performance_Enable;
                    GameData_Manager.Instance.gameData.ScreenMode = ScreenModeType;
                    AudioManager.Instance.Save();
                    GameData_Manager.Instance.Save();
                    Screen.SetResolution(int.Parse(ResSplitStr[0]), int.Parse(ResSplitStr[1]), fullScreenMode);
                    isEnterMode = false;
                }
                break;
        }
        CurrentSelectPos.anchoredPosition = SelectPos;//���݂̑I�΂�Ă��鍀�ڂ̏ꏊ�֖���u��
    }

    void RefreshUIValue()
    {
        float FinalBGMValue = 0.0f;
        float FinalSEValue = 0.0f;
        float MouseValue = 0.0f;
        FinalBGMValue = BGMSlider.value * 100;
        FinalSEValue = SESlider.value * 100;
        MouseValue = Mouse_Sensi.value;
        BGMValue.text = FinalBGMValue.ToString("0") + "%";
        SEValue.text = FinalSEValue.ToString("0") + "%";
        MouseSensiValue.text = MouseValue.ToString("0.0");
    }
    void CheckPerfomanceSettingEnable()
    {
        switch (performance_Enable)
        {
            case 0:
                Performance_ON_Object.SetActive(false);
                Performance_OFF_Object.SetActive(true);
                break;
            case 1:
                Performance_OFF_Object.SetActive(false);
                Performance_ON_Object.SetActive(true);
                break;
        }
    }
}
