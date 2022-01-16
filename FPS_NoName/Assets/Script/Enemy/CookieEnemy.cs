using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CookieEnemy : BaseEnemy
{
    Rigidbody _rigidbody;

    const float moveAcceleration = 14.0f;
    const float maxMoveSpeed = 30.0f;

    Vector3 moveDirection = new Vector3(0, 0, 0);
    float currentMoveSpeed = 0.0f;


    [SerializeField] private Transform player;

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //éÄñSèàóù
            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    void Start()
    {
        //ÉvÉåÉCÉÑÅ[ÇÃTransformÇéÊìæ
        var gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in gameObjects)
        {
            Debug.Log(item.name);
            Character_Info charaInfo = item.GetComponent<Character_Info>();
            if (charaInfo)
            {
                player = item.transform;
                break;
            }
        }
    }

    void Update()
    {
        moveDirection = new Vector3(
            player.position.x - this.transform.position.x ,0,
            player.position.z - this.transform.position.z).normalized;
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, Vector3.down, out hit))
        {
            float dot = Vector3.Dot(moveDirection, hit.normal);
            moveDirection = moveDirection - dot * hit.normal;
        }
    }

    void FixedUpdate()
    {
        if (_rigidbody.velocity.magnitude <= maxMoveSpeed)
        {
            _rigidbody.velocity += moveDirection * moveAcceleration * Time.fixedDeltaTime;
        }
        _rigidbody.velocity -= new Vector3(0, 10.0f * Time.fixedDeltaTime, 0);
    }
}
