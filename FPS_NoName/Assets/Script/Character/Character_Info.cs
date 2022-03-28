using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character_Info : MonoBehaviour, IDamageable, IHeal
{
    public bool IsGetDamage; //�_���[�W���󂯂����ǂ���
    private bool IsBattle; //���ݐ퓬��Ԃ��ǂ���
    private float DamageEffectShownTimer; //�_���[�W�G�t�F�N�g�̕\������
    [SerializeField] private float character_BattleTimer; //�L�����N�^�[�̐퓬����
    [SerializeField] private int character_CurrentWeapon;�@//�L�����N�^�[�̌��݂̕���
    [SerializeField] private int character_MaxWeapons;�@//�L�����N�^�[�̍ő啐�퐔
    [SerializeField] private int character_CurrentGrenades;�@//�L�����N�^�[�̌��݃O���l�[�h��
    [SerializeField] private int character_GetKeys;�@//�L�����N�^�[�̌��ݎ擾���Ă��錮�̐�
    [SerializeField] private float character_CurrentHP;�@//�L�����N�^�[�̌��ݑ̗�
    [SerializeField] private float character_MaxHP; //�L�����N�^�[�̍ő�̗�
    [SerializeField] private float character_CurrentArmor; //�L�����N�^�[�̌��݃A�[�}�[
    [SerializeField] private float character_MaxArmor; //�L�����N�^�[�̍ő�A�[�}�[
    [SerializeField] private bool character_IsMove; //�L�����N�^�[�����ݓ����Ă��邩�ǂ���
    [SerializeField] private bool character_IsReload; //�L�����N�^�[�����݃����[�h���Ă��邩�ǂ���
    [SerializeField] private float[] character_MovementSpeed; //�L�����N�^�[�̈ړ����x
    [SerializeField] private AudioClip character_HitSE; //�L�����N�^�[�̓�����ꂽ���̉�

    public bool IsKnockback { get; private set; }

    private void Update()
    {
        CheckCurrentCharaState();
        RegenArmor();
    }

    private void CheckCurrentCharaState() //�L�����N�^�[�̌��ݏ�Ԃ̊m�F
    {
        if (character_BattleTimer > 0.0f)
        {
            character_BattleTimer -= Time.deltaTime;
            IsBattle = true;
        }
        else if (character_BattleTimer <= 0.0f)
        {
            IsBattle = false;
        }

        if (DamageEffectShownTimer > 0.0f)
        {
            DamageEffectShownTimer -= Time.deltaTime;
            IsGetDamage = true;
        }
        else if(DamageEffectShownTimer <= 0.0f)
        {
            IsGetDamage = false;
        }
    }

    private void RegenArmor() //�A�[�}�[�̎�����
    {
        if (character_CurrentArmor >= character_MaxArmor)
        {
            return;
        }

        if (!IsBattle)
        {
            character_CurrentArmor += 0.1f;
        }
    }

    public int Character_CurrentWeapon
    {
        get { return character_CurrentWeapon; }
        set
        {
            character_CurrentWeapon = value;
            if (character_CurrentWeapon < 0)
            {
                character_CurrentWeapon = character_MaxWeapons - 1;
            }
            else if (character_CurrentWeapon >= character_MaxWeapons)
            {
                character_CurrentWeapon = 0;
            }
        }
    }

    public float Character_BattleTimer
    {
        get { return character_BattleTimer; }
    }
    public int Character_CurrentGrenades { get { return character_CurrentGrenades; } set { character_CurrentGrenades = value;} }
    public int Character_GetKeys { get { return character_GetKeys; } set { character_GetKeys = value; } }
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

    public void TakeHeal(float healamount)
    {
        if (Character_CurrentHP >= Character_MaxHP)
        {
            Character_CurrentHP = Character_MaxHP;
            return;
        }
        Character_CurrentHP += healamount;
    }

    public void TakeDamage(int damage, Vector3 pow)
    {
        AudioManager.Instance.SE.PlayOneShot(character_HitSE);
        DamageEffectShownTimer = 0.5f;
        if (Character_CurrentArmor >= damage)
        {
            character_BattleTimer = 10.0f;
            Character_CurrentArmor -= damage;
        }
        else
        {
            character_BattleTimer = 10.0f;
            Character_CurrentHP -= damage - Character_CurrentArmor;
            Character_CurrentArmor = 0;
        }
    }


    public float Character_MovementSpeed { get { return character_MovementSpeed[character_CurrentWeapon] * (character_IsReload ? 0.5f : 1.0f); }}
}
