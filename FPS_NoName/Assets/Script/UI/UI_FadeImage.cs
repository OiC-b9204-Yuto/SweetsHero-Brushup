using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AnimationState
{
    FadeWait,
    FadeStart,
    FadeFinish,
}

public class UI_FadeImage : MonoBehaviour
{
    public AnimationState animationState;                   //�t�F�[�h�̌��݂̃A�j���[�V������
    [SerializeField] private float FadeTime;                //�t�F�[�h����
    [SerializeField] private bool FadeOut;                  //�t�F�[�h�A�E�g�����s����
    [SerializeField] private bool FadeIn;                   //�t�F�[�h�C�������s����
    [SerializeField] private bool IsTimingFadeImage;        //�^�C�~���O�t�F�[�h�V�X�e�������s����
    [SerializeField] private bool SpeedUpFadeSpeedx2;       //�t�F�[�h�X�s�[�h���{�ɂ���
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

    private void Update()
    {
        DisableImage();
    }

    void FixedUpdate()                                      //�ǂ��PC�ł����x���ψ�ɂȂ�悤��FixedUpdate���g�p
    {
        WaitSystem();
        FadeIN();
        FadeOUT();
        TimingFadeIN();
        TimingFadeOUT();
    }

    void DisableImage()
    {
        if(animationState != AnimationState.FadeFinish)
        {
            return;
        }
        else
        {
            FadeImage.enabled = false;
        }
    }

    void WaitSystem()
    {
        if (animationState == AnimationState.FadeWait)
        {
            if (FadeIn)
            {
                FadeImage.fillAmount = 1;
            }
            else if (FadeOut)
            {
                FadeImage.fillAmount = 0;
            }
            FadeImage.enabled = false;
        }
    }

    void FadeIN()
    {
        if(FadeIn && !FadeOut && !IsTimingFadeImage)
        {
            animationState = AnimationState.FadeStart;
            FadeImage.fillAmount -= FadeTime;
            if (FadeImage.fillAmount <= 0.02f)
            {
                animationState = AnimationState.FadeFinish;
            }
        }
    }

    void FadeOUT()
    {
        if (!FadeIn && FadeOut && !IsTimingFadeImage)
        {
            animationState = AnimationState.FadeStart;
            FadeImage.fillAmount += FadeTime;
            if(FadeImage.fillAmount >= 0.98f)
            {
                animationState = AnimationState.FadeFinish;
            }
        }
    }

    void TimingFadeIN()
    {
        if (FadeIn && !FadeOut && IsTimingFadeImage)
        {
            if (animationState == AnimationState.FadeStart)
            {
                FadeImage.enabled = true;
                FadeImage.fillAmount -= FadeTime;
                if (FadeImage.fillAmount <= 0.02f)
                {
                    animationState = AnimationState.FadeFinish;
                }
            }
        }
    }

    void TimingFadeOUT()
    {
        if (!FadeIn && FadeOut && IsTimingFadeImage)
        {
            if (animationState == AnimationState.FadeStart) 
            {
                FadeImage.enabled = true;
                FadeImage.fillAmount += FadeTime;
                if (FadeImage.fillAmount >= 0.98f)
                {
                    animationState = AnimationState.FadeFinish;
                }
            }
        }
    }
}
