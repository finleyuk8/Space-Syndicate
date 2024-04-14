using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform targetDestination;
    [SerializeField] float moveSpeed;
    GameObject targetGameobject;
    Rigidbody2D rb;
    public GameObject Explosion;

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

    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision with the player bullet
        if (col.tag == "PlayerBullet")
        {
            ExplosionAnimation();
            Destroy(col.gameObject);
            Destroy(gameObject);
            Debug.Log("Enemy ship destroyed.");
        }
    }

    void ExplosionAnimation()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        Explosion.transform.position = transform.position;
    }


}
