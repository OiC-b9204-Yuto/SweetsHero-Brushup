using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float  ExplosivTime;
    [SerializeField] private float  ExplosivRadius;
    [SerializeField] private int    ExplosivDamage;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explosion", ExplosivTime);
    }

    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosivRadius);
        foreach(var collider in colliders)
        {
            if (collider.gameObject.tag != "Enemy")
            {
                continue;
            }
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, collider.transform.position - transform.position, out hit))
            {
                continue;
            }
            if (hit.collider == collider)
            {
                //collision.gameObject.GetCompornent<BaseEnemy>().TakeDamage(ExplosivDamage); 
                Debug.Log("Grenade Hit!");
            }
        }
        Destroy(this.gameObject);
    }
}
