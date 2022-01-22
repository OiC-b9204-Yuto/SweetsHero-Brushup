using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Option : MonoBehaviour
{

    UI_MainMenu MainMenuSystem;
    [SerializeField] private GameObject MainMenuObject; //メインメニュのスクリプト参照用のオブジェクト
    [SerializeField] private Image CurrentSelect;       //現在選んでる項目を表示する矢印
    [SerializeField] private int CurrentColumn;         //現在選んでる項目
    private int OptionColumns = 4;                      //オプションの項目数 (0: BGM設定 / 1:SE設定 / 2:パフォーマンス表示設定 / 3: 解像度設定 / 4: OKボタン)
    public bool isEnterMode;                            //オンの時は、選んでいる項目の設定を変更できる
    private RectTransform CurrentSelectPos;             //CurrentSelectのポジション用
    private RectTransform CurrentEnterPos;              //現在の編集中の項目の背景のポジション用
    private Vector2 SelectPos;                          //RectTransformに反映させる用
    [SerializeField] private int performance_Enable;    //パフォーマンス表示のON/OFF (0:Disable / 1:Enable)
    public int Performance_Enable { get { return performance_Enable; } }//パフォーマンス表示のON/OFF Getter/Setter
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SESlider;
    [SerializeField] private Text BGMValue;
    [SerializeField] private Text SEValue;

    [SerializeField] private Image StartNoSelect_Button;
    [SerializeField] private Image StartSelect_Button;
    [SerializeField] private Image CurrentEnter_IMG;

    [SerializeField] private GameObject Performance_ON_Object;
    [SerializeField] private GameObject Performance_OFF_Object;
    private void Awake()
    {
        CurrentSelectPos = CurrentSelect.GetComponent<RectTransform>();
        CurrentEnterPos = CurrentEnter_IMG.GetComponent<RectTransform>();
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
            case 0:         //BGM設定
                CurrentSelect.enabled = true;
                StartSelect_Button.enabled = false;
                StartNoSelect_Button.enabled = true;
                SelectPos = new Vector2(-200, 190);
                if (isEnterMode)
                {
                    CurrentEnter_IMG.enabled = true;
                    CurrentEnterPos.anchoredPosition = new Vector2(240,194);
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
                    CurrentEnter_IMG.enabled = false;
                }
                break;
            case 1:         //SE設定
                CurrentSelect.enabled = true;
                StartSelect_Button.enabled = false;
                StartNoSelect_Button.enabled = true;
                SelectPos = new Vector2(-200, 105);
                if (isEnterMode)
                {
                    CurrentEnter_IMG.enabled = true;
                    CurrentEnterPos.anchoredPosition = new Vector2(240, 104);
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
                    CurrentEnter_IMG.enabled = false;
                }
                break;
            case 2:         //パフォーマンス表示設定
                CurrentSelect.enabled = true;
                StartSelect_Button.enabled = false;
                StartNoSelect_Button.enabled = true;
                SelectPos = new Vector2(-200, -40);
                if (isEnterMode)
                {
                    CurrentEnter_IMG.enabled = true;
                    CurrentEnterPos.anchoredPosition = new Vector2(240, -39);
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
                    CurrentEnter_IMG.enabled = false;
                }
                break;
            case 3:         //解像度設定
                CurrentSelect.enabled = true;
                StartSelect_Button.enabled = false;
                StartNoSelect_Button.enabled = true;
                SelectPos = new Vector2(-200, -115);
                if (isEnterMode)
                {
                    CurrentEnter_IMG.enabled = true;
                    CurrentEnterPos.anchoredPosition = new Vector2(240, -116);
                }
                else
                {
                    CurrentEnter_IMG.enabled = false;
                }
                break;
            case 4:         //OKボタン
                CurrentSelect.enabled = false;
                StartNoSelect_Button.enabled = false;
                StartSelect_Button.enabled = true;
                if (isEnterMode)
                {

                }
                else
                {
                    CurrentEnter_IMG.enabled = false;
                }
                break;
        }
        CurrentSelectPos.anchoredPosition = SelectPos;//現在の選ばれている項目の場所へ矢印を置く
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
