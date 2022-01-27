using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float  ExplosivTime;
    [SerializeField] private float  ExplosivRadius;
    [SerializeField] private int    ExplosivDamage;
    [SerializeField] private GameObject ExplossionParticle;
    [SerializeField] private AudioClip SoundExplosiv;
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
                hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(ExplosivDamage);
                Debug.Log("Grenade Hit!");
            }
        }
        Instantiate(ExplossionParticle, transform.position, Quaternion.identity);
        AudioManager.Instance.SE.PlayOneShot(SoundExplosiv);
        Destroy(this.gameObject);
    }
}
