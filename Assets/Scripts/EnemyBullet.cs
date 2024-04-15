using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed; // Speed of the bullet
    Vector2 bulletDirection; // Direction of the bullet
    bool Ready; // Check if the bullet is ready to move

     void Awake()
    {
        Ready = false;
    }
    
    public void SetDirection(Vector2 direction) // Set the direction of the bullet
    {
        bulletDirection = direction.normalized; 
        Ready = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Ready)  // Move the bullet if it is ready
        {
            Vector2 position = transform.position; 
            position += bulletDirection * speed * Time.deltaTime; 
            transform.position = position; // Update the bullet position

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
            if ((transform.position.x < min.x) || (transform.position.x > max.x) || (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject); // Destroy the bullet if it goes off screen
            }
        }

        
    }
    
}
