using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int BulletDamage;
    public int Bullet_Damage { set{ BulletDamage = value; } }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")//�G����
        {
            //2022/01/15 �e�ۂ���G�ւ̃_���[�W���菈��
            //BaseEnemy�̕����͊Y������X�N���v�g��(�N���X������������)�ɕύX����ׂ�
            //collision.gameObject.GetCompornent<BaseEnemy>().TakeDamage(BulletDamage); 
        }

        Destroy(this.gameObject);
    }

}
