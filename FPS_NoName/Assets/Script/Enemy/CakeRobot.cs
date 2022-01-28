using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CakeRobot : BaseEnemy, IDamageable
{
    [HideInInspector] public UnityEvent brokenEvent;
    [HideInInspector] public UnityEvent repairEvent;

    public bool IsBroken { get; protected set; }
    [SerializeField] protected float repairRequiredTime;
    protected float repairTimer;

    [SerializeField] protected ParticleSystem smoke;

    public void TakeDamage(int damage)
    { 
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            smoke.Play();
            IsBroken = true;
            brokenEvent.Invoke();
        }
    }

    private void Repair()
    {
        IsBroken = false;
        currentHealth = MaxHealth;
        repairEvent.Invoke();
        smoke.Stop();
    }

    protected virtual void Update()
    {
        if(IsBroken)
        {
            repairTimer += Time.deltaTime;
            if (repairTimer > repairRequiredTime)
            {
                repairTimer = 0;
                Repair();
            }
        }
    }

    public abstract void SkillAttack();
}
