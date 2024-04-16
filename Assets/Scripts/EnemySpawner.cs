using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public float spawnTime = 2f; // Set the enemies spawn time
    public GameObject leftBound; // Left boundary object
    public GameObject rightBound; // Right boundary object
    public float[] spawnLayers; // Array of possible Y positions depending on the number of layers
    public int maxEnemiesPerLayer = 5; // Maximum number of enemies per layer
    private Dictionary<float, int> enemyCounts = new Dictionary<float, int>(); // Track the number of enemies per layer

    // Wave parameters
    private int currentWave = 0;
    public int totalWaves = 3;
    private int[] waveEnemyCounts = { 10, 20, 30 };

    // Number of enemies remaining in the current wave
    private int enemiesRemainingInWave;

    // Start is called before the first frame update
    void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        Debug.Log("Wave " + (currentWave + 1) + " begins!"); // Debug log indicating the start of a new wave

        enemiesRemainingInWave = waveEnemyCounts[currentWave];

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesRemainingInWave > 0)
        {
            SpawnEnemy();
            enemiesRemainingInWave--;

            yield return new WaitForSeconds(spawnTime);
        }

        // If there are no enemies remaining, start the next wave
        currentWave++;
        if (currentWave < totalWaves)
        {
            StartCoroutine(StartNextWave());
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(3f); // Delay before starting the next wave

        StartWave();
    }

    // Spawn enemy between left and right bounds
    void SpawnEnemy()
    {
        float minX = leftBound.transform.position.x;
        float maxX = rightBound.transform.position.x;

        // Randomly select a layer
        float targetLayer = spawnLayers[Random.Range(0, spawnLayers.Length)];

        // Check if the maximum number of enemies per layer has been reached
        if (!enemyCounts.ContainsKey(targetLayer) || enemyCounts[targetLayer] < maxEnemiesPerLayer)
        {
            float randomX = Random.Range(minX, maxX); // Randomize X position between left and right bounds

            // Spawn enemy if the condition is met
            GameObject enemy = Instantiate(Enemy, new Vector2(randomX, targetLayer), Quaternion.identity);

            // Update the count of enemies spawned on this layer
            if (enemyCounts.ContainsKey(targetLayer))
                enemyCounts[targetLayer]++;
            else
                enemyCounts[targetLayer] = 1;
        }
    }

    public void DecrementEnemyCount(float layer) // Decrement the enemy count when an enemy is destroyed
    {
        if (enemyCounts.ContainsKey(layer))
        {
            enemyCounts[layer]--;
        }
    }
}