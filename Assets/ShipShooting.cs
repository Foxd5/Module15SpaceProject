using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ShipShooting : MonoBehaviour
{
    public GameObject BulletPrefab;      // Reference to the bullet prefab
    public Transform firePoint;          // The point where the bullet will be instantiated
    public float bulletSpeed = 10f;      // Speed of the bullet
    public int maxBullets = 10;          // Maximum number of bullets before reloading
    private int currentBullets;          // Current bullets available
    public float reloadTime = 2f;        // Time it takes to reload
    private bool isReloading = false;    // Whether the ship is currently reloading

    public TextMeshProUGUI bulletCounterText;       // Reference to UI Text for displaying bullet count

    void Start()
    {
        currentBullets = maxBullets;     // Initialize with full bullets
        UpdateBulletUI();                // Update the UI with the current bullet count
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && currentBullets > 0) // Fire if bullets are available
        {
            Shoot();
        }

        // Reload if out of bullets
        if (currentBullets <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the firePoint position
        GameObject bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);

        // Add velocity to the bullet to make it move
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, firePoint.right);

        rb.velocity = firePoint.up * bulletSpeed;

        currentBullets--;  // Decrease the bullet count
        UpdateBulletUI();  // Update the UI with the new bullet count
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);  // Wait for reload time

        currentBullets = maxBullets;  // Reset the bullet count
        isReloading = false;

        UpdateBulletUI();  // Update the UI with the reloaded bullet count
        Debug.Log("Reloaded!");
    }

    void UpdateBulletUI()
    {
        bulletCounterText.text = "Bullets: " + currentBullets;  // Update the bullet counter on screen
    }
}
