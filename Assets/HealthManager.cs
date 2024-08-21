using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f; 

    // Start is called before the first frame update
    void Start()
    {

        if (healthBar == null)
        {
            Debug.LogError("Health bar Image is not assigned in the Inspector."); //debug testing !
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0) //testing damage and healing. enter to take dam,
                               //space to heal, if at 0 level should reset. 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Return)) //enter does 20 damage to ship
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //space bar heals 5 damage
        {
            Heal(5);
        }
        
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }
}
