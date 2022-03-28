using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreamBullet : MonoBehaviour
{
    [SerializeField] int damage;
    float destroyTimer = 0;
    Rigidbody rigidbody;
    float knockbackPower = 3;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Shot(Vector3 target)
    {
        transform.LookAt(target);
        rigidbody.AddForce(transform.forward * 50, ForceMode.Impulse);
    }

    private void Update()
    {
        destroyTimer += Time.deltaTime;
        if (destroyTimer > 20)
        {
            Destroy(this.gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            IDamageable character = other.GetComponent<IDamageable>();
            if (character != null)
            {
                character.TakeDamage(damage, transform.forward * knockbackPower);
                Destroy(this.gameObject);
            }
        }
    }
}
