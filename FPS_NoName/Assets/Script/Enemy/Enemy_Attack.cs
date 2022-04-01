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
            Vector3 dir = other.transform.position - this.transform.position;
            dir.y = 0;
            other.GetComponent<IDamageable>().TakeDamage(parameter.AttackPower,(dir.normalized * parameter.KnocbackPower));
        }
    }
}
