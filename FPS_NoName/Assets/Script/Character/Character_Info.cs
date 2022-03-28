using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character_Info : MonoBehaviour, IDamageable, IHeal
{
    public bool IsGetDamage; //ダメージを受けたかどうか
    private bool IsBattle; //現在戦闘状態かどうか
    private float DamageEffectShownTimer; //ダメージエフェクトの表示時間
    [SerializeField] private float character_BattleTimer; //キャラクターの戦闘時間
    [SerializeField] private int character_CurrentWeapon;　//キャラクターの現在の武器
    [SerializeField] private int character_MaxWeapons;　//キャラクターの最大武器数
    [SerializeField] private int character_CurrentGrenades;　//キャラクターの現在グレネード数
    [SerializeField] private int character_GetKeys;　//キャラクターの現在取得している鍵の数
    [SerializeField] private float character_CurrentHP;　//キャラクターの現在体力
    [SerializeField] private float character_MaxHP; //キャラクターの最大体力
    [SerializeField] private float character_CurrentArmor; //キャラクターの現在アーマー
    [SerializeField] private float character_MaxArmor; //キャラクターの最大アーマー
    [SerializeField] private bool character_IsMove; //キャラクターが現在動いているかどうか
    [SerializeField] private bool character_IsReload; //キャラクターが現在リロードしているかどうか
    [SerializeField] private float[] character_MovementSpeed; //キャラクターの移動速度
    [SerializeField] private AudioClip character_HitSE; //キャラクターの当たられた時の音

    public bool IsKnockback { get; private set; }

    private void Update()
    {
        CheckCurrentCharaState();
        RegenArmor();
    }

    private void CheckCurrentCharaState() //キャラクターの現在状態の確認
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

    private void RegenArmor() //アーマーの自動回復
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
