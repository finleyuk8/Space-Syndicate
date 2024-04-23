using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject enemyBullet;
    public float minFireRate = 0.5f; // Minimum fire rate
    public float maxFireRate = 2.0f; // Maximum fire rate

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireRepeatedly()); // Start firing bullets repeatedly
    }
    IEnumerator FireRepeatedly() // Coroutine to fire bullets repeatedly
    {
        while (true) 
        {
            float randomFireRate = Random.Range(minFireRate, maxFireRate); // Randomize the fire rate
            yield return new WaitForSeconds(randomFireRate); 
            Fire();
        }
    }

    void Fire() // Function to fire a bullet
    {
        GameObject playerShip = GameObject.FindWithTag("Player"); // Find the player object
        if (playerShip != null) // Check if the player object exists
        {
            GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity); // Instantiate a bullet
            Vector2 direction = playerShip.transform.position - bullet.transform.position; // Calculate the direction of the bullet
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
