using UnityEngine;
using MinimalShooting.ControllerPackage;  // Make sure to include the namespace

public class ShipMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust as needed
    private KeyboardController controller;

    void Start()
    {
        // Get reference to the KeyboardController script attached to the same object
        controller = GetComponent<KeyboardController>();
    }

    void Update()
    {
        // Use the input vector from the KeyboardController to move the ship
        Vector3 movement = controller.InputVector;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}

