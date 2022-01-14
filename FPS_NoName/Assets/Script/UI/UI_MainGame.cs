using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainGame : MonoBehaviour
{
    [SerializeField] private Text Weapon_Name;
    [SerializeField] private Text Weapon_AmmoText;
    [SerializeField] private Text Weapon_StateMessage;

    Weapon_State Weapon_Stats;
    void Start()
    {
        Weapon_Stats = GameObject.Find("Weapon").GetComponent<Weapon_State>();
    }

    void Update()
    {
        RefreshUIText();
    }

    void RefreshUIText()
    {
        if (Weapon_Stats.IsReload)
        {
            Weapon_StateMessage.text = "ÉäÉçÅ[ÉhíÜÇ≈Ç∑";
            Weapon_StateMessage.color = Color.green;
        }
        else if (Weapon_Stats.IsNoAmmo)
        {
            Weapon_StateMessage.text = "íeêÿÇÍ";
            Weapon_StateMessage.color = Color.red;
        }
        else
        {
            Weapon_StateMessage.text = "";
        }
        Weapon_AmmoText.text = Weapon_Stats.Weapon_CurrentAmmo.ToString() +" / " + Weapon_Stats.Weapon_CurrentMagazine.ToString();
        
    }
}
