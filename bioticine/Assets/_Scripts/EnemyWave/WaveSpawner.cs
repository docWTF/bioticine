using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    private Wave currentWave;
    [SerializeField] private Transform[] spawnPoints;

    private float timeBetweenSpawn;
    private int currentWaveIndex = 0;
    private bool stopSpawning = false;

    private List<int> occupiedSpawnPointIndices = new List<int>();

    private void Awake()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (stopSpawning)
            return;

        if (AllEnemiesDefeated())
            StartNextWave();
    }

    private bool AllEnemiesDefeated()
    {
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        return totalEnemies.Length == 0;
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            currentWave = waves[currentWaveIndex];
            StartCoroutine(SpawnWave(currentWave));
            currentWaveIndex++;
        }
        else
        {
            stopSpawning = true;
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.NumberToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, wave.EnemiesInWaves.Length);
            int spawnPointIndex = GetAvailableSpawnPointIndex();

            if (spawnPointIndex != -1)
            {
                Instantiate(wave.EnemiesInWaves[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                occupiedSpawnPointIndices.Add(spawnPointIndex);
            }

            yield return new WaitForSeconds(wave.TimeBetweenSpawns);
        }

        occupiedSpawnPointIndices.Clear(); // Clear the list after spawning the wave
    }

    private int GetAvailableSpawnPointIndex()
    {
        List<int> availableIndices = new List<int>();

        // Populate the availableIndices list with indices of spawn points that are not occupied
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!occupiedSpawnPointIndices.Contains(i))
            {
                availableIndices.Add(i);
            }
        }

        if (availableIndices.Count > 0)
        {
            // Return a random index from the list of available indices
            return availableIndices[Random.Range(0, availableIndices.Count)];
        }
        else
        {
            // No available spawn points
            return -1;
        }
    }
}
