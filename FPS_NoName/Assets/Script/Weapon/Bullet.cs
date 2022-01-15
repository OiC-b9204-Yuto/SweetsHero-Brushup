using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int BulletDamage;
    public int Bullet_Damage { set{ BulletDamage = value; } }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")//敵判別
        {
            //2022/01/15 弾丸から敵へのダメージ判定処理
            //BaseEnemyの部分は該当するスクリプト名(クラス名だったかも)に変更するべし
            //collision.gameObject.GetCompornent<BaseEnemy>().TakeDamage(BulletDamage); 
        }

        Destroy(this.gameObject);
    }

}
