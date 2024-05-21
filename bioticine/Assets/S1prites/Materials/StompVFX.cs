using UnityEngine;

public class StompVFX : MonoBehaviour
{
    public ParticleSystem stompParticleSystem; // Reference to the stomp particle system

    // Method to trigger the stomp VFX
    public void TriggerStompVFX()
    {
        // Instantiate the particle system at the current GameObject's position
        Instantiate(stompParticleSystem, transform.position, Quaternion.identity);
    }
}