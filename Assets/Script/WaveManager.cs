using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private int wave;
    [SerializeField] private float delayNextWave;


    [Header("Reward Settings")]
    [SerializeField] private GameObject cratePrefab; // Changed to Prefab for multiple waves
    [SerializeField] private Transform crateSpawnPoint; // Where the crate should drop

    private void Start()
    {
        NextWave();
    }


    public void NextWave()
    {
        StartCoroutine(StartWaveCountdown());
    }

    public int ReturnWave()
    {
        Debug.Log("apopopop");
        return wave;
    }

    public void OnWavedFinished()
    {
        SpawnRewardCrate();
        StartCoroutine(StartWaveCountdown());
    }


    IEnumerator StartWaveCountdown()
    {
        yield return new WaitForSeconds(delayNextWave);
        wave++;
        EnemySpawner.instance.StartNextWave();
    }

    private void SpawnRewardCrate()
    {
        if (cratePrefab != null)
        {
            // If using an object pool for your crates, substitute this Instantiate call 
            // with your pooling activation logic!
            Vector3 spawnPos = crateSpawnPoint != null ? crateSpawnPoint.position : transform.position;
            Instantiate(cratePrefab, spawnPos, Quaternion.identity);
        }
    }


}
