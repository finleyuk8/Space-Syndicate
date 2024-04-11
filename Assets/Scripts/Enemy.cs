using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform targetDestination;
    [SerializeField] float moveSpeed;
    GameObject targetGameobject;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetGameobject = targetDestination.gameObject;
    }
    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameobject)
        {
           Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Attacking");
    }
}
