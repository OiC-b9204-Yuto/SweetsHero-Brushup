using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int BulletDamage;
    public int Bullet_Damage { set{ BulletDamage = value; } }

    private GameObject owner;

    public GameObject Owner { get { return owner; } set { owner = value; } }

    private Vector3 BeforePosition;

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject HitParticle;
    [SerializeField] private LayerMask Layer;
    //消えるまでの時間 <- new! (空などの障害物のない方向へ飛ばした時の対策)
    [SerializeField] private float DeleteTime;

    private void Start()
    {
        BeforePosition = transform.position;
    }

    private void Update()
    {
        HitCheck();
        DeleteTime -= Time.deltaTime;
        if(DeleteTime <= 0) Destroy(this.gameObject);
        BeforePosition = transform.position;
    }

    //何かに当たった時の判定
    //HitParticleのためにRayに変更 <- Colliderが要らないかも
    private void HitCheck()
    {
        RaycastHit[] hits = Physics.RaycastAll(BeforePosition, transform.forward,Vector3.Distance(BeforePosition,transform.position) + rigidbody.velocity.magnitude * Time.deltaTime, Layer);
        //Debug.DrawRay(BeforePosition, transform.forward * (Vector3.Distance(BeforePosition, transform.position) + rigidbody.velocity.magnitude * Time.deltaTime), Color.green,1);

        if (hits == null) return;
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.tag != "Enemy" || hit.collider.isTrigger == true)
            {
                continue;
            }
            hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(BulletDamage, transform.forward * 2);
            GameObject obj = Instantiate(HitParticle, hit.point, Quaternion.identity);
            obj.transform.LookAt(Camera.main.transform.position);
            Destroy(this.gameObject);
            break;
        }
    }
}
