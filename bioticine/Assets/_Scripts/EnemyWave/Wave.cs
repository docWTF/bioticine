using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Waves", order = 1)]
public class Wave : ScriptableObject
{
    [SerializeField] private GameObject[] enemiesInWaves;
    [SerializeField] private float timeBetweenSpawns = 1f;
    [SerializeField] private int numberToSpawn = 5;

    public GameObject[] EnemiesInWaves { get { return enemiesInWaves; } }
    public float TimeBetweenSpawns { get { return timeBetweenSpawns; } }
    public int NumberToSpawn { get { return numberToSpawn; } }
}
