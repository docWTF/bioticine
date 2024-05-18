using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusExplosion : MonoBehaviour
{
    public float explosionDamage = 50f; // Adjust the damage value as needed
    public float explosionRadius = 5f; // Adjust the explosion radius as needed

    private void Update()
    {
        // Trigger the explosion effect
        Explode();
    }

    private void Explode()
    {
        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // Loop through each collider found
        foreach (Collider collider in colliders)
        {
            // Check if the collider belongs to the player
            if (collider.CompareTag("Player"))
            {
                // Inflict damage on the player using the PlayerStats component
                collider.GetComponent<PlayerStats>().TakeDamage(explosionDamage);
            }
        }

        // Destroy the virus object after the explosion
        Destroy(gameObject);
    }
}
