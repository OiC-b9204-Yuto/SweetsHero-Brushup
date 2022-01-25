using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int BulletDamage;
    public int Bullet_Damage { set{ BulletDamage = value; } }

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject HitParticle;

    private void Update()
    {
        HitCheck();
    }

    //âΩÇ©Ç…ìñÇΩÇ¡ÇΩéûÇÃîªíË
    //HitParticleÇÃÇΩÇﬂÇ…RayÇ…ïœçX <- ColliderÇ™óvÇÁÇ»Ç¢Ç©Ç‡
    private void HitCheck()
    {
        RaycastHit Hit;

        bool rayCheck = Physics.Raycast(this.transform.position, transform.forward, out Hit, rigidbody.velocity.magnitude * Time.deltaTime * 2);

        if (!rayCheck) return;
        if (Hit.collider.gameObject.tag != "Enemy")
        {
            Debug.Log(Hit.collider.gameObject.name);
            return;
        }
        else
        {
            Debug.Log("ìGÇ…ñΩíÜ");
            //Hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(BulletDamage);
            GameObject obj = Instantiate(HitParticle, Hit.point, Quaternion.identity);
            obj.transform.LookAt(Camera.main.transform.position);
        }
        
        Destroy(this.gameObject);
    }
}
