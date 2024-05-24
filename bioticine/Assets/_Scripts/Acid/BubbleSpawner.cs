using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab; // Reference to the bubble prefab
    public float spawnIntervalMin = 1f; // Minimum time between spawns
    public float spawnIntervalMax = 3f; // Maximum time between spawns

    // Water area bounds
    public Vector2 waterAreaMin;
    public Vector2 waterAreaMax;

    // Platform area bounds
    public Vector2 platformAreaMin;
    public Vector2 platformAreaMax;

    void Start()
    {
        StartCoroutine(SpawnBubbles());
    }

    IEnumerator SpawnBubbles()
    {
        while (true)
        {
            // Wait for a random amount of time before spawning the next bubble
            float spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);

            // Randomly determine the spawn position within the water area and outside the platform area
            Vector3 spawnPosition;
            do
            {
                spawnPosition = new Vector3(
                    Random.Range(waterAreaMin.x, waterAreaMax.x),
                    transform.position.y, // Use the spawner's Y position
                    Random.Range(waterAreaMin.y, waterAreaMax.y)
                );
            } while (IsInsidePlatformArea(spawnPosition));

            // Instantiate the bubble at the spawn position
            Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
        }
    }


    bool IsInsidePlatformArea(Vector2 position)
    {
        return position.x >= platformAreaMin.x && position.x <= platformAreaMax.x &&
               position.y >= platformAreaMin.y && position.y <= platformAreaMax.y;
    }

    void OnDrawGizmos()
    {
        // Draw spawner position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, 0, transform.position.z), 0.5f);

        // Draw water area
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3((waterAreaMin.x + waterAreaMax.x) * 0.5f, 0, (waterAreaMin.y + waterAreaMax.y) * 0.5f),
                            new Vector3(waterAreaMax.x - waterAreaMin.x, 0, waterAreaMax.y - waterAreaMin.y));

        // Draw platform area
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3((platformAreaMin.x + platformAreaMax.x) * 0.5f, 0, (platformAreaMin.y + platformAreaMax.y) * 0.5f),
                            new Vector3(platformAreaMax.x - platformAreaMin.x, 0, platformAreaMax.y - platformAreaMin.y));
    }


}
