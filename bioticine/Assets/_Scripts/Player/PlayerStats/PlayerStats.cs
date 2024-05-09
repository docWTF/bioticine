using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public float health = 1000;
    public float maxHealth = 1000;
    public float stamina = 500;
    public float maxStamina = 500;
    public int soulsCount = 0;
    public float weaponDamageMultiplier;
    public GameObject player;
    public PlayerMovement playerMovement;

    public float staminaRegenRate = 30f;  
    public float staminaRegenDelay = 2f; 
    private float staminaRegenTimer = 0;

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
}
