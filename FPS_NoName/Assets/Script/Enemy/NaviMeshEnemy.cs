using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaviMeshEnemy : BaseEnemy
{
    [SerializeField] private float searchRadius = 10.0f;
    [SerializeField] private Transform target;
    NavMeshAgent navMeshAgent;


    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //Ž€–Sˆ—
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target)
        {
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
            foreach (var collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    target = collider.transform;
                }
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }

}
