using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerLevelingUI : MonoBehaviour
{
    public GameObject levelingUI;
    public bool isLeveling;

    public int uiHealthLevel;
    public int uiStaminaLevel;
    public int uiDamageLevel;
    public int uiDexLevel;
    public int uiDiscoveryLevel;
    public int uiSoulsCount;
    public int uiSoulLevel;
    public int uiSoulsRequirement;

    public int uiHealthCoefficient;
    public int uiStaminaCoefficient;
    public int uiDamageCoefficient;
    public int uiDexCoefficient;
    public int uiDiscoveryCoefficient;
    public int uiLevelingCoefficient;

    public TextMeshProUGUI levelHealth;
    public TextMeshProUGUI levelStamina;
    public TextMeshProUGUI levelDamage;
    public TextMeshProUGUI levelDexterity;
    public TextMeshProUGUI levelLuck;
    public TextMeshProUGUI soulsCount;
    public TextMeshProUGUI soulsLevel;
    public TextMeshProUGUI soulsRequirement;

    public TextMeshProUGUI levelHealthMod;
    public TextMeshProUGUI levelStaminaMod;
    public TextMeshProUGUI levelDamageMod;
    public TextMeshProUGUI levelDexterityMod;
    public TextMeshProUGUI levelLuckMod;

    public Button[] increaseButton;
    public Button[] decreaseButton;
    public Button confirmButton;
    public Button cancelButton;


    private void Awake()
    {
        levelingUI.SetActive(false);
        isLeveling = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLevelingUI()
    {
        if (isLeveling == false && PlayerStats.Instance.isAllowLevelUp)
        {
            isLeveling = true;
            levelingUI.SetActive(true);
            UpdatePlayerStatsUI();
            UpdatePlayerStatsDisplay();
            UpdateLevelingButtons();
            return;
        }
        else if (isLeveling == true)
        {
            isLeveling = false;
            levelingUI.SetActive(false);
            return;
        }
        else if (PlayerStats.Instance.isAllowLevelUp == false)
        {
            return;
        }


    }


    public void UpdatePlayerStatsUI()
    {
        uiSoulLevel = PlayerStats.Instance.playerLevel;
        uiSoulsRequirement = PlayerStats.Instance.soulsRequirement;

        uiHealthLevel = PlayerStats.Instance.levelHealth;
        uiStaminaLevel = PlayerStats.Instance.levelStamina;
        uiDamageLevel = PlayerStats.Instance.levelDamage;
        uiDexLevel = PlayerStats.Instance.levelDex;
        uiDiscoveryLevel = PlayerStats.Instance.levelDiscovery;
        uiSoulsCount = PlayerStats.Instance.soulsCount;

        uiHealthCoefficient = PlayerStats.Instance.levelHealthCoefficient;
        uiStaminaCoefficient = PlayerStats.Instance.levelStaminaCoefficient;
        uiDamageCoefficient = PlayerStats.Instance.levelDamageCoefficient;
        uiDexCoefficient = PlayerStats.Instance.levelDexCoefficient;
        uiDiscoveryCoefficient = PlayerStats.Instance.levelDiscoveryCoefficient;
    }

    public void UpdatePlayerStatsDisplay()
    {
        soulsLevel.text = uiSoulLevel.ToString();
        soulsRequirement.text = uiSoulsRequirement.ToString();

        levelHealth.text = uiHealthLevel.ToString();
        levelStamina.text = uiStaminaLevel.ToString();
        levelDamage.text = uiDamageLevel.ToString();
        levelDexterity.text = uiDexLevel.ToString();
        levelLuck.text = uiDiscoveryLevel.ToString();
        soulsCount.text = uiSoulsCount.ToString();

        levelHealthMod.text = "(" + uiHealthCoefficient + ")";
        levelStaminaMod.text = "(" + uiStaminaCoefficient + ")";
        levelDamageMod.text = "(" + uiDamageCoefficient + ")";
        levelDexterityMod.text = "(" + uiDexCoefficient + ")";
        levelLuckMod.text = "(" + uiDiscoveryCoefficient + ")";
    }

    public void UpdateLevelingButtons()
    {
        UpdateDecreaseButtons();
        UpdateIncreaseButtons();
        UpdateConfirmButton();
    }

    public void UpdateDecreaseButtons()
    {
        for (int i = 0; i < decreaseButton.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (uiHealthLevel == PlayerStats.Instance.levelHealth)
                    {
                        decreaseButton[i].interactable = false;
                    }
                    else if (uiHealthLevel > PlayerStats.Instance.levelHealth)
                    {
                        decreaseButton[i].interactable = true;
                    }
                    break;
                case 1:
                    if (uiStaminaLevel == PlayerStats.Instance.levelStamina)
                    {
                        decreaseButton[i].interactable = false;
                    }
                    else if (uiStaminaLevel > PlayerStats.Instance.levelStamina)
                    {
                        decreaseButton[i].interactable = true;
                    }
                    break;
                case 2:
                    if (uiDamageLevel == PlayerStats.Instance.levelDamage)
                    {
                        decreaseButton[i].interactable = false;
                    }
                    else if (uiDamageLevel > PlayerStats.Instance.levelDamage)
                    {
                        decreaseButton[i].interactable = true;
                    }
                    break;
                case 3:
                    if (uiDexLevel == PlayerStats.Instance.levelDex)
                    {
                        decreaseButton[i].interactable = false;
                    }
                    else if (uiDexLevel > PlayerStats.Instance.levelDex)
                    {
                        decreaseButton[i].interactable = true;
                    }
                    break;
                case 4:
                    if (uiDiscoveryLevel == PlayerStats.Instance.levelDiscovery)
                    {
                        decreaseButton[i].interactable = false;
                    }
                    else if (uiDiscoveryLevel > PlayerStats.Instance.levelDiscovery)
                    {
                        decreaseButton[i].interactable = true;
                    }
                    break;
            }
        }
    }

    public void UpdateIncreaseButtons()
    {
        for (int i = 0; i < increaseButton.Length; i++)
        {
            if (uiSoulsCount >= uiSoulsRequirement)
            {
                increaseButton[i].interactable = true;
            }
            else if (uiSoulsCount < uiSoulsRequirement)
            {
                increaseButton[i].interactable = false;
            }
        }
    }

    public void UpdateConfirmButton()
    {
        if (uiSoulLevel > PlayerStats.Instance.playerLevel)
        {
            confirmButton.interactable = true;
        }
        else if (uiSoulLevel <= PlayerStats.Instance.playerLevel)
        {
            confirmButton.interactable = false;
        }
    }

    public void IncreaseLevel(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                uiSoulsCount -= uiSoulsRequirement;
                uiSoulLevel += 1;
                uiHealthLevel += 1;
                RecalculateAllLevel();
                CalculateSoulsRequirement();
                UpdatePlayerStatsDisplay();
                UpdateLevelingButtons();
                break;
            case 1:
                uiSoulsCount -= uiSoulsRequirement;
                uiSoulLevel += 1;
                uiStaminaLevel += 1;
                RecalculateAllLevel();
                CalculateSoulsRequirement();
                UpdatePlayerStatsDisplay();
                UpdateLevelingButtons();
                break;
            case 2:
                uiSoulsCount -= uiSoulsRequirement;
                uiSoulLevel += 1;
                uiDamageLevel += 1;
                RecalculateAllLevel();
                CalculateSoulsRequirement();
                UpdatePlayerStatsDisplay();
                UpdateLevelingButtons();
                break;
            case 3:
                uiSoulsCount -= uiSoulsRequirement;
                uiSoulLevel += 1;
                uiDexLevel += 1;
                RecalculateAllLevel();
                CalculateSoulsRequirement();
                UpdatePlayerStatsDisplay();
                UpdateLevelingButtons();
                break;
            case 4:
                uiSoulsCount -= uiSoulsRequirement;
                uiSoulLevel += 1;
                uiDiscoveryLevel += 1;
                RecalculateAllLevel();
                CalculateSoulsRequirement();
                UpdatePlayerStatsDisplay();
                UpdateLevelingButtons();
                break;
        }
    }

    public void DecreaseLevel(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 0:
                uiSoulLevel -= 1;
                CalculateSoulsRequirement();
                uiSoulsCount += uiSoulsRequirement;
                uiHealthLevel -= 1;
                RecalculateAllLevel();
                UpdatePlayerStatsDisplay();
                UpdateLevelingButtons();
                break;
            case 1:
                uiSoulLevel -= 1;
                CalculateSoulsRequirement();
                uiSoulsCount += uiSoulsRequirement;
                uiStaminaLevel -= 1;
                RecalculateAllLevel();
                UpdatePlayerStatsDisplay();
                UpdateLevelingButtons();
                break;
            case 2:
                uiSoulLevel -= 1;
                CalculateSoulsRequirement();
                uiSoulsCount += uiSoulsRequirement;
                uiDamageLevel -= 1;
                RecalculateAllLevel();
                UpdatePlayerStatsDisplay();
                UpdateLevelingButtons();
                break;
            case 3:
                uiSoulLevel -= 1;
                CalculateSoulsRequirement();
                uiSoulsCount += uiSoulsRequirement;
                uiDexLevel -= 1;
                RecalculateAllLevel();
                UpdatePlayerStatsDisplay();
                UpdateLevelingButtons();
                break;
            case 4:
                uiSoulLevel -= 1;
                CalculateSoulsRequirement();
                uiSoulsCount += uiSoulsRequirement;
                uiDiscoveryLevel -= 1;
                RecalculateAllLevel();
                UpdatePlayerStatsDisplay();
                UpdateLevelingButtons();
                break;
        }
    }

    public void ConfirmLeveling()
    {
        PlayerStats.Instance.playerLevel = uiSoulLevel;
        PlayerStats.Instance.soulsCount = uiSoulsCount;
        PlayerStats.Instance.levelHealth = uiHealthLevel;
        PlayerStats.Instance.levelStamina = uiStaminaLevel;
        PlayerStats.Instance.levelDamage = uiDamageLevel;
        PlayerStats.Instance.levelDex = uiDexLevel;
        PlayerStats.Instance.levelDiscovery = uiDiscoveryLevel;
        PlayerStats.Instance.RecalculateAllLevel();
        PlayerStats.Instance.RecalculateAllStats();
        PlayerStats.Instance.CalculateLevelRequirement();
        UpdatePlayerStatsUI();
        UpdatePlayerStatsDisplay();
        UpdateLevelingButtons();
    }

    public void RecalculateAllLevel()
    {
        CalculateLevelHealth();
        CalculateLevelStamina();
        CalculateLevelDamage();
        CalculateLevelDex();
        CalculateLevelDiscovery();
    }

    public void CalculateSoulsRequirement()
    {
        uiSoulsRequirement = Mathf.RoundToInt(200 * Mathf.Pow(1 + 0.25f, uiSoulLevel - 1));
    }

    public void CalculateLevelHealth()
    {
        uiHealthCoefficient = uiHealthLevel;

        if (uiStaminaLevel >= uiLevelingCoefficient)
        {
            uiHealthCoefficient -= uiStaminaLevel / uiLevelingCoefficient;
        }

        if (uiDamageLevel >= uiLevelingCoefficient)
        {
            uiHealthCoefficient += uiDamageLevel / uiLevelingCoefficient;
        }

        if (uiHealthCoefficient <= 0)
        {
            uiHealthCoefficient = 1;
        }
    }

    public void CalculateLevelStamina()
    {
        uiStaminaCoefficient = uiStaminaLevel;

        if (uiDamageLevel >= uiLevelingCoefficient)
        {
            uiStaminaCoefficient -= uiDamageLevel / uiLevelingCoefficient;
        }

        if (uiDexLevel >= uiLevelingCoefficient)
        {
            uiStaminaCoefficient += uiDexLevel / uiLevelingCoefficient;
        }

        if (uiStaminaCoefficient <= 0)
        {
            uiStaminaCoefficient = 1;
        }
    }

    public void CalculateLevelDamage()
    {
        uiDamageCoefficient = uiDamageLevel;

        if (uiDexLevel >= uiLevelingCoefficient)
        {
            uiDamageCoefficient -= uiDexLevel / uiLevelingCoefficient;
        }

        if (uiHealthLevel >= uiLevelingCoefficient)
        {
            uiDamageCoefficient += uiHealthLevel / uiLevelingCoefficient;
        }

        if (uiDiscoveryLevel >= uiLevelingCoefficient)
        {
            uiDamageCoefficient -= uiDiscoveryLevel / uiLevelingCoefficient;
        }

        if (uiDamageCoefficient <= 0)
        {
            uiDamageCoefficient = 1;
        }
    }

    public void CalculateLevelDex()
    {
        uiDexCoefficient = uiDexLevel;

        if (uiDamageLevel >= uiLevelingCoefficient)
        {
            uiDexCoefficient -= uiDamageLevel / uiLevelingCoefficient;
        }

        if (uiHealthLevel >= uiLevelingCoefficient)
        {
            uiDexCoefficient += uiHealthLevel / uiLevelingCoefficient;
        }

        if (uiDiscoveryLevel >= uiLevelingCoefficient)
        {
            uiDexCoefficient -= uiDiscoveryLevel / uiLevelingCoefficient;
        }

        if (uiDexCoefficient <= 0)
        {
            uiDexCoefficient = 1;
        }
    }

    public void CalculateLevelDiscovery()
    {
        uiDiscoveryCoefficient = uiDiscoveryLevel;
    }


}
