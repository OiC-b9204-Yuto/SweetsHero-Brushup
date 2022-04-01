using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    private EnemyBase enemy;
    [SerializeField] private GameObject[] meshObjects;
    private Material[] meshMaterial;
    [SerializeField] private float disolveSpeed;

    public event Action OnDeathEffectStart;
    public event Action OnDeathEffectEnd;
    void Awake()
    {
        meshMaterial = new Material[meshObjects.Length];
        enemy = this.GetComponent<EnemyBase>();
        enemy.OnEnemyDead += () => StartCoroutine(DeathEffectPlay());
        for (int i = 0; i < meshObjects.Length; i++)
        {
            meshMaterial[i] = meshObjects[i].GetComponent<Renderer>().material;
        }
    }

    IEnumerator DeathEffectPlay()
    {
        OnDeathEffectStart?.Invoke();
        float getValue = meshMaterial[0].GetFloat("_Threshold");
        while (getValue < 1.0f)
        {
            getValue += disolveSpeed * Time.deltaTime;
            for (int i = 0; i < meshObjects.Length; i++)
            {
                meshMaterial[i].SetFloat("_Threshold", getValue);
            }
            yield return null;
            getValue = meshMaterial[0].GetFloat("_Threshold");
        }
        OnDeathEffectEnd?.Invoke();
        Destroy(this.gameObject);
    }
}
