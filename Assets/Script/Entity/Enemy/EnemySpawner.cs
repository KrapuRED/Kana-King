using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private List<EnemySO> normalEnemySO = new();
    [SerializeField] private List<EnemySO> bossEnemySO = new() ;
    [SerializeField] private int baseSpawning = 20;
    [SerializeField] private float delaySpawning = 0.5f;

    [Header("Spawn Limits Per Burst")]
    [SerializeField] private int minimumSpawnInOneTime = 1;
    [SerializeField] private int maximumSpawnInOneTime = 3;

    [Header("Debug")]
    [SerializeField] private int currSpawning = 0;
    [SerializeField] private int currEnemyAlived = 0;
    [SerializeField] private int totalEnemyDefeated = 0;
    [SerializeField] private bool bossWave = false;

    [Header("Reference")]
    [SerializeField] private BossNotification bossNotification;


    private Coroutine spawnCoroutine;


    public void StartNextWave()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }

        currSpawning = 0;
        bossWave = false; // Reset status boss wave di awal wave baru

        int currWave = WaveManager.instance.ReturnWave();

        // Tentukan apakah wave ini adalah boss wave
        if (currWave % 5 == 0)
        {
            bossWave = true;
        }

        int totalEnemiesForWave = baseSpawning + (currWave * 10);

        // JIKA Boss Wave, jumlah musuh yang hidup ditambah 1 (untuk si Boss)
        currEnemyAlived = bossWave ? totalEnemiesForWave + 1 : totalEnemiesForWave;

        spawnCoroutine = StartCoroutine(SpawnWaveRoutine(totalEnemiesForWave));
    }

    private IEnumerator SpawnWaveRoutine(int totalEnemiesForWave)
    {
        // 1. Spawn semua musuh biasa sampai habis
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
                EnemySO normalSO = RandomizeEnemySpawn(normalEnemySO);
                GameObject x = Instantiate(normalSO.enemyPrefab, spawnPosition, Quaternion.identity);
                x.GetComponent<Enemy>().InitEnemy();
            }

            yield return new WaitForSeconds(delaySpawning);
        }

        // 2. DI SINI: Musuh biasa sudah habis di-spawn semua.
        // Jika ini adalah wave boss, spawn boss-nya sekarang!
        if (bossWave)
        {
            bossNotification.PlayBossNotification();
            // Beri sedikit jeda dramatis sebelum boss muncul (opsional)
            yield return new WaitForSeconds(1.5f);

            Vector2 bossSpawnPosition = GetRandomSpawnPosition2D();
            EnemySO bossSO = RandomizeEnemySpawn(bossEnemySO);
            Instantiate(bossSO.enemyPrefab, bossSpawnPosition, Quaternion.identity);

            Debug.Log("Boss telah bangkit!");
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

    private EnemySO RandomizeEnemySpawn(List<EnemySO> enemyData)
    {
        int totalData = enemyData.Count;
        int index = Random.Range(0, totalData);
        return enemyData[index];
    }


    public int ReturnTotalEnemyDefeated()
    {
        return totalEnemyDefeated;
    }

}