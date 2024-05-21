using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float delay = 2f; // Time in seconds before the bubble is destroyed

    void Start()
    {
        // Call the DestroyBubble method after the specified delay
        Invoke("DestroyBubble", delay);
    }

    void DestroyBubble()
    {
        // Destroy the bubble GameObject
        Destroy(gameObject);
    }
}
