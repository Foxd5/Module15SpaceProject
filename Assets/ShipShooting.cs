using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{

    public GameObject BulletPrefab;  // Reference to the bullet prefab
    public Transform firePoint;      // The point where the bullet will be instantiated
    public float bulletSpeed = 10f;  // Speed of the bullet

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Shoot()
    {
        // Instantiate the bullet at the firePoint position
        GameObject bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);

        // Add velocity to the bullet to make it move
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, firePoint.right);

        rb.velocity = firePoint.up * bulletSpeed;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to shoot
        {
            Shoot();
        }
    }
}
