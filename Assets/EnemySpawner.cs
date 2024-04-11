using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public float spawnTime = 2f; // Reduced spawn time
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

    // Spawn enemy within camera bounds
    public void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Get the bottom-left corner of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Get the top-right corner of the screen

        // Randomly select a layer
        float targetLayer = spawnLayers[Random.Range(0, spawnLayers.Length)];

        // Check if the maximum number of enemies per layer has been reached
        if (!enemyCounts.ContainsKey(targetLayer) || enemyCounts[targetLayer] < maxEnemiesPerLayer)
        {
            float randomX = Random.Range(min.x, max.x); // Randomize X position within the screen bounds

            // Spawn enemy if the condition is met
            GameObject enemy = Instantiate(Enemy, new Vector2(randomX, targetLayer), Quaternion.identity);

            // Update the count of enemies spawned on this layer
            if (enemyCounts.ContainsKey(targetLayer))
                enemyCounts[targetLayer]++;
            else
                enemyCounts[targetLayer] = 1;
        }
    }
}