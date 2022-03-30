using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class UI_Option : MonoBehaviour
{

    UI_Menu MainMenuSystem;
    [SerializeField] private GameObject MainMenuObject; //メインメニュのスクリプト参照用のオブジェクト
    [SerializeField] private GameObject ApplyMessageObject;
    [SerializeField] private Image CurrentSelect;       //現在選んでる項目を表示する矢印
    [SerializeField] private int CurrentColumn;         //現在選んでる項目
    private int OptionColumns = 6;                      //オプションの項目数 (0: BGM設定 / 1:SE設定 / 2: マウス感度 / 3:パフォーマンス表示設定 / 4: 解像度設定 / 5: 画面モード / 6: OKボタン)
    public bool isEnterMode;                            //オンの時は、選んでいる項目の設定を変更できる
    public bool isApplyShown;                           //適用メッセージ表示中
    private RectTransform CurrentSelectPos;             //CurrentSelectのポジション用
    private Vector2 SelectPos;                          //RectTransformに反映させる用
    [SerializeField] private int CurrentSelectRes;      //現在選んでいるRes
    [SerializeField] private int performance_Enable;    //パフォーマンス表示のON/OFF (0:Disable / 1:Enable)
    public int Performance_Enable { get { return performance_Enable; } }//パフォーマンス表示のON/OFF Getter/Setter
    [SerializeField] private List<string> ResolutionList;       //解像度を保持するためのリスト
    [SerializeField] private string CurrentResolutionString;    //現在の解像度をStringで保存する
    [SerializeField] private string[] ResSplitStr;              //解像度を反映させるために数字だけを保持する配列
    FullScreenMode fullScreenMode;                              //フルスクリーンモードを変更させる
    [SerializeField] private int ScreenModeType;                //スクリーンタイプを保存する (0: フルスクリーン / 1:フルスクリーン ウィンドウ / 2:ウィンドウ画面)
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
                ScreenModeShowText.text = "フルスクリーン";
                break;
            case 1:
                ScreenModeShowText.text = "フルスクリーン ウィンドウ";
                break;
            case 2:
                ScreenModeShowText.text = "ウィンドウ";
                break;
        }

    }
    private void Start()
    {
        CurrentColumn = 0;

        //解像度の自動取得

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
            case 0:         //BGM設定
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
            case 1:         //SE設定
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
            case 2:         //マウス感度
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
            case 3:         //パフォーマンス表示設定
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
            case 4:         //解像度設定
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
                            ScreenModeShowText.text = "フルスクリーン";
                            fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                            break;
                        case 1:
                            ScreenModeShowText.text = "フルスクリーン ウィンドウ";
                            fullScreenMode = FullScreenMode.FullScreenWindow;
                            break;
                        case 2:
                            ScreenModeShowText.text = "ウィンドウ";
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
            case 6:         //OKボタン
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
        CurrentSelectPos.anchoredPosition = SelectPos;//現在の選ばれている項目の場所へ矢印を置く
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
