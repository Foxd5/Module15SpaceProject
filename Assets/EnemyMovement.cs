using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;  // Movement speed
    public float moveTime = 2f;   // Time spent moving in one direction
    public float waitTime = 1f;   // Time spent waiting between direction changes

    private Vector3 randomDirection;
    private float moveTimer;
    private float waitTimer;
    private bool isWaiting;

    private Vector2 screenBounds;     // Screen boundaries
    private float objectWidth;        // Half-width of the enemy ship
    private float objectHeight;       // Half-height of the enemy ship

    void Start()
    {
        SetRandomDirection();
        moveTimer = moveTime;
        waitTimer = waitTime;

        // Calculate the screen bounds in world units
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Get the bounds from the MeshRenderer component
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        objectWidth = meshRenderer.bounds.extents.x;  // Half the width of the mesh
        objectHeight = meshRenderer.bounds.extents.y; // Half the height of the mesh
    }

    void Update()
    {
        if (!isWaiting)
        {
            MoveEnemy();
        }
        else
        {
            Wait();
        }

        // Check if the enemy is hitting the screen boundary and change direction if needed
        CheckBoundsAndChangeDirection();
    }

    void MoveEnemy()
    {
        // Move the enemy in the current random direction
        transform.position += randomDirection * moveSpeed * Time.deltaTime;
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            isWaiting = true;
            waitTimer = waitTime;
        }
    }

    void Wait()
    {
        // Wait for a short time before moving again
        waitTimer -= Time.deltaTime;

        if (waitTimer <= 0)
        {
            SetRandomDirection();
            isWaiting = false;
            moveTimer = moveTime;
        }
    }

    void SetRandomDirection()
    {
        // Set a new random direction for movement
        randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
    }

    void CheckBoundsAndChangeDirection()
    {
        Vector3 pos = transform.position;

        // If the enemy ship hits the screen boundary, reverse its direction on that axis
        if (pos.x >= screenBounds.x - objectWidth || pos.x <= -screenBounds.x + objectWidth)
        {
            // Reverse the X direction if hitting the left or right screen bounds
            randomDirection.x = -randomDirection.x;
        }

        if (pos.y >= screenBounds.y - objectHeight || pos.y <= -screenBounds.y + objectHeight)
        {
            // Reverse the Y direction if hitting the top or bottom screen bounds
            randomDirection.y = -randomDirection.y;
        }

        // Clamp the position to prevent the enemy ship from moving off-screen
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        transform.position = pos;
    }
}