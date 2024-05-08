using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float hitPoint;

    public void TakeDamage(float damage)
    {
        Debug.Log(gameObject.name + " took damage");
        hitPoint -= damage;

        if (hitPoint <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " died.");
        // make enemy die
    }
}
