using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaviMeshEnemy : BaseEnemy , IDamageable
{
    [SerializeField] private float searchRadius = 10.0f;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject AttackCol;
    NavMeshAgent navMeshAgent;
    Animator animator;
    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //éÄñSèàóù
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
    private void OnTriggerEnter(Collider player)
    {
        animator.SetBool("isAttack", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isAttack", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }

    public void OnAttack() //çUåÇíÜ
    {
        AttackCol.SetActive(true);
    }

    public void AttackFinish() //çUåÇèI
    {
        AttackCol.SetActive(false);
    }

}
