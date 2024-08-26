using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ShipShooting : MonoBehaviour
{
    public GameObject BulletPrefab;      
    public Transform firePoint;          // fire point is where bulle is being launched from 
    public float bulletSpeed = 10f;      
    public int maxBullets = 10;          // usable bullets before reload
    private int currentBullets;          // current bullets available
    public float reloadTime = 2f;        // time it takes to reload
    private bool isReloading = false;    // whether the ship is currently reloading
    public float fireRate = .2f;
    private float nextFireTime = 0f;    

    public TextMeshProUGUI bulletCounterText;

    void Start()
    {
        currentBullets = maxBullets;     //start with full bullets and update bullet UI
        UpdateBulletUI();                
    }

    void Update()
    {
        if (isReloading)
            return;

        //fire if bullets are available, and space is held down OR pressed
        if (Input.GetKey(KeyCode.Space) && currentBullets > 0 && Time.time >= nextFireTime) // Fire if bullets are available
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        // eload if out of bullets
        if (currentBullets <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        
        GameObject bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);

        // add velocity to the bullet to make it move. Should add ships velocity to this to make more realistic
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, firePoint.right);

        rb.velocity = firePoint.up * bulletSpeed;

        currentBullets--;  
        UpdateBulletUI(); 
    }

    IEnumerator Reload()
    {
        isReloading = true;
        //Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);  

        currentBullets = maxBullets;  
        isReloading = false;

        UpdateBulletUI();  
        //Debug.Log("Reloaded!");
    }

    void UpdateBulletUI()
    {
        //bulletCounterText.text = "Bullets: " + currentBullets; 
        string bulletTicks = "Ammo: ";
        for (int i = 0; i < currentBullets; i++)
        {
            bulletTicks += "|";  // adds tick mark for each bullet
        }

        
        bulletCounterText.text = bulletTicks;
    }
}
