using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Info : MonoBehaviour , IDamageable
{
    [SerializeField] private int character_CurrentWeapon;
    [SerializeField] private int character_MaxWeapons;
    [SerializeField] private int character_CurrentGrenades;
    [SerializeField] private float character_CurrentHP;
    [SerializeField] private float character_MaxHP;
    [SerializeField] private float character_CurrentArmor;
    [SerializeField] private float character_MaxArmor;
    [SerializeField] private bool character_IsMove;
    [SerializeField] private bool character_IsReload;
    [SerializeField] private float[] character_MovementSpeed;
    public int Character_CurrentWeapon
    {get { return character_CurrentWeapon; } 
        set 
        { 
            character_CurrentWeapon = value;
            if (character_CurrentWeapon < 0)
            {
                character_CurrentWeapon = character_MaxWeapons - 1;
            }
            else if(character_CurrentWeapon >= character_MaxWeapons)
            {
                character_CurrentWeapon = 0;
            }
        } 
    }
    public int Character_CurrentGrenades { get { return character_CurrentGrenades; } set { character_CurrentGrenades = value;} }

    public float Character_CurrentHP 
    { get { return character_CurrentHP; } 
        private set 
        { 
            character_CurrentHP = value;
            if(character_CurrentHP < 0)
            {
                character_CurrentHP = 0;
            }
        } 
    }
    public float Character_MaxHP { get { return character_MaxHP; } }
    public float Character_CurrentArmor 
    { get { return character_CurrentArmor; } 
        private set 
        { 
            character_CurrentArmor = value;
            if (character_CurrentArmor < 0)
            {
                character_CurrentArmor = 0;
            }
        } 
    }
    public float Character_MaxArmor { get { return character_MaxArmor; } }

    public bool Character_IsMove { get { return character_IsMove; } set { character_IsMove = value; } }
    public bool Character_IsReload { get { return character_IsReload; } set { character_IsReload = value; } }
    public void TakeDamage(int damage)
    {
        if(Character_CurrentArmor >= damage)
        {
            Character_CurrentArmor -= damage;
        }
        else
        {
            Character_CurrentHP -= damage - Character_CurrentArmor;
            Character_CurrentArmor = 0;
        }
    }
    public float Character_MovementSpeed { get { return character_MovementSpeed[character_CurrentWeapon] * (character_IsReload ? 0.5f : 1.0f); }}
}
