using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll_BG : MonoBehaviour
{
    //Thanks :: http://mizusoba.blog.fc2.com/blog-entry-802.html?sp


    //��ʏc�T�C�Y�Ɖ��T�C�Y
    public int WINDOW_X = 1920;
    public int WINDOW_Y = 1080;

    //X���x�AY���x
    public float speedX = -4;
    public float speedY = 0;

    private RectTransform myImage;

    //��ʒu
    Vector2 DefPos;
    //��T�C�Y
    Vector2 defsize;

    private void Awake()
    {

        myImage = GetComponent<RectTransform>();

        DefPos = new Vector2(0, 0);

        //�f�t�H���g�T�C�Y�擾
        defsize = myImage.sizeDelta;
        //�g����̃T�C�Y
        float extx = defsize.x;
        float exty = defsize.y;

        if (speedX != 0)
        {
            //�T�C�Y���g��
            extx = defsize.x;
            while (true)
            {
                extx += defsize.x;
                if (extx > WINDOW_X * 2) break;
            }
            //��ʒu�𐧒�
            DefPos.x = (extx - WINDOW_X) / 2;
            if (speedX > 0) DefPos.x *= -1;
        }
        if (speedY != 0)
        {
            //�T�C�Y���g��
            exty = defsize.y;
            while (true)
            {
                exty += defsize.y;
                if (exty > WINDOW_Y * 2) break;
            }
            //��ʒu�𐧒�
            DefPos.y = (exty - WINDOW_Y) / 2;
            if (speedY > 0) DefPos.y *= -1;
        }

        myImage.sizeDelta = new Vector2(extx, exty);
        myImage.anchoredPosition = DefPos;

    }




    void FixedUpdate()
    {
        myImage.anchoredPosition += new Vector2(speedX, 0);
        if (speedX < 0)
        {
            if (myImage.anchoredPosition.x < DefPos.x - defsize.x) myImage.anchoredPosition += new Vector2(defsize.x, 0);
        }
        else if (speedX > 0)
        {
            if (myImage.anchoredPosition.x > DefPos.x + defsize.x) myImage.anchoredPosition -= new Vector2(defsize.x, 0);
        }


        myImage.anchoredPosition += new Vector2(0, speedY);
        if (speedY < 0)
        {
            if (myImage.anchoredPosition.y < DefPos.y - defsize.y) myImage.anchoredPosition += new Vector2(0, defsize.y);
        }
        else if (speedY > 0)
        {
            if (myImage.anchoredPosition.y > DefPos.y + defsize.y) myImage.anchoredPosition -= new Vector2(0, defsize.y);
        }
    }
}
