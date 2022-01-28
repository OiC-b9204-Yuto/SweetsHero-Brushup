using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    CakeBossEnemy bossEnemy;

    private void Start()
    {
        bossEnemy = FindObjectOfType<CakeBossEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            bossEnemy.activate = true;
        }
    }
}
