using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    Vector2 bulletDirection;
    bool Ready;

     void Awake()
    {
        Ready = false;
    }
    
    void Start()
    {
       
    }

    public void SetDirection(Vector2 direction)
    {
        bulletDirection = direction.normalized;
        Ready = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Ready)
        {
            Vector2 position = transform.position;
            position += bulletDirection * speed * Time.deltaTime;
            transform.position = position;

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
            if ((transform.position.x < min.x) || (transform.position.x > max.x) || (transform.position.y < min.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }
        }

        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //Detect collision with the enemy
        if (col.tag == "Player")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            Debug.Log("Player ship destroyed.");
        }
    }
}
