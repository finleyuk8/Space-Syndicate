using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public float spawnTime = 2f;
    public GameObject leftBound;
    public GameObject rightBound;
    public float[] spawnLayers;
    public int maxEnemiesPerLayer = 5;
    private Dictionary<float, int> enemyCounts = new Dictionary<float, int>();

    private int currentWave = 0;
    public int totalWaves = 3;
    private int[] waveEnemyCounts = { 10, 20, 30 };

    void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        Debug.Log("Wave " + (currentWave + 1) + " begins!");

        int enemyCount = waveEnemyCounts[currentWave];

        if (currentWave == 2)
        {
            SpawnBoss(); // Spawn boss enemy at wave 3
            enemyCount -= 1; // Reduce enemy count by 1 to account for boss enemy
        }

        StartCoroutine(SpawnEnemies(enemyCount));
    }

    IEnumerator SpawnEnemies(int count)
    {
        while (count > 0)
        {
            SpawnEnemy();
            count--;

            yield return new WaitForSeconds(spawnTime);
        }

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
        yield return new WaitForSeconds(3f);
        StartWave();
    }

    void SpawnEnemy()
    {
        float minX = leftBound.transform.position.x;
        float maxX = rightBound.transform.position.x;

        float targetLayer = spawnLayers[Random.Range(0, spawnLayers.Length)];

        if (!enemyCounts.ContainsKey(targetLayer) || enemyCounts[targetLayer] < maxEnemiesPerLayer)
        {
            float randomX = Random.Range(minX, maxX);
            GameObject enemy = Instantiate(enemyPrefab, new Vector2(randomX, targetLayer), Quaternion.identity);

            if (enemyCounts.ContainsKey(targetLayer))
                enemyCounts[targetLayer]++;
            else
                enemyCounts[targetLayer] = 1;
        }
    }

    void SpawnBoss()
    {
        float bossLayer = spawnLayers[Random.Range(0, spawnLayers.Length)];
        float randomX = Random.Range(leftBound.transform.position.x, rightBound.transform.position.x);
        GameObject boss = Instantiate(bossPrefab, new Vector2(randomX, bossLayer), Quaternion.identity);
    }

    public void DecrementEnemyCount(float layer)
    {
        if (enemyCounts.ContainsKey(layer))
        {
            enemyCounts[layer]--;
        }
    }
}