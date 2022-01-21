using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Option : MonoBehaviour
{

    UI_MainMenu MainMenuSystem;
    [SerializeField] private GameObject MainMenuObject;
    [SerializeField] private int CurrentColumn;
    private bool isEnterMode;
    private int OptionColumns = 3;

    private void Awake()
    {
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
                break;
            case 1:         //SE設定
                break;
            case 2:         //パフォーマンス表示設定
                break;
            case 3:         //解像度設定
                break;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isEnterMode = !isEnterMode;

        }
    }
}
