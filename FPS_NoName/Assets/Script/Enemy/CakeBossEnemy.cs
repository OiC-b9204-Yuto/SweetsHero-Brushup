using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeBossEnemy : BaseEnemy 
{
    [SerializeField] private CookieKing cookieKing;
    [SerializeField] private List<CakeRobot> cakeRobots;

    [SerializeField] private GameObject barrier;

    public bool IsCakeRobotBroken { get; private set; }

    [SerializeField] bool[] brokenCheckArray;
    //プイレイヤー
    private GameObject player;
    //旋回用変数
    bool turn = false;
    Vector3 turnTargetPositon;
    float turnTimer = 0;
    //攻撃用変数
    float attackTimer = 0;
    [SerializeField] float attackCooldownTime = 10.0f;

    public bool IsDead { get; private set; }

    MainGameManager mainGameManager;

    void Start()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();
        brokenCheckArray = new bool[3];
        for (int i = 0; i < cakeRobots.Count; i++)
        {
            cakeRobots[i].brokenEvent.AddListener(() => BrokenCheck());
            cakeRobots[i].repairEvent.AddListener(() => BrokenCheck());
        }
        player = FindObjectOfType<Character_Info>().gameObject;
        //クッキーキングの討伐時のイベントに関数を登録
        cookieKing.defeatedEvent.AddListener(() => {
            TakeDamage();
            if (IsDead) StartCoroutine(GameClear());
            });
    }

    void BrokenCheck()
    {
        for (int i = 0; i < cakeRobots.Count; i++)
        {
            brokenCheckArray[i] = cakeRobots[i].IsBroken;
        }

        for (int i = 0; i < cakeRobots.Count; i++)
        {
            if(!cakeRobots[i].IsBroken)
            {
                return;
            }
        }

        IsCakeRobotBroken = true;
        barrier.SetActive(false);
    }

    IEnumerator GameClear()
    {
        yield return new WaitForSeconds(2.0f);
        mainGameManager.gameProgressState = GameProgressState.Game_IsGameClear;
    }

    void TakeDamage()
    {
        currentHealth -= currentHealth;
        if (currentHealth <= 0)
        {
            IsDead = true;
        }
    }

    public bool activate = false;

    void Update()
    {
        if (!activate || IsDead)
        {
            return;
        }
        TurnUpdate();
        AttackUpdate();
    }

    private void AttackUpdate()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackCooldownTime)
        {
            attackTimer = 0;
            int index = UnityEngine.Random.Range(0, cakeRobots.Count);
            Debug.Log(brokenCheckArray[index]);
            if (!brokenCheckArray[index])
            {
                cakeRobots[index].SkillAttack();
            }
        }

    }

    void TurnUpdate()
    {
        if (!turn)
        {
            Vector3 vector = (player.transform.position - transform.position);
            vector.y = 0;
            float dot = Vector3.Dot(transform.forward, vector);
            if (dot < 0.7)
            {
                turnTargetPositon = player.transform.position;
                turnTimer = 0;
                turn = true;
            }
        }
        else
        {
            Vector3 targetPositon = turnTargetPositon;

            if (transform.position.y != turnTargetPositon.y)
            {
                turnTargetPositon = new Vector3(turnTargetPositon.x, transform.position.y, turnTargetPositon.z);
            }
            Quaternion targetRotation = Quaternion.LookRotation(targetPositon - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*2);
            if (turnTimer >= 3.0f)
            {
                turn = false;
            }
            turnTimer += Time.deltaTime;
        }
    }
}
