using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeRobotBottom : CakeRobot
{
    [SerializeField] Transform SpoonR;
    [SerializeField] Transform SpoonRCreamPos;
    [SerializeField] Transform SpoonL;
    [SerializeField] Transform SpoonLCreamPos;

    [SerializeField] GameObject creamBulletPrefab;

    private Transform target;

    int count = 0;

    public bool IsAttack { get; protected set; }

    float attackTimer;

    CreamBullet creamBullet;

    private void Start()
    {
        currentHealth = MaxHealth;
        target = FindObjectOfType<Character_Info>().transform;
    }

    public override void SkillAttack()
    {
        if(IsAttack)
        {
            return;
        }

        count++;
        if( count % 2 == 0)
        {
            GameObject obj = Instantiate(creamBulletPrefab, SpoonRCreamPos);
            creamBullet = obj.GetComponent<CreamBullet>();
            IsAttack = true;
            attackTimer = 0;
            StartCoroutine(AttackStart());
        }
        else
        {
            GameObject obj = Instantiate(creamBulletPrefab, SpoonLCreamPos);
            creamBullet = obj.GetComponent<CreamBullet>();
            IsAttack = true;
            attackTimer = 0;
            StartCoroutine(AttackStart());
        }
    }

    private IEnumerator AttackStart()
    {
        while (attackTimer < 1.0f)
        {
            if (count % 2 == 0)
            {
                SpoonR.transform.Rotate(new Vector3(40.0f * Time.deltaTime, 0f, 0f), Space.Self);
            }
            else
            {
                SpoonL.transform.Rotate(new Vector3(40.0f * Time.deltaTime, 0f, 0f), Space.Self);
            }
            attackTimer += Time.deltaTime;
            yield return null;
        }
        creamBullet.transform.parent = null;
        creamBullet.Shot(target.position);
        while (attackTimer < 2.0f)
        {
            if (count % 2 == 0)
            {
                SpoonR.transform.Rotate(new Vector3(-40.0f * Time.deltaTime, 0f, 0f), Space.Self);
            }
            else
            {
                SpoonL.transform.Rotate(new Vector3(-40.0f * Time.deltaTime, 0f, 0f), Space.Self);
            }
            attackTimer += Time.deltaTime;
            yield return null;
        }
        SpoonR.transform.localEulerAngles = new Vector3(-20, 0, 0);
        SpoonL.transform.localEulerAngles = new Vector3(-20, 0, 0);
        IsAttack = false;
    }
}
