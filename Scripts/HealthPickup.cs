using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestoreAmount = 25; // Amount of health restored to the player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider belongs to the player
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>(); // Get the PlayerMovement component
            if (playerMovement != null) // Check if the PlayerMovement component exists
            {
                playerMovement.RestoreHealth(healthRestoreAmount); // Restore health to the player
                Destroy(gameObject); // Destroy the health pickup
            }
        }
    }
}