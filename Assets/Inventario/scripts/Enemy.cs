using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public float moveSpeed = 4;
   Rigidbody2D rb;
   Transform target;
   UnityEngine.Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (target)
        {
            UnityEngine.Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //rb.rotation = angle;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.linearVelocity = new UnityEngine.Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

   



}
