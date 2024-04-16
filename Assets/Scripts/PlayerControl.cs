using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject PlayerBullet;
    public GameObject BulletPos;

    // Movement control
    public float moveSpeed;
    public float maxHealth = 3; // Maximum health of the player
    float currentHealth; // Current health of the player
    public int maxBullets = 30; // Maximum number of bullets
    int remainingBullets; // Number of bullets remaining
    Rigidbody2D rb;
    Vector2 moveDirection;

    // Infinite ammo control
    bool hasInfiniteAmmo = false;
    float infiniteAmmoDuration = 15f;
    float currentInfiniteAmmoTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // Initialize current health to max health
        remainingBullets = maxBullets; // Initialize remaining bullets
    }

    void Update()
    {
        InputManagement();

        // Shoot bullet upon pressing left mouse button if there are remaining bullets or if player has infinite ammo
        if (Input.GetMouseButtonDown(0) && (remainingBullets > 0 || hasInfiniteAmmo))
        {
            GameObject bullet = Instantiate(PlayerBullet);
            bullet.transform.position = BulletPos.transform.position; // Set the bullet's initial position

            if (!hasInfiniteAmmo)
                remainingBullets--; // Reduce remaining bullets only if not in infinite ammo mode

            Debug.Log("Remaining Bullets: " + remainingBullets); // Debug log remaining bullets count
        }

        // Update infinite ammo duration
        if (hasInfiniteAmmo)
        {
            currentInfiniteAmmoTime -= Time.deltaTime;
            if (currentInfiniteAmmoTime <= 0)
            {
                hasInfiniteAmmo = false;
                // Reset the player's ammo to a default value
            }
            else
            {
                Debug.Log("Infinite Ammo Active. Time Remaining: " + currentInfiniteAmmoTime); // Debug log infinite ammo status and remaining time
            }
        }
    }

    private void FixedUpdate()
    {
        Move(); // Move the player
    }

    void InputManagement() // Get the input from the player
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move() // Move the player
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Detect collision with the enemy bullet
        if (col.tag == "EnemyBullet")
        {
            Destroy(col.gameObject);
            currentHealth -= 1; // Decrease player's current health
            Debug.Log("Player hit by enemy bullet. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }


        }

        // Detect collision with the boss bullet
        else if (col.tag == "BossBullet")
        {
            Destroy(col.gameObject);
            currentHealth -= 2.5f; // Decrease player's current health by 5
            Debug.Log("Player hit by boss bullet. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }
        }

        // Detect collision with the boss bullet
        else if (col.tag == "BossBullet2")
        {
            Destroy(col.gameObject);
            currentHealth -= 3f; // Decrease player's current health by 5
            Debug.Log("Player hit by boss bullet. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }
        }

        // Detect collision with the boss bullet
        else if (col.tag == "Fireball")
        {
            Destroy(col.gameObject);
            currentHealth -= 2.5f; // Decrease player's current health by 5
            Debug.Log("Player hit by fireball. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }
        }

        else if (col.tag == "EnemyShip")
        {
            
            currentHealth -= 10f; // Decrease player's current health by 20
            Debug.Log("Player hit by enemy ship. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }
        }

        else if (col.tag == "EnemyBullet3")
        {
            Destroy(col.gameObject);
            currentHealth -= 2f; // Decrease player's current health by 5
            Debug.Log("Player hit by fireball. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }
        }

        // Handle collision with pickup objects
        if (col.CompareTag("InfiniteAmmoPickup"))
        {
            // Activate infinite ammo effect
            ActivateInfiniteAmmo();
            // Destroy the pickup
            Destroy(col.gameObject);
            Debug.Log("Infinite Ammo Pickup Collected"); // Debug log pickup collection
        }
    }

    // Method to activate infinite ammo pickup effect
    public void ActivateInfiniteAmmo()
    {
        hasInfiniteAmmo = true;
        currentInfiniteAmmoTime = infiniteAmmoDuration;
        Debug.Log("Infinite Ammo Activated"); // Debug log infinite ammo activation
    }


    public void AddBullets(int amount)
    {
        remainingBullets += amount; // Add bullets to the player's remaining bullets
    }

    public void RestoreHealth(float amount)
    {
        currentHealth += (int)amount;
        // Ensure health doesn't exceed max health
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        Debug.Log("Player health restored. Current health: " + currentHealth);
    }
}