using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Info : MonoBehaviour
{
    [SerializeField] private int character_CurrentWeapon;
    [SerializeField] private int character_MaxWeapons;
    [SerializeField] private int character_CurrentGrenades;
    [SerializeField] private float character_CurrentHP;
    [SerializeField] private float character_MaxHP;
    [SerializeField] private float character_CurrentArmor;
    [SerializeField] private float character_MaxArmor;
    public int Character_CurrentWeapon
    {
        get { return character_CurrentWeapon; } 
        set 
        { 
            character_CurrentWeapon = value;
            if (character_CurrentWeapon < 0)
            {
                character_CurrentWeapon = character_MaxWeapons;
            }
            else if(character_CurrentWeapon > character_MaxWeapons)
            {
                character_CurrentWeapon = 0;
            }
        } 
    }
    public int Character_CurrentGrenades { get { return character_CurrentGrenades; } set { character_CurrentGrenades = value; } }

    public float Character_CurrentHP { get { return character_CurrentHP; } set { character_CurrentHP = value; } }
    public float Character_MaxHP { get { return character_MaxHP; } }

    public float Character_CurrentArmor { get { return character_CurrentArmor; } set { character_CurrentArmor = value; } }
    public float Character_MaxArmor { get { return character_MaxArmor; } }
}
