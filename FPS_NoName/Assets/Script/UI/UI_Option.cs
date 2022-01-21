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
    private bool isEnterMode;                           //オンの時は、選んでいる項目の設定を変更できる
    private RectTransform CurrentSelectPos;             //CurrentSelectのポジション用
    private Vector2 SelectPos;                          //RectTransformに反映させる用


    private void Awake()
    {
        CurrentSelectPos = CurrentSelect.GetComponent<RectTransform>();
        MainMenuSystem = MainMenuObject.GetComponent<UI_MainMenu>();
        CurrentColumn = 0;
    }

    void Update()
    {
        InputDir();
        EnterChangeOptionMode();
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
            if(CurrentColumn == OptionColumns)
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
        switch (CurrentColumn)
        {
            case 0:         //BGM設定
                CurrentSelect.enabled = true;
                SelectPos = new Vector2(-200, 190);
                break;
            case 1:         //SE設定
                CurrentSelect.enabled = true;
                SelectPos = new Vector2(-200, 105);
                break;
            case 2:         //パフォーマンス表示設定
                CurrentSelect.enabled = true;
                SelectPos = new Vector2(-200, -40);
                break;
            case 3:         //解像度設定
                CurrentSelect.enabled = true;
                SelectPos = new Vector2(-200, -115);
                break;
            case 4:         //OKボタン
                CurrentSelect.enabled = false;
                break;
        }
        CurrentSelectPos.anchoredPosition = SelectPos;//現在の選ばれている項目の場所へ矢印を置く
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isEnterMode = !isEnterMode;

        }
        
    }
}
