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

    bool[] brokenCheckArray;
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
        foreach (var item in cakeRobots)
        {
            item.brokenEvent.AddListener(() => BrokenCheck());
            item.repairEvent.AddListener(() => BrokenCheck());
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
        barrier.SetActive(true);

        for (int i = 0; i < cakeRobots.Count; i++)
        {
            if(brokenCheckArray[i])
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
        mainGameManager.MainGame_IsGameClear = true;
    }

    void TakeDamage()
    {
        currentHealth -= 40;
        if (currentHealth <= MaxHealth * 0.5f)
        {
            attackCooldownTime = 2;
        }

        if (currentHealth <= 0)
        {
            IsDead = true;
        }
    }

    void Update()
    {
        if (IsDead)
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
            Debug.Log("SkillAttack");
            int index = UnityEngine.Random.Range(0, cakeRobots.Count);
            while (brokenCheckArray[index])
            {
                index = UnityEngine.Random.Range(0, cakeRobots.Count);
            }
            cakeRobots[index].SkillAttack();
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
