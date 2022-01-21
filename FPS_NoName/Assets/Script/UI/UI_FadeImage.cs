using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeImage : MonoBehaviour
{
    [SerializeField] private float FadeTime;                //�t�F�[�h����
    [SerializeField] private bool FadeOut;                  //�t�F�[�h�A�E�g�����s����
    [SerializeField] private bool FadeIn;                   //�t�F�[�h�C�������s����
    [SerializeField] private bool IsTimingFadeImage;        //�^�C�~���O�t�F�[�h�V�X�e�������s����
    [SerializeField] private bool SpeedUpFadeSpeedx2;       //�t�F�[�h�X�s�[�h���{�ɂ���
    public bool StartFadeImage;                             //�^�C�~���O�t�F�[�h�����s����p��bool
    public bool FinishFadeOUT;                              //�t�F�[�h�A�E�g���I������m�F�p��bool
    public bool FinishFadeIN;                               //�t�F�[�h�C�����I������m�F�p��bool
    Image FadeImage;


    private void Awake()
    {
        FadeImage = this.GetComponent<Image>();
        if (IsTimingFadeImage && !FadeIn)
        {
            FadeImage.enabled = false;
        }
        if (FadeIn)
        {
            FadeImage.fillAmount = 1;
        }
        else if (FadeOut)
        {
            FadeImage.fillAmount = 0;
        }
        if (SpeedUpFadeSpeedx2)
        {
            FadeTime = FadeTime * 2.0f;
        }
    }
    void FixedUpdate()                                      //�ǂ��PC�ł����x���ψ�ɂȂ�悤��FixedUpdate���g�p
    {
        FadeIN();
        FadeOUT();
        TimingFadeIN();
        TimingFadeOUT();
    }

    void FadeIN()
    {
        if(FadeIn && !FadeOut && !IsTimingFadeImage)
        {
            FadeImage.fillAmount -= FadeTime;
            if (FadeImage.fillAmount <= 0.02)
            {
                FadeImage.enabled = false;
            }
        }
    }

    void FadeOUT()
    {
        if (!FadeIn && FadeOut && !IsTimingFadeImage)
        {
            FadeImage.fillAmount += FadeTime;
            if(FadeImage.fillAmount >= 0.98)
            {
            }
        }
    }

    void TimingFadeIN()
    {
        if (FadeIn && !FadeOut && IsTimingFadeImage)
        {
            if (StartFadeImage)
            {
                FadeImage.enabled = true;
                FadeImage.fillAmount -= FadeTime;
                if (FadeImage.fillAmount <= 0.02)
                {
                    FadeImage.enabled = false;
                    FinishFadeIN = true;
                    StartFadeImage = false;
                }
            }
            else if(!FinishFadeIN & !StartFadeImage)
            {
                FadeImage.fillAmount = 1.0f;
            }
        }
    }

    void TimingFadeOUT()
    {
        if (!FadeIn && FadeOut && IsTimingFadeImage)
        {
            if (StartFadeImage) 
            {
                FadeImage.enabled = true;
                FadeImage.fillAmount += FadeTime;
                if (FadeImage.fillAmount >= 0.98)
                {
                    FinishFadeOUT = true;
                    StartFadeImage = false;
                }
            }
            else if (!FinishFadeOUT && !StartFadeImage)
            {
                FadeImage.enabled = false;
                FadeImage.fillAmount = 0.0f;
            }
        }
    }
}
