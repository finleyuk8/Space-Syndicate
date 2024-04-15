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
    public GameObject BulletPickupPrefab; // Bullet pickup prefab
    public float pickupMoveSpeed = 5f; // Speed of the bullet pickup movement towards the player
    private float layer; // Store the Y position of the layer the enemy spawns in
    public GameObject HealthPickupPrefab; // Health pickup prefab

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

                // Random chance to drop a bullet pickup
                float bulletDropChance = Random.Range(1, 6); // Random value between 1 and 5 for bullet pickup
                float healthDropChance = Random.Range(1, 11); // Random value between 1 and 10 for health pickup

                if (bulletDropChance == 1) // Adjust this value to change the drop chance for bullets
                {
                    DropBulletPickup();
                }

                if (healthDropChance == 1) // Adjust this value to change the drop chance for health pickups
                {
                    DropHealthPickup();
                }
            
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

    void DropBulletPickup()
    {
        // Instantiate the bullet pickup at the enemy's position
        GameObject bulletPickup = Instantiate(BulletPickupPrefab, transform.position, Quaternion.identity);

        // Get the direction towards the player
        Vector3 directionToPlayer = (targetGameobject.transform.position - transform.position).normalized;

        // Add force to make the bullet pickup move towards the player
        Rigidbody2D rb = bulletPickup.GetComponent<Rigidbody2D>();
        rb.AddForce(directionToPlayer * pickupMoveSpeed, ForceMode2D.Impulse);
    }

    void DropHealthPickup()
    {
        // Instantiate the health pickup at the enemy's position
        GameObject healthPickup = Instantiate(HealthPickupPrefab, transform.position, Quaternion.identity);

        // Get the direction towards the player
        Vector3 directionToPlayer = (targetGameobject.transform.position - transform.position).normalized;

        // Add force to make the health pickup move towards the player
        Rigidbody2D rb = healthPickup.GetComponent<Rigidbody2D>();
        rb.AddForce(directionToPlayer * pickupMoveSpeed, ForceMode2D.Impulse);
    }
}