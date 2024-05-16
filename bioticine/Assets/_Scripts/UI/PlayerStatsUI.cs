using UnityEngine;
using TMPro;

public class PlayerStatsUI : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text staminaText;
    public TMP_Text soulsText;
    public TMP_Text weaponDamageMultiplierText;
    public TMP_Text discoveryMultiplierText;
    public TMP_Text speedMultiplierText;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = PlayerStats.Instance;
    }

    void Update()
    {
        if (playerStats != null)
        {
            healthText.text = $"Health: {playerStats.health}/{playerStats.maxHealth}";
            staminaText.text = $"Stamina: {playerStats.stamina}/{playerStats.maxStamina}";
            soulsText.text = $"Souls: {playerStats.soulsCount}";
            weaponDamageMultiplierText.text = $"Weapon Damage Multiplier: {playerStats.weaponDamageMultiplier}";
            discoveryMultiplierText.text = $"Discovery Multiplier: {playerStats.discoveryMultiplier}";
            speedMultiplierText.text = $"Speed Multiplier: {playerStats.speedMultiplier}";
        }
    }
}
