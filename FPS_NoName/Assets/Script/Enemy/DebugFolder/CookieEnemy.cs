using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CookieEnemy : EnemyBase
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


    public override void TakeDamage(int damage, Vector3 pow)
    {
        if (!target)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius * 5);
            foreach (var collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    target = collider.transform;
                }
            }
        }
        transform.position += pow;
        CurrentHealth -= damage;
    }

    protected override void Awake()
    {
        CurrentHealth = parameter.MaxHealth;
        AttackCol.GetComponent<Enemy_Attack>().SetParameter(this.parameter);
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
        if (CurrentHealth <= 0.0f)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
        }
        if (target)
        {
            targetTimer -= Time.deltaTime;
            //ターゲットの設定
            navMeshAgent.SetDestination(target.position);
            animator.SetBool("isMove", navMeshAgent.velocity.sqrMagnitude > 0.1f);
            //視線が届くか調べる
            RaycastHit rayHit;
            LayerMask layerMask = 1 << LayerMask.NameToLayer("Player");
            if (Physics.Raycast(this.transform.position, target.position - this.transform.position, out rayHit, 100.0f, layerMask) &&
                rayHit.collider.tag == "Player")
            {
                //ターゲット用のタイマーを初期値に戻す
                targetTimer = initTargetTimer;
            }
        }
        else
        {
            target = PlayerSearch().transform;
        }

        if (targetTimer <= 0)
        {
            //初期に戻る処理
            target = null;
            navMeshAgent.SetDestination(startPos);
        }
    }

    private GameObject PlayerSearch()
    {
        //索敵範囲内の検知
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (var collider in colliders)
        {
            if (collider.tag == "Player") continue;
            //視野内に入っているか
            if (Vector3.Angle(this.transform.forward, collider.transform.position - this.transform.position) < searchAngle * 0.5f)
            {
                return collider.gameObject;
            }
        }
        return null;
    }
    
    protected void DeadAction()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
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

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
        Handles.color = new Color(1, 0, 0, 0.1f);
        Handles.DrawSolidArc(this.transform.position, Vector3.up, Quaternion.Euler(0.0f, -searchAngle * 0.5f, 0.0f) * transform.forward, searchAngle, searchRadius);
    }
#endif

    public void OnAttack() //攻撃中
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = navMeshAgent.velocity * 0.5f;
        AttackCol.SetActive(true);

    }

    public void AttackFinish() //攻撃終
    {
        navMeshAgent.isStopped = false;
        AttackCol.SetActive(false);
    }

}
