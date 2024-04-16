using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : MonoBehaviour
{
    [SerializeField] Transform targetDestination;
    [SerializeField] float moveSpeed;
    [SerializeField] int maxHealth = 3; // Maximum health of the enemy
    int currentHealth; // Current health of the enemy
    GameObject targetGameobject;
    Rigidbody2D rb;
    public GameObject Explosion;
    private float layer; // Store the Y position of the layer the enemy spawns in

    private float dropTimer = 0f; // Timer for dropping behavior
    private bool dropping = false; // Flag to indicate if the enemy is dropping

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        targetGameobject = GameObject.FindWithTag("Player"); // Find the player object
        layer = transform.position.y; // Store the Y position of the layer the enemy spawns in
        currentHealth = maxHealth; // Set current health to max health
    }

    private void FixedUpdate()
    {
        if (targetGameobject != null)
        {
            Vector3 direction = (targetGameobject.transform.position - transform.position).normalized;
            direction.y = 0; // Only follow along the x-axis
            rb.velocity = direction * moveSpeed;

            // Handle dropping behavior
            dropTimer += Time.fixedDeltaTime;
            if (dropTimer >= 5f)
            {
                if (!dropping)
                    StartCoroutine(DropAndReturn());
            }
        }
    }

    IEnumerator DropAndReturn()
    {
        dropping = true;
        float startY = transform.position.y;

        // Drop down
        while (transform.position.y > layer - 7) // Drop down to 2 units below layer
        {
            transform.position -= new Vector3(0, 0.1f, 0);
            yield return null;
        }

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Return to original y position
        while (transform.position.y < startY)
        {
            transform.position += new Vector3(0, 0.1f, 0);
            yield return null;
        }

        dropping = false;
        dropTimer = 0f;
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

}