using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeRobotMiddle : CakeRobot
{

    public bool IsAttack { get; protected set; }

    float attackTimer;

    [SerializeField] ParticleSystem smoke;

    [SerializeField] NaviMeshEnemy cookieEnemy;
    [SerializeField] List<Transform> spawnPointList;

    private void Start()
    {
        currentHealth = MaxHealth;
        brokenEvent.AddListener(() => smoke.Play());
        repairEvent.AddListener(() => smoke.Stop());
    }

    public override void SkillAttack()
    {
        Instantiate(cookieEnemy.gameObject, spawnPointList[Random.Range(0, spawnPointList.Count)].position, Quaternion.identity);
    }
}
