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
        StartCoroutine(FireRepeatedly());
    }

    IEnumerator FireRepeatedly()
    {
        while (true)
        {
            float randomFireRate = Random.Range(minFireRate, maxFireRate);
            yield return new WaitForSeconds(randomFireRate);
            Fire();
        }
    }

    void Fire()
    {
        GameObject playerShip = GameObject.FindWithTag("Player");
        if (playerShip != null)
        {
            GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            Vector2 direction = playerShip.transform.position - bullet.transform.position;
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
