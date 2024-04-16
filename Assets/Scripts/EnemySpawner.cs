using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public float spawnTime = 2f; // Time between enemy spawns
    public GameObject leftBound;
    public GameObject rightBound;
    public float[] spawnLayers; // Y positions of the layers where enemies can spawn
    public int maxEnemiesPerLayer = 5; // Maximum number of enemies that can spawn in a layer
    private Dictionary<float, int> enemyCounts = new Dictionary<float, int>(); // Dictionary to store the number of enemies in each layer

    private int currentWave = 0; // Current wave number
    public int totalWaves = 3; // Total number of waves
    private int[] waveEnemyCounts = { 10, 20, 30 }; // Number of enemies to spawn in each wave

    void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        Debug.Log("Wave " + (currentWave + 1) + " begins!"); 

        int enemyCount = waveEnemyCounts[currentWave]; // Get the number of enemies to spawn in the current wave

        if (currentWave == 2)
        {
            SpawnBoss(); // Spawn boss enemy at wave 3
            enemyCount -= 1; // Reduce enemy count by 1 to account for boss enemy
        }

        StartCoroutine(SpawnEnemies(enemyCount)); // Start spawning enemies
    }

    IEnumerator SpawnEnemies(int count) // Coroutine to spawn enemies
    {
        while (count > 0) // Loop until all enemies are spawned
        {
            SpawnEnemy(); // Spawn an enemy
            count--; // Decrease the enemy count

            yield return new WaitForSeconds(spawnTime); // Wait for spawnTime before spawning the next enemy
        }

        currentWave++; // Increment the wave number
        if (currentWave < totalWaves)
        {
            StartCoroutine(StartNextWave());
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }

    IEnumerator StartNextWave() // Coroutine to start the next wave
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds before starting the next wave
        StartWave(); 
    }

    void SpawnEnemy()
    {
        float minX = leftBound.transform.position.x; // Get the left bound position
        float maxX = rightBound.transform.position.x; // Get the right bound position

        float targetLayer = spawnLayers[Random.Range(0, spawnLayers.Length)]; // Get a random layer to spawn the enemy

        if (!enemyCounts.ContainsKey(targetLayer) || enemyCounts[targetLayer] < maxEnemiesPerLayer) // Check if the layer has reached the maximum enemy count
        {
            float randomX = Random.Range(minX, maxX); // Get a random X position within the bounds to spawn the enemy
            GameObject enemy = Instantiate(enemyPrefab, new Vector2(randomX, targetLayer), Quaternion.identity);

            if (enemyCounts.ContainsKey(targetLayer)) // Update the enemy count in the layer
                enemyCounts[targetLayer]++;
            else
                enemyCounts[targetLayer] = 1;
        }
    }

    void SpawnBoss() // Function to spawn the boss enemy
    {
        float bossLayer = spawnLayers[Random.Range(0, spawnLayers.Length)];
        float randomX = Random.Range(leftBound.transform.position.x, rightBound.transform.position.x);
        GameObject boss = Instantiate(bossPrefab, new Vector2(randomX, bossLayer), Quaternion.identity);
    }

    public void DecrementEnemyCount(float layer) // Function to decrement the enemy count in a layer
    {
        if (enemyCounts.ContainsKey(layer))
        {
            enemyCounts[layer]--;
        }
    }
}