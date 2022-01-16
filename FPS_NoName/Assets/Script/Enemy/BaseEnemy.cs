using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour , IDamageable
{
    [SerializeField] protected EnemyParameter parameter;
    protected int currentHealth;

    public string EnemyName { get { return parameter.EnemyName; }}
    public int MaxHealth { get { return parameter.MaxHealth; } }
    public int CurrentHealth { get { return CurrentHealth; } }

    public abstract void TakeDamage(int damage);
}
