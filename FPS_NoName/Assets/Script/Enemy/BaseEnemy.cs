using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected EnemyParameter parameter;
    [SerializeField] protected int currentHealth;

    public string EnemyName { get { return parameter.EnemyName; }}
    public int MaxHealth { get { return parameter.MaxHealth; } }
    public int CurrentHealth { get { return currentHealth; } }
}
