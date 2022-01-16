using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�����Œ�l�̃p�����[�^�p�N���X
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Create EnemyParameter")]
public class EnemyParameter : ScriptableObject
{
    [SerializeField] private string enemyName;
    public string EnemyName { get { return enemyName; } }

    [SerializeField] private int maxHealth;
    public int MaxHealth { get { return maxHealth; } }
}
