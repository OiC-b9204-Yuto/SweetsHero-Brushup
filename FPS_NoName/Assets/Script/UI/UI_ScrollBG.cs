using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScrollBG : MonoBehaviour
{
    [SerializeField] private float ScrollSpeedX;
    [SerializeField] private float ScrollSpeedY;
    private Image ScrollImage;

    private void Awake()
    {
        ScrollImage = this.GetComponent<Image>();
    }

    void Update()
    {
        ScrollImage.material.mainTextureOffset += new Vector2(ScrollSpeedX * Time.deltaTime, ScrollSpeedY * Time.deltaTime);
    }
}
