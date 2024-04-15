using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteAmmoPickup : MonoBehaviour
{
    // Duration of the infinite ammo effect
    public float pickupDuration = 15f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the PlayerMovement component from the player GameObject
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            // Check if the PlayerMovement component exists
            if (playerMovement != null)
            {
                // Activate infinite ammo effect
                playerMovement.ActivateInfiniteAmmo();

                // Destroy the infinite ammo pickup
                Destroy(gameObject);
            }
        }
    }
}