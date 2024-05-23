using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public bool isSceneStart;

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

    public float staminaRegenRate = 30f;  
    public float staminaRegenDelay = 2f; 
    private float staminaRegenTimer = 0;

    public int levelHealth;
    public int levelStamina;
    public int levelDamage;
    public int levelDex;
    public int levelDiscovery;

    public int levelHealthCoeffecient;
    public int levelStaminaCoeffecient;
    public int levelDamageCoeffecient;
    public int levelDexCoeffecient;
    public int levelDiscoveryCoeffecient;
    public int levelingCoeffecient;

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

        RecalculateAllLevel();
        RecalculateAllStats();

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
            if (health < 0)
            {
                health = 0;
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
        Destroy(this);
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

    public void RecalculateHitpoint()
    {
        maxHealth += maxHealth * levelHealthCoeffecient * 0.05f;
    }

    public void RecalculateStamina()
    {
        maxStamina += maxStamina * levelStaminaCoeffecient * 0.1f;
    }

    public void RecalculateDamage()
    {
        weaponDamageMultiplier *= levelDamageCoeffecient; 
    }

    public void RecalculateDex()
    {
        speedMultiplier *= levelDexCoeffecient;
    }

    public void RecalculateDiscovery()
    {
        discoveryMultiplier *= levelDiscoveryCoeffecient;
    }

    public void CalculateLevelHealth()
    {
        levelHealthCoeffecient = levelHealth;

        if (levelStamina >= levelingCoeffecient)
        {
            levelHealthCoeffecient -= levelStamina / levelingCoeffecient;
        }

        if (levelDamage >= levelingCoeffecient)
        {
            levelHealthCoeffecient += levelDamage / levelingCoeffecient;
        }

        if (levelHealthCoeffecient <= 0)
        {
            levelHealthCoeffecient = 1;
        }
    }

    public void CalculateLevelStamina()
    {
        levelStaminaCoeffecient = levelStamina;

        if (levelDamage >= levelingCoeffecient)
        {
            levelStaminaCoeffecient -= levelDamage / levelingCoeffecient;
        }

        if (levelDex >= levelingCoeffecient)
        {
            levelStaminaCoeffecient += levelDex / levelingCoeffecient;
        }

        if (levelStaminaCoeffecient <= 0)
        {
            levelStaminaCoeffecient = 1;
        }
    }

    public void CalculateLevelDamage()
    {
        levelDamageCoeffecient = levelDamage;

        if (levelDex >= levelingCoeffecient)
        {
            levelDamageCoeffecient -= levelDex / levelingCoeffecient;
        }

        if (levelHealth >= levelingCoeffecient)
        {
            levelDamageCoeffecient += levelHealth / levelingCoeffecient;
        }

        if (levelDiscovery >= levelingCoeffecient)
        {
            levelDamageCoeffecient -= levelDiscovery / levelingCoeffecient;
        }

        if (levelDamageCoeffecient <= 0)
        {
            levelDamageCoeffecient = 1;
        }
    }

    public void CalculateLevelDex()
    {
        levelDexCoeffecient = levelDex;

        if (levelDex >= levelingCoeffecient)
        {
            levelDamageCoeffecient -= levelDex / levelingCoeffecient;
        }

        if (levelHealth >= levelingCoeffecient)
        {
            levelDamageCoeffecient += levelHealth / levelingCoeffecient;
        }

        if (levelDiscovery >= levelingCoeffecient)
        {
            levelDexCoeffecient -= levelDiscovery / levelingCoeffecient;
        }

        if (levelDamageCoeffecient <= 0)
        {
            levelDamageCoeffecient = 1;
        }
    }

    public void CalculateLevelDiscovery()
    {
        levelDiscoveryCoeffecient = levelDiscovery;
    }

}
