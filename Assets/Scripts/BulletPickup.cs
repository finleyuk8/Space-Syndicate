using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    public int bulletAmount; // Number of bullets to add when picked up

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>(); // Get the PlayerMovement component from the player GameObject
            if (playerMovement != null)  // Check if the PlayerMovement component exists
            {
                playerMovement.AddBullets(bulletAmount);  // Increase the remaining bullets count in the PlayerMovement script
                Destroy(gameObject); // Destroy the pickup GameObject
            }
        }
    }
}