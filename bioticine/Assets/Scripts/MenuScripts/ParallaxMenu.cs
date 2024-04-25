using System.Collections;
using UnityEngine;

public class ParallaxMenu : MonoBehaviour
{
    public float parallaxFactor = 0.02f;
    public float smoothTime = 0.5f;
    public float floatAmplitude = 5f;
    public float floatFrequency = 0.5f;
    public float maxXOffset = 20f;
    public float maxYOffset = 20f;

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private Vector2 velocity = Vector2.zero;

    private float floatTimer;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        // Mouse parallax effect
        Vector2 mousePosition = new Vector2(
            (Input.mousePosition.x / Screen.width) * 2 - 1,
            (Input.mousePosition.y / Screen.height) * 2 - 1);

        // Calculate target position based on mouse position and clamp it within the defined boundaries
        Vector2 parallaxOffset = mousePosition * parallaxFactor;
        targetPosition = startPosition + parallaxOffset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, startPosition.x - maxXOffset, startPosition.x + maxXOffset);
        targetPosition.y = Mathf.Clamp(targetPosition.y, startPosition.y - maxYOffset, startPosition.y + maxYOffset);

        // Floating effect
        floatTimer += Time.deltaTime;
        float floatX = Mathf.Sin(floatTimer * Mathf.PI * floatFrequency) * floatAmplitude;
        float floatY = Mathf.Cos(floatTimer * Mathf.PI * floatFrequency) * floatAmplitude;
        Vector2 floatOffset = new Vector2(floatX, floatY);

        // Smoothly move towards the target position with floating effect, clamped within boundaries
        Vector2 newPos = Vector2.SmoothDamp(transform.localPosition, targetPosition + floatOffset, ref velocity, smoothTime);
        newPos.x = Mathf.Clamp(newPos.x, startPosition.x - maxXOffset, startPosition.x + maxXOffset);
        newPos.y = Mathf.Clamp(newPos.y, startPosition.y - maxYOffset, startPosition.y + maxYOffset);

        // Apply the new position
        transform.localPosition = newPos;
    }
}