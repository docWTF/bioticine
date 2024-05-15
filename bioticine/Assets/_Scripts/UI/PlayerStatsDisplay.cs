using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI soulsDisplay;
    


    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.Instance != null)
        {
            soulsDisplay.text = PlayerStats.Instance.soulsCount.ToString();
        }
    }
}
