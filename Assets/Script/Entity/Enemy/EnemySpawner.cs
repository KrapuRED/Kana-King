using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }



    [Header("Player")]
    [SerializeField] private Transform playerLocation;

    [Header("Radius (Jarak dari Player)")]
    [SerializeField] private float minRadius = 10f;
    [SerializeField] private float maxRadius = 15f;

    [Header("Enemy")]
    [SerializeField] private EnemySO normalEnemySO;
    [SerializeField] private EnemySO bossEnemySO;
    [SerializeField] private int baseSpawning = 20;
    [SerializeField] private float delaySpawning = 0.5f;

    [Header("Spawn Limits Per Burst")]
    [SerializeField] private int minimumSpawnInOneTime = 1;
    [SerializeField] private int maximumSpawnInOneTime = 3;

    [Header("Debug")]
    [SerializeField] private int currSpawning = 0;
    [SerializeField] private int currEnemyAlived = 0;
    [SerializeField] private int totalEnemyDefeated = 0;

    private Coroutine spawnCoroutine;

    public void StartNextWave()
    {

        // Jika ada coroutine wave sebelumnya yang masih jalan, matikan dulu supaya tidak double spawner
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }

        currSpawning = 0;        

        int totalEnemiesForWave = baseSpawning + (WaveManager.instance.ReturnWave() * 10);
        currEnemyAlived = totalEnemiesForWave;
        spawnCoroutine = StartCoroutine(SpawnWaveRoutine(totalEnemiesForWave));
    }

    private IEnumerator SpawnWaveRoutine(int totalEnemiesForWave)
    {
        while (currSpawning < totalEnemiesForWave)
        {
            int spawnCount = Random.Range(minimumSpawnInOneTime, maximumSpawnInOneTime + 1);

            if (spawnCount + currSpawning > totalEnemiesForWave)
            {
                spawnCount = totalEnemiesForWave - currSpawning;
            }

            currSpawning += spawnCount;

            for (int i = 0; i < spawnCount; i++)
            {
                Vector2 spawnPosition = GetRandomSpawnPosition2D();
                Instantiate(normalEnemySO.enemyPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(delaySpawning);
        }
    }

    private Vector2 GetRandomSpawnPosition2D()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minRadius, maxRadius);

        Vector2 spawnOffset = randomDirection * randomDistance;
        return (Vector2)playerLocation.position + spawnOffset;
    }

    public void EnemyDeathCount()
    {
        Debug.Log("Hola");
        currEnemyAlived--;
        totalEnemyDefeated++;
        if(currEnemyAlived <= 0)
        {
            WaveManager.instance.OnWavedFinished();
        }
    }


    public int ReturnTotalEnemyDefeated()
    {
        return totalEnemyDefeated;
    }

}