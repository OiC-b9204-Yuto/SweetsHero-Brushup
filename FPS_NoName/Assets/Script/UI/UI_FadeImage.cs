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
    public AnimationState animationState;                   //フェードの現在のアニメーション状況
    [SerializeField] private float FadeTime;                //フェード時間
    [SerializeField] private bool FadeOut;                  //フェードアウトを実行する
    [SerializeField] private bool FadeIn;                   //フェードインを実行する
    [SerializeField] private bool IsTimingFadeImage;        //タイミングフェードシステムを実行する
    [SerializeField] private bool SpeedUpFadeSpeedx2;       //フェードスピードを二倍にする
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

    void FixedUpdate()                                      //どんなPCでも速度が均一になるようにFixedUpdateを使用
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
