using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    private Wave currentWave;

    [SerializeField] private Vector2 spawnAreaCenter; // Center of the spawn area
    [SerializeField] private Vector2 spawnAreaSize;   // Size of the spawn area

    private int currentWaveIndex = 0;
    private bool stopSpawning = false;
    private bool isSpawning = false;

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (stopSpawning || isSpawning)
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
            if (currentWaveIndex == 0) // First wave starts, play music
            {
                MusicManager.instance.PlayFightingMusic();
            }
            StartCoroutine(SpawnWave(currentWave));
            currentWaveIndex++;
        }
        else
        {
            stopSpawning = true;
            MusicManager.instance.StopMusic(); // Stop the music when all waves are completed
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true; // Set the flag to true to indicate spawning is in progress

        Debug.Log("Spawning wave: " + currentWaveIndex);

        for (int i = 0; i < wave.NumberToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, wave.EnemiesInWaves.Length);
            Vector3 spawnPosition = GetRandomSpawnPosition();

            Instantiate(wave.EnemiesInWaves[enemyIndex], spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(wave.TimeBetweenSpawns);
        }

        isSpawning = false; // Reset the flag to false as spawning is complete

        // Wait for all enemies to be defeated before starting the next wave
        StartCoroutine(CheckForNextWave());
    }

    private IEnumerator CheckForNextWave()
    {
        while (!AllEnemiesDefeated())
        {
            yield return new WaitForSeconds(1f); // Check every second if all enemies are defeated
        }
        StartNextWave();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2);
        float randomZ = Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2);
        return new Vector3(randomX, 0, randomZ);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the spawn area as a red rectangle in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(spawnAreaCenter.x, 0, spawnAreaCenter.y), new Vector3(spawnAreaSize.x, 0, spawnAreaSize.y));
    }
}
