using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaviMeshEnemy : BaseEnemy , IDamageable
{
    [SerializeField] private float searchRadius = 10.0f;
    [SerializeField] private float searchAngle = 120.0f;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject AttackCol;
    NavMeshAgent navMeshAgent;
    Animator animator;

    Vector3 startPos;

    const float initTargetTimer = 10.0f;
    float targetTimer = initTargetTimer;
    

    public void TakeDamage(int damage)
    {
        if (!target)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius*3);
            foreach (var collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    target = collider.transform;
                }
            }
        }

        currentHealth -= damage;
    }

    void Awake()
    {
        currentHealth = MaxHealth;
    }

    void Start()
    {
        startPos = transform.position;
        AttackCol.SetActive(false);
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (currentHealth <= 0.0f)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
        }
        if (target)
        {
            targetTimer -= Time.deltaTime;
            //�^�[�Q�b�g�̐ݒ�
            navMeshAgent.SetDestination(target.position);
            animator.SetBool("isMove", navMeshAgent.velocity.sqrMagnitude > 0.1f);
            //�������͂������ׂ�
            RaycastHit rayHit;
            LayerMask layerMask = 1 << LayerMask.NameToLayer("Player");
            if (Physics.Raycast(this.transform.position, target.position - this.transform.position, out rayHit, 100.0f, layerMask) &&
                rayHit.collider.tag == "Player")
            {
                //�^�[�Q�b�g�p�̃^�C�}�[�������l�ɖ߂�
                targetTimer = initTargetTimer;
            }
        }
        else
        {
            //���G�͈͓��̌��m
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
            foreach (var collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    //������ɓ����Ă��邩
                    if (Vector3.Angle(this.transform.forward, collider.transform.position - this.transform.position) < searchAngle * 0.5f) {
                        target = collider.transform;
                        targetTimer = initTargetTimer;
                    }
                }
            }
        }

        if (targetTimer <= 0)
        {
            //�����ɖ߂鏈��
            target = null;
            navMeshAgent.SetDestination(startPos);
        }
    }
    private void OnTriggerEnter(Collider player)
    {
        if (!target || player.tag != "Player")
        {
            return;
        }
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

    public void OnAttack() //�U����
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = navMeshAgent.velocity * 0.5f;
        AttackCol.SetActive(true);

    }

    public void AttackFinish() //�U���I
    {
        navMeshAgent.isStopped = false;
        AttackCol.SetActive(false);
    }

}
