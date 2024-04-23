using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); // Get the screen bounds
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position; // Get the position of the object
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x, screenBounds.x); // Clamp the x position of the object
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y, screenBounds.y); // Clamp the y position of the object
        transform.position = viewPos; // Update the position of the object
    }
}
