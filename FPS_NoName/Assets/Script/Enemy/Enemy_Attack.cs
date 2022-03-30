using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    private EnemyParameter parameter;

    public void SetParameter(EnemyParameter param) => this.parameter = param;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<IDamageable>().TakeDamage(parameter.AttackPower,(other.transform.position - this.transform.position).normalized * parameter.KnocbackPower);
        }
    }
}
