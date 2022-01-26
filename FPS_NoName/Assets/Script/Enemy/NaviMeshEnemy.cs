using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaviMeshEnemy : BaseEnemy
{
    [SerializeField] private float searchRadius = 10.0f;
    [SerializeField] private Transform target;
    NavMeshAgent navMeshAgent;
    Animator animator;


    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //Ž€–Sˆ—
            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        currentHealth = MaxHealth;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Debug.Log(navMeshAgent  .pathStatus);
        if (target)
        {
            navMeshAgent.SetDestination(target.position);
            animator.SetBool("isMove", navMeshAgent.velocity.sqrMagnitude > 0.1f);
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
