using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeImage : MonoBehaviour
{
    [SerializeField] private float FadeTime;
    [SerializeField] private bool FadeOut;
    [SerializeField] private bool FadeIn;
    [SerializeField] private bool IsTimingFadeImage;
    [SerializeField] private bool SpeedUpFadeSpeedx2;
    public bool StartFadeImage;
    public bool FinishFadeOUT;
    public bool FinishFadeIN;
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
    void FixedUpdate()
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
                FadeImage.fillAmount = 0.0f;
            }
        }
    }
}
