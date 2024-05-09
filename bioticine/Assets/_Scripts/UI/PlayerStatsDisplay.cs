using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI hitpointDisplay;
    public TextMeshProUGUI staminaDisplay;
    


    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.Instance != null)
        {
            hitpointDisplay.text = Mathf.RoundToInt(PlayerStats.Instance.health).ToString() + "/" + Mathf.RoundToInt(PlayerStats.Instance.maxHealth).ToString();
            staminaDisplay.text = Mathf.RoundToInt(PlayerStats.Instance.stamina).ToString() + "/" + Mathf.RoundToInt(PlayerStats.Instance.maxStamina).ToString();
        }
    }
}
