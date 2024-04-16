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
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>(); // Get the PlayerMovement component
            if (playerMovement != null) // Check if the PlayerMovement component exists
            {
                playerMovement.ActivateInfiniteAmmo(); // Activate infinite ammo effect
                Destroy(gameObject); // Destroy the infinite ammo pickup
            }
        }
    }
}