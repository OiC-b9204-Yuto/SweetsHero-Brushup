using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeImage : MonoBehaviour
{
    [SerializeField] private float FadeTime;                //フェード時間
    [SerializeField] private bool FadeOut;                  //フェードアウトを実行する
    [SerializeField] private bool FadeIn;                   //フェードインを実行する
    [SerializeField] private bool IsTimingFadeImage;        //タイミングフェードシステムを実行する
    [SerializeField] private bool SpeedUpFadeSpeedx2;       //フェードスピードを二倍にする
    public bool StartFadeImage;                             //タイミングフェードを実行する用のbool
    public bool FinishFadeOUT;                              //フェードアウトが終わった確認用のbool
    public bool FinishFadeIN;                               //フェードインが終わった確認用のbool
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
    void FixedUpdate()                                      //どんなPCでも速度が均一になるようにFixedUpdateを使用
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
