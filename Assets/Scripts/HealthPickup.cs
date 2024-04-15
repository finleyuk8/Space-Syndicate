using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestoreAmount = 25; // Amount of health restored to the player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the PlayerMovement component from the player GameObject
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            // Check if the PlayerMovement component exists
            if (playerMovement != null)
            {
                // Restore health to the player
                playerMovement.RestoreHealth(healthRestoreAmount);

                // Destroy the health pickup
                Destroy(gameObject);
            }
        }
    }
}