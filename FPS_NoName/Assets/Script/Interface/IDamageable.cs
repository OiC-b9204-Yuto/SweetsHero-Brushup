using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    void TakeDamage(int damage, Vector3 pow);
    //TODO�FEnemy�̔�e���^�[�Q�b�g�����ȗ��̂��߈����Ɏ��sObject�ǉ�
    //      �ύX�ӏ����������o��Ǝv���邽�ߊm�F���Ă������
    //void TakeDamage(int damage, Vector3 pow, GameObject causer);
}
