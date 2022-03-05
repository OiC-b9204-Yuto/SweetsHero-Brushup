using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Performance : MonoBehaviour
{
    [SerializeField] private GameObject PerformanceUI;
    [SerializeField] private Text FPSText;

    int FrameCount;
    float PreviewTime;
    float FPS;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        GameData_Manager.Instance.Load();
        if (GameData_Manager.Instance.gameData.FpsShown == 1)
        {
            PerformanceUI.SetActive(true);
        }
        else
        {
            PerformanceUI.SetActive(false);
        }

    }

    private void Start()
    {

        FrameCount = 0;
        PreviewTime = 0.0f;
        FPS = 0.0f;
    }

    private void Update()
    {
        CheckEnable();
        FpsCount(); //FPSŒvŽZŠÖ”
    }

    void CheckEnable()
    {
        if (GameData_Manager.Instance.gameData.FpsShown == 1)
        {
            PerformanceUI.SetActive(true);
        }
        else
        {
            PerformanceUI.SetActive(false);
        }
    }

    void FpsCount()
    {
        FrameCount++;
        float time = Time.realtimeSinceStartup - PreviewTime;

        if(time >= 0.15f)
        {
            FPS = FrameCount / time;

            FrameCount = 0;
            PreviewTime = Time.realtimeSinceStartup;
        }

        FPSText.text = "FPS:" + FPS.ToString("0");

    }
}
