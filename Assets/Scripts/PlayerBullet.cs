using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get the bullets current position
        Vector2 position = transform.position;

        //Work out and update the new bullet position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        transform.position = position; 

        //Destroy bullet if it goes off screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if(transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

}
