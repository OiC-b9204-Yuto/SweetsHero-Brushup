using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected EnemyParameter parameter;
    private int currentHealth;
    public virtual int CurrentHealth
    {
        get { return currentHealth; }
        protected set
        {
            if (IsDead || currentHealth == value) return;
            currentHealth = value;
            OnCurrentHealthChanged?.Invoke();
            if (currentHealth <= 0)
            {
                IsDead = true;
                OnEnemyDead?.Invoke();
            }
        }
    }

    public string EnemyName { get { return parameter.EnemyName; } }
    public int MaxHealth { get { return parameter.MaxHealth; } }

    public int AttackPower { get { return parameter.AttackPower; } }
    public float KnocbackPower { get { return parameter.KnocbackPower; } }

    private bool isDead = false;
    public bool IsDead { get { return isDead; } protected set { isDead = value; } }

    /// <summary>
    /// �̗͂̐��l���ύX���ꂽ�Ƃ��ɌĂяo�����
    /// </summary>
    public event Action OnCurrentHealthChanged;
    /// <summary>
    /// ���S���ɌĂяo�����
    /// </summary>    
    public event Action OnEnemyDead;

    protected virtual void Awake()
    {
        CurrentHealth = parameter.MaxHealth;
    }

    public virtual void TakeDamage(int damage, Vector3 pow)
    {
        CurrentHealth -= damage;
    }
}
