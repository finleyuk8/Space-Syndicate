using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform targetDestination;
    [SerializeField] float moveSpeed;
    [SerializeField] int maxHealth = 3; // Maximum health of the enemy
    int currentHealth; // Current health of the enemy
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
        currentHealth = maxHealth; // Initialize current health to max health
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
        // Detect collision with the player bullet
        if (col.tag == "PlayerBullet")
        {
            Debug.Log("Enemy hit by bullet.");
            currentHealth--; // Decrease current health
            Destroy(col.gameObject);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the enemy if health reaches zero
                Debug.Log("Enemy ship destroyed.");

                // Update the count of enemies in the layer
                EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
                if (enemySpawner != null)
                {
                    enemySpawner.DecrementEnemyCount(layer);
                }

                // Play explosion animation when the enemy is destroyed
                ExplosionAnimation();
            }
            else
            {
                Debug.Log("Enemy health: " + currentHealth);
            }
        }
    }

    void ExplosionAnimation()
    {
        GameObject explosion = Instantiate(Explosion); // Instantiate the explosion
        explosion.transform.position = transform.position; // Set the position of the explosion
    }
}