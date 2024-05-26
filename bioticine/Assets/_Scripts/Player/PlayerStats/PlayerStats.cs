using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public int newGamePlus;
    public bool isAllowLevelUp;
    public bool isSceneStart;

    public int playerLevel;
    public int soulsRequirement;
    public float health = 1000;
    public float maxHealth = 1000;
    public float stamina = 500;
    public float maxStamina = 500;
    public int soulsCount = 0;
    public float weaponDamageMultiplier;
    public float discoveryMultiplier;
    public float speedMultiplier;
    public GameObject player;
    public PlayerMovement playerMovement;
    public Animator animator;
    public bool isDead;

    public float staminaRegenRate = 30f;  
    public float staminaRegenDelay = 2f; 
    private float staminaRegenTimer = 0;

    public int levelHealth;
    public int levelStamina;
    public int levelDamage;
    public int levelDex;
    public int levelDiscovery;

    public int levelHealthCoefficient;
    public int levelStaminaCoefficient;
    public int levelDamageCoefficient;
    public int levelDexCoefficient;
    public int levelDiscoveryCoefficient;
    public int levelingCoefficient;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);  
        }

        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();

        RecalculateAllLevel();
        RecalculateAllStats();
        CalculatePlayerLevel();
        CalculateLevelRequirement();

        RestoreHealth(maxHealth -  health);

        isSceneStart = false;
    }

    private void Update()
    {
        if (stamina < maxStamina && staminaRegenTimer <= 0)
        {
            RestoreStamina(staminaRegenRate * Time.deltaTime);
        }
        else if (staminaRegenTimer > 0)
        {
            staminaRegenTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!playerMovement.isDashing)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                isDead = true;
                PlayerDeath();
            }
            Debug.Log("Health: " + health);
        }

    }

    public void UseStamina(float amount)
    {
        stamina -= amount;
        if (stamina < 0)
        {
            stamina = 0;
        }
        staminaRegenTimer = staminaRegenDelay; 

        Debug.Log("Stamina: " + stamina);
    }

    public void RestoreHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        
        Debug.Log("Health restored to: " + health);
    }

    public void RestoreStamina(float amount)
    {
            stamina += amount;
            if (stamina > maxStamina) stamina = maxStamina;
        }

    public void PlayerDeath()
    {
        SceneManager.LoadScene("Death");
        gameObject.SetActive(false);
    }

    public void AddSouls(int amount)
    {
        soulsCount += amount;
    }

    public void SpendSouls(int amount)
    {
        soulsCount -= amount;
    }

    public void RecalculateAllStats()
    {
        RecalculateHitpoint();
        RecalculateStamina();
        RecalculateDamage();
        RecalculateDex();
        RecalculateDiscovery();
    }

    public void RecalculateAllLevel()
    {
        CalculateLevelHealth();
        CalculateLevelStamina();
        CalculateLevelDamage();
        CalculateLevelDex();
        CalculateLevelDiscovery();
    }

    public void CalculatePlayerLevel()
    {
        playerLevel = levelHealth + levelStamina + levelDamage + levelDex + levelDiscovery;
    }

    public void CalculateLevelRequirement()
    {
        soulsRequirement = Mathf.RoundToInt(200 * Mathf.Pow(1 + 0.25f, playerLevel - 1));
    }

    public void RecalculateHitpoint()
    {
        maxHealth += maxHealth * levelHealthCoefficient * 0.05f;
    }

    public void RecalculateStamina()
    {
        maxStamina += maxStamina * levelStaminaCoefficient * 0.1f;
    }

    public void RecalculateDamage()
    {
        weaponDamageMultiplier *= levelDamageCoefficient; 
    }

    public void RecalculateDex()
    {
        speedMultiplier *= levelDexCoefficient;
    }

    public void RecalculateDiscovery()
    {
        discoveryMultiplier *= levelDiscoveryCoefficient;
    }

    public void CalculateLevelHealth()
    {
        levelHealthCoefficient = levelHealth;

        if (levelStamina >= levelingCoefficient)
        {
            levelHealthCoefficient -= levelStamina / levelingCoefficient;
        }

        if (levelDamage >= levelingCoefficient)
        {
            levelHealthCoefficient += levelDamage / levelingCoefficient;
        }

        if (levelHealthCoefficient <= 0)
        {
            levelHealthCoefficient = 1;
        }
    }

    public void CalculateLevelStamina()
    {
        levelStaminaCoefficient = levelStamina;

        if (levelDamage >= levelingCoefficient)
        {
            levelStaminaCoefficient -= levelDamage / levelingCoefficient;
        }

        if (levelDex >= levelingCoefficient)
        {
            levelStaminaCoefficient += levelDex / levelingCoefficient;
        }

        if (levelStaminaCoefficient <= 0)
        {
            levelStaminaCoefficient = 1;
        }
    }

    public void CalculateLevelDamage()
    {
        levelDamageCoefficient = levelDamage;

        if (levelDex >= levelingCoefficient)
        {
            levelDamageCoefficient -= levelDex / levelingCoefficient;
        }

        if (levelHealth >= levelingCoefficient)
        {
            levelDamageCoefficient += levelHealth / levelingCoefficient;
        }

        if (levelDiscovery >= levelingCoefficient)
        {
            levelDamageCoefficient -= levelDiscovery / levelingCoefficient;
        }

        if (levelDamageCoefficient <= 0)
        {
            levelDamageCoefficient = 1;
        }
    }

    public void CalculateLevelDex()
    {
        levelDexCoefficient = levelDex;

        if (levelDex >= levelingCoefficient)
        {
            levelDamageCoefficient -= levelDex / levelingCoefficient;
        }

        if (levelHealth >= levelingCoefficient)
        {
            levelDamageCoefficient += levelHealth / levelingCoefficient;
        }

        if (levelDiscovery >= levelingCoefficient)
        {
            levelDexCoefficient -= levelDiscovery / levelingCoefficient;
        }

        if (levelDamageCoefficient <= 0)
        {
            levelDamageCoefficient = 1;
        }
    }

    public void CalculateLevelDiscovery()
    {
        levelDiscoveryCoefficient = levelDiscovery;
    }

}
