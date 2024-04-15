using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    public int bulletAmount = 10; // Number of bullets to add when picked up

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerMovement component from the player GameObject
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            // Check if the PlayerMovement component exists
            if (playerMovement != null)
            {
                // Increase the remaining bullets count in the PlayerMovement script
                playerMovement.AddBullets(bulletAmount);

                // Destroy the pickup GameObject
                Destroy(gameObject);
            }
        }
    }
}