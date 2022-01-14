using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private bool IsDead;
    [SerializeField] private string enemy_Name;
    [SerializeField] private bool IsRespawnObject;
    [SerializeField] private float RespawnTimer;
    [SerializeField] private int enemy_Health;
    public int Enemy_Health { get { return enemy_Health; } set { value = enemy_Health; } }
    public string Enemy_Name { get { return enemy_Name; } }


    void Update()
    {
        CheckEnemyState();
    }

    void CheckEnemyState()
    {
        if (enemy_Health <= 0 && !IsRespawnObject)
        {
            Destroy(this);
        }
        else if (enemy_Health <= 0 && IsRespawnObject)
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag == "Bullet")
        {

        }
    }
}
