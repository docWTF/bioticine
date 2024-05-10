using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider easehealthSlider;
    public Slider healthSlider;
    public PlayerStats playerStats;  // Reference to the PlayerStats component

    private float LerpSpeed = 5f;

    private void Start()
    {
        // Initialize the sliders
        healthSlider.maxValue = playerStats.maxHealth;
        easehealthSlider.maxValue = playerStats.maxHealth;
        healthSlider.value = playerStats.health;
        easehealthSlider.value = playerStats.health;
    }

    private void Update()
    {
        // Update the primary health slider with the current health from PlayerStats
        if (healthSlider.value != playerStats.health)
        {
            healthSlider.value = playerStats.health;
        }

        // Smoothly update the ease health slider to catch up to the primary slider
        if (easehealthSlider.value != healthSlider.value)
        {
            easehealthSlider.value = Mathf.Lerp(easehealthSlider.value, healthSlider.value, LerpSpeed * Time.deltaTime);
        }
    }
}
