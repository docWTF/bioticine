using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public float health = 1000;
    public float maxHealth = 1000;
    public float stamina = 500;
    public float maxStamina = 500;
    public GameObject player;

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


    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        Debug.Log("Health: " + health);
    }

    public void UseStamina(float amount)
    {
        stamina -= amount;
        if (stamina < 0)
        {
            stamina = 0;
        }
        Debug.Log("Stamina: " + stamina);
    }

    public void RestoreHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        //implement gradual health regen?
        
        Debug.Log("Health restored to: " + health);
    }

    public void RestoreStamina(float amount)
    {
        //implement stamina regen
    }
}
