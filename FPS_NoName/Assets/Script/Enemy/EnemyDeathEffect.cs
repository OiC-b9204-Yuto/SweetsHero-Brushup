using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    BaseEnemy Enemy;
    [SerializeField] private GameObject[] MeshObjects;
    [SerializeField] private Material[] MeshMaterial;
    [SerializeField] private float DisolveSpeed;
    void Awake()
    {
        MeshMaterial = new Material[MeshObjects.Length];
        Enemy = this.GetComponent<BaseEnemy>();
        for (int i = 0; i < MeshObjects.Length; i++)
        {
            MeshMaterial[i] = MeshObjects[i].GetComponent<Renderer>().material;
        }
    }
    void Update()
    {

        if (Enemy.CurrentHealth <= 0)
        {
            float GetValue = MeshMaterial[0].GetFloat("_Threshold");
            GetValue += DisolveSpeed * Time.deltaTime;
            for (int i = 0; i < MeshObjects.Length; i++)
            {
                MeshMaterial[i].SetFloat("_Threshold", GetValue);
            }
            if (GetValue >= 1.0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
