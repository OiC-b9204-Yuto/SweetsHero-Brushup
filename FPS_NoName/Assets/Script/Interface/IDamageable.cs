using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    void TakeDamage(int damage, Vector3 pow);
    //TODO：Enemyの被弾時ターゲット処理簡略のため引数に実行Object追加
    //      変更箇所がいくつか出ると思われるため確認してから実装
    //void TakeDamage(int damage, Vector3 pow, GameObject causer);
}
