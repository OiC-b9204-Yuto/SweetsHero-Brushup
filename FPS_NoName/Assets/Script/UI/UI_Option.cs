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
    private int OptionColumns = 4;                      //�I�v�V�����̍��ڐ� (0: BGM�ݒ� / 1:SE�ݒ� / 2:�p�t�H�[�}���X�\���ݒ� / 3: �𑜓x�ݒ� / 4: OK�{�^��)
    private bool isEnterMode;                           //�I���̎��́A�I��ł��鍀�ڂ̐ݒ��ύX�ł���
    private RectTransform CurrentSelectPos;             //CurrentSelect�̃|�W�V�����p
    private Vector2 SelectPos;                          //RectTransform�ɔ��f������p


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
            case 0:         //BGM�ݒ�
                CurrentSelect.enabled = true;
                SelectPos = new Vector2(-200, 190);
                break;
            case 1:         //SE�ݒ�
                CurrentSelect.enabled = true;
                SelectPos = new Vector2(-200, 105);
                break;
            case 2:         //�p�t�H�[�}���X�\���ݒ�
                CurrentSelect.enabled = true;
                SelectPos = new Vector2(-200, -40);
                break;
            case 3:         //�𑜓x�ݒ�
                CurrentSelect.enabled = true;
                SelectPos = new Vector2(-200, -115);
                break;
            case 4:         //OK�{�^��
                CurrentSelect.enabled = false;
                break;
        }
        CurrentSelectPos.anchoredPosition = SelectPos;//���݂̑I�΂�Ă��鍀�ڂ̏ꏊ�֖���u��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isEnterMode = !isEnterMode;

        }
        
    }
}
