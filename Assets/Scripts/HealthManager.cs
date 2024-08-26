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

    public GameObject miniShipPrefab;
    public Transform ShipLivesPanel;
    public GameObject GameOverPanel;

    public TextMeshProUGUI LivesCounterText; //for displaying lives in text. but i want it in ships!

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; //adding this fixed the issue of the game being paused upon gameover restart.
        CurrentLives = MaxLives;
        UpdateLivesUI();
        GameOverPanel.SetActive(false); //makes sure the gameover panel is hidden on startup


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
            GameOver();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name); //remember, just resets the scene.
        }
        else
        {
            healthAmount = 100f;
            healthBar.fillAmount = healthAmount / 100f; 
        }
    }
    void UpdateLivesUI()
    {
        foreach (Transform child in ShipLivesPanel)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < CurrentLives; i++)
        {
            GameObject miniShip = Instantiate(miniShipPrefab, ShipLivesPanel);

            RectTransform shipRect = miniShip.GetComponent<RectTransform>();
            shipRect.anchoredPosition = new Vector2(i * 100f, 0);

        }
        LivesCounterText.text = "Lives: "; //+ CurrentLives;//this was for the text display of lives. we want prefabs though!
    }
    void GameOver()
    {
        GameOverPanel.SetActive(true);

        Time.timeScale = 0f; //stops the game! 
    }
   public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
