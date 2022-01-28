using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CookieKing : BaseEnemy, IDamageable
{
    [HideInInspector] public UnityEvent defeatedEvent;

    public bool IsDead { get; private set; }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) {
            IsDead = true;
            defeatedEvent.Invoke();
        }
    }
    
    public void Revive()
    {
        currentHealth = MaxHealth;
        IsDead = false;
    }
}
