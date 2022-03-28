using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵が持つ固定値のパラメータ用クラス
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Create EnemyParameter")]
public class EnemyParameter : ScriptableObject
{
    [SerializeField] private string enemyName;
    public string EnemyName { get { return enemyName; } }

    [SerializeField] private int maxHealth;
    public int MaxHealth { get { return maxHealth; } }

    [SerializeField] private int attackPower;
    public int AttackPower { get { return attackPower; } }

    [SerializeField] private int knocbackPower;
    public float KnocbackPower { get { return knocbackPower; } }
}
