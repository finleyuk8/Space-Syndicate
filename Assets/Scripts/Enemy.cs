using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform targetDestination;
    [SerializeField] float moveSpeed;
    GameObject targetGameobject;
    Rigidbody2D rb;
    public GameObject Explosion;
    private float layer; // Store the Y position of the layer the enemy spawns in

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetGameobject = GameObject.FindWithTag("Player");
        if (targetGameobject == null)
        {
            Debug.LogError("Player GameObject not found!");
        }
        layer = transform.position.y; // Store the Y position of the layer the enemy spawns in
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

            // Update the count of enemies in the layer
            EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>(); // Find the EnemySpawner script
            if (enemySpawner != null)
            {
                enemySpawner.DecrementEnemyCount(layer);
            }
        }
    }

    void ExplosionAnimation()
    {
        GameObject explosion = Instantiate(Explosion); // Instantiate the explosion
        explosion.transform.position = transform.position; // Set the position of the explosion
    }
}
