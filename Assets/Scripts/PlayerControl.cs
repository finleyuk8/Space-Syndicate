using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject PlayerBullet;
    public GameObject BulletPos;

    // Movement control
    public float moveSpeed;
    public int maxHealth = 3; // Maximum health of the player
    int currentHealth; // Current health of the player
    public int maxBullets = 30; // Maximum number of bullets
    int remainingBullets; // Number of bullets remaining
    Rigidbody2D rb;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // Initialize current health to max health
        remainingBullets = maxBullets; // Initialize remaining bullets
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();

        // Shoot bullet upon pressing left mouse button if there are remaining bullets
        if (Input.GetMouseButtonDown(0) && remainingBullets > 0)
        {
            GameObject bullet = Instantiate(PlayerBullet);
            bullet.transform.position = BulletPos.transform.position; // Set the bullet's initial position
            remainingBullets--; // Reduce remaining bullets
        }

        // Debug log the remaining bullets count
        Debug.Log("Remaining Bullets: " + remainingBullets);
    }

    private void FixedUpdate()
    {
        Move();
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
            currentHealth--; // Decrease player's current health
            Debug.Log("Player hit by enemy bullet. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }
        }
    }
}

