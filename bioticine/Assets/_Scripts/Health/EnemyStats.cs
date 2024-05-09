using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public HealthBar healthBar;

    private float lastHp;

    [SerializeField] private float maxHp = 100;
    private float hp;
    public float MaxHp => maxHp;

    [SerializeField] private int soulsAmount;

    public float Hp
    {
        get => hp;
        set
        {
            var oldValue = hp;
            var isDamage = value < hp;
            hp = Mathf.Clamp(value, 0, maxHp);
            if (isDamage)
            {
                OnDamaged(hp);
            }
            else
            {
                OnHealed(hp);
            }
            if (hp <= 0)
            {
                OnDied(hp);
            }
            if (hp != oldValue)
            { // Only update if there's a change
                healthBar.UpdateHealthBar(hp - oldValue); // Send difference
            }
        }
    }

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        hp = maxHp;
        lastHp = hp;
    }

    public void Damage(float amount)
    {
        Hp -= amount;
        healthBar.UpdateHealthBar(amount);
    }

    public void Heal(float amount)
    {
        Hp += amount;
        healthBar.UpdateHealthBar(amount);
    }

    public void HealMax()
    {
        Hp = maxHp;
        healthBar.UpdateHealthBar(maxHp);
    }

    public void Kill()
    {
        Hp = 0;
    }

    public void Adjust(float value)
    {
        Hp = value;
        healthBar.UpdateHealthBar(value);
    }

    public void OnDamaged(float newHealth)
    {
        // Add your code here to handle what happens when damaged
        Debug.Log("Damaged: " + newHealth);
    }

    public void OnHealed(float newHealth)
    {
        // Add your code here to handle what happens when healed
        Debug.Log("Healed: " + newHealth);
    }

    public void OnDied(float newHealth)
    {
        
        PlayerStats.Instance.AddSouls(soulsAmount);
        Debug.Log("Died: " + newHealth);
        Destroy(gameObject); // Example: Set the GameObject inactive
    }
}
