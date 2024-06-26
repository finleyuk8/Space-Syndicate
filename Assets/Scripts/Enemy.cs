using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform targetDestination;
    [SerializeField] float moveSpeed;
    [SerializeField] int maxHealth; // Maximum health of the enemy
    int currentHealth; // Current health of the enemy
    GameObject targetGameobject;
    Rigidbody2D rb;
    public GameObject Explosion;
    public GameObject BulletPickupPrefab; // Bullet pickup prefab
    public GameObject HealthPickupPrefab; // Health pickup prefab
    public GameObject InfiniteAmmoPickupPrefab; // Infinite ammo pickup prefab
    public float pickupMoveSpeed = 5f; // Speed of the bullet pickup movement towards the player
    private float layer; // Store the Y position of the layer the enemy spawns in
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetGameobject = GameObject.FindWithTag("Player");
        layer = transform.position.y;
        currentHealth = maxHealth; // Set current health to max health
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
            Destroy(col.gameObject); //Destroy the enemy

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
                float infiniteAmmoDropChance = Random.Range(1, 16); // Random value between 1 and 20 for infinite ammo pickup

                if (bulletDropChance == 1) // value to change the drop chance for bullets
                {
                    DropBulletPickup();
                }

                if (healthDropChance == 1) // value to change the drop chance for health pickups
                {
                    DropHealthPickup();
                }

                if (infiniteAmmoDropChance == 1) // value to change the drop chance for infinite ammo pickups
                {
                    DropInfiteAmmo();
                }
            
        }
            else
            {
                Debug.Log("Enemy health: " + currentHealth); //If enemy is not destroyed, log the current health
            }
        }
    }

    void ExplosionAnimation()
    {
        GameObject explosion = Instantiate(Explosion); // Instantiate the explosion
        explosion.transform.position = transform.position; // Set the position of the explosion at the enemy's position
    }

    void DropBulletPickup()
    {
        GameObject bulletPickup = Instantiate(BulletPickupPrefab, transform.position, Quaternion.identity); // Instantiate the bullet pickup at the enemy's position
        Vector3 directionToPlayer = (targetGameobject.transform.position - transform.position).normalized; // Get the direction towards the player
        // Add force to make the bullet pickup move towards the player
        Rigidbody2D rb = bulletPickup.GetComponent<Rigidbody2D>();
        rb.AddForce(directionToPlayer * pickupMoveSpeed, ForceMode2D.Impulse);
    }

    void DropHealthPickup()
    {
        GameObject healthPickup = Instantiate(HealthPickupPrefab, transform.position, Quaternion.identity); // Instantiate the health pickup at the enemy's position
        Vector3 directionToPlayer = (targetGameobject.transform.position - transform.position).normalized; // Get the direction towards the player
        // Add force to make the health pickup move towards the player
        Rigidbody2D rb = healthPickup.GetComponent<Rigidbody2D>();
        rb.AddForce(directionToPlayer * pickupMoveSpeed, ForceMode2D.Impulse);
    }

    void DropInfiteAmmo()
    {
        GameObject infiniteAmmoPickup = Instantiate(InfiniteAmmoPickupPrefab, transform.position, Quaternion.identity); // Instantiate the infinite pickup at the enemy's position
        Vector3 directionToPlayer = (targetGameobject.transform.position - transform.position).normalized; // Get the direction towards the player
        // Add force to make the infinite ammo pickup move towards the player
        Rigidbody2D rb = infiniteAmmoPickup.GetComponent<Rigidbody2D>();
        rb.AddForce(directionToPlayer * pickupMoveSpeed, ForceMode2D.Impulse);
    }
}