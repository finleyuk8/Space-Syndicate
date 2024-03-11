using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Movement control
    public float moveSpeed;
    Rigidbody2D rb;
    Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();

    }
    private void FixedUpdate()
    {
        Move();
    }

    void InputManagement() //Get the input from the player
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move() //Move the player
    {
        rb.velocity = moveDirection * moveSpeed;
    }

}

