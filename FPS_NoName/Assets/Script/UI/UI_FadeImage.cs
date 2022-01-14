using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeImage : MonoBehaviour
{
    [SerializeField] private float FadeTime;
    [SerializeField] private bool FadeOut;
    [SerializeField] private bool FadeIn;
    Image FadeImage;


    private void Awake()
    {
        FadeImage = this.GetComponent<Image>();
        if (FadeIn)
        {
            FadeImage.fillAmount = 1;
        }
        else if (FadeOut)
        {
            FadeImage.fillAmount = 0;
        }
    }
    void FixedUpdate()
    {
        FadeIN();
        FadeOUT();
    }

    void FadeIN()
    {
        if(FadeIn && !FadeOut)
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
        if (!FadeIn && FadeOut)
        {
            FadeImage.fillAmount += FadeTime;
            if(FadeImage.fillAmount >= 0.98)
            {
            }
        }
    }
}
