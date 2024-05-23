using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    public Slider easeStaminaSlider;
    public Slider staminaSlider;
    public PlayerStats playerStats;  // Reference to the PlayerStats component

    private float LerpSpeed = 2f;

    private void Start()
    {
        // Initialize the sliders
        staminaSlider.maxValue = playerStats.maxStamina;
        easeStaminaSlider.maxValue = playerStats.maxStamina;
        staminaSlider.value = playerStats.stamina;
        easeStaminaSlider.value = playerStats.stamina;
    }

    private void Update()
    {
        // Update the primary stamina slider with the current stamina from PlayerStats
        if (staminaSlider.value != playerStats.stamina)
        {
            staminaSlider.value = playerStats.stamina;
        }

        // Smoothly update the ease stamina slider to catch up to the primary slider
        if (easeStaminaSlider.value != staminaSlider.value)
        {
            easeStaminaSlider.value = Mathf.Lerp(easeStaminaSlider.value, staminaSlider.value, LerpSpeed * Time.deltaTime);
        }
    }
}
