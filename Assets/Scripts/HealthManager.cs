using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public int MaxLives = 3;
    private int CurrentLives;
    public TextMeshProUGUI LivesCounterText;

    // Start is called before the first frame update
    void Start()
    {
        CurrentLives = MaxLives;
        UpdateLivesUI();


        /* if (healthBar == null)
        {
            Debug.LogError("Health bar Image is not assigned in the Inspector."); //debug testing!!! yay!
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0) //testing damage and healing. enter to take dam,
                               //space to heal, if at 0 level should reset. 
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);//This line just reloaded the scene. I want to lose lives until the game ends.
            LoseLife();
        }
        if (Input.GetKeyDown(KeyCode.Return)) //enter does 20 damage to ship
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.B)) //B button heals 5 damage
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
    void LoseLife()
    {
        CurrentLives--;
        UpdateLivesUI();

        if(CurrentLives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //remember, just resets the scene.
        }
        else
        {
            healthAmount = 100f;
            healthBar.fillAmount = healthAmount / 100f; 
        }
    }
    void UpdateLivesUI()
    {
        LivesCounterText.text = "Lives: " + CurrentLives;
    }

}
