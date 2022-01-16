using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Info : MonoBehaviour
{
    [SerializeField] private int character_CurrentWeapon;
    [SerializeField] private int character_CurrentGrenades;
    [SerializeField] private float character_CurrentHP;
    [SerializeField] private float character_MaxHP;
    [SerializeField] private float character_CurrentArmor;
    [SerializeField] private float character_MaxArmor;
    public int Character_CurrentWeapon { get { return character_CurrentWeapon; } set { value = character_CurrentWeapon; } }
    public int Character_CurrentGrenades { get { return character_CurrentGrenades; } set { value = character_CurrentGrenades; } }

    public float Character_CurrentHP { get { return character_CurrentHP; } set { value = character_CurrentHP; } }
    public float Character_MaxHP { get { return character_MaxHP; } }

    public float Character_CurrentArmor { get { return character_CurrentArmor; } set { value = character_CurrentArmor; } }
    public float Character_MaxArmor { get { return character_MaxArmor; } }
}
