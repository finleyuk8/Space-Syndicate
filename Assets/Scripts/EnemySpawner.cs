using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public float spawnTime = 2f; // Reduced spawn time
    public GameObject leftBound; // Left boundary object
    public GameObject rightBound; // Right boundary object
    public float[] spawnLayers; // Array of possible Y positions
    public int maxEnemiesPerLayer = 5; // Maximum number of enemies per layer
    private Dictionary<float, int> enemyCounts = new Dictionary<float, int>(); // Track the number of enemies per layer

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime); // Spawn the enemy after a delay and repeat
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Spawn enemy between left and right bounds
    public void SpawnEnemy()
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
    public void DecrementEnemyCount(float layer)
    {
        if (enemyCounts.ContainsKey(layer))
        {
            enemyCounts[layer]--;
        }
    }
}