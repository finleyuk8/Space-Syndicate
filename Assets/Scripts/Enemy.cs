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
        targetGameobject = GameObject.FindWithTag("Player");
        if (targetGameobject == null)
        {
            Debug.LogError("Player GameObject not found!");
        }
    }

    private void FixedUpdate()
    {
        if (targetGameobject != null)
        {
            Vector3 direction = (targetGameobject.transform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Attacking");
    }
}
