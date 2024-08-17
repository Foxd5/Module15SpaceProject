using UnityEngine;
using MinimalShooting.ControllerPackage;  // Make sure to include the namespace

public class ShipMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust as needed
    private KeyboardController controller;

    // Variables for clamping the ship's position
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // Get reference to the KeyboardController script attached to the same object
        controller = GetComponent<KeyboardController>();

        // Calculate screen bounds
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Get the ship's width and height for proper boundary clamping
        objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;  // Half of the width
        objectHeight = GetComponent<SpriteRenderer>().bounds.extents.y;  // Half of the height
    }

    void Update()
    {
        // Use the input vector from the KeyboardController to move the ship
        Vector3 movement = controller.InputVector;
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Clamp the ship's position within screen boundaries
        ClampPosition();
    }

    // Function to clamp the ship's position within the screen bounds
    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        transform.position = pos;
    }
}

