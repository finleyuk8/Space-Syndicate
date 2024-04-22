using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
//using new library for TMP and UI
public class HealthIndicators : MonoBehaviour
{
    public Image healthIndicatorImage; // Reference to the health image component displaying health indicators
    public Sprite[] healthSprites; // Array of sprites for different health levels

    public TextMeshProUGUI healthText; // Reference to the health text component (TMP)

    public PlayerMovement player; // allows me to access the player's health

    // Update is called once per frame
    void Update()
    {
        UpdateHealthIndicator();
        UpdateHealthText();
    }

    void UpdateHealthIndicator()
    {
        // Calculate the health percentage
        float healthPercentage = player.currentHealth / player.maxHealth;

        // decide what sprite to use
        Sprite healthSprite = GetHealthSprite(healthPercentage);

        // Update the health indicator image
        healthIndicatorImage.sprite = healthSprite;
    }

    void UpdateHealthText()
    {
        // Update the health text
        healthText.text = player.currentHealth.ToString();
    }

    Sprite GetHealthSprite(float healthPercentage)
    {
        // Define thresholds for different health levels
        float[] healthThresholds = { 1f, 0.9f, 0.6f, 0.45f, 0.3f, 0.15f };
        //find appropriote sprite
        for (int i = 0; i < healthThresholds.Length; i++)
        {
            if (healthPercentage >= healthThresholds[i])
            {
                return healthSprites[i];
            }
        }
        //return the lowest health sprite if health is below all thresholds
        return healthSprites[healthSprites.Length - 1];
    }
}