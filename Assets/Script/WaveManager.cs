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
        return wave;
    }

    public void OnWavedFinished()
    {
        if(wave % 5 == 0)
        {
            ArtefactManager.instance.AddArtefact();
            //Open Artefact
             
        }
        StartCoroutine(StartWaveCountdown());
    }


    IEnumerator StartWaveCountdown()
    {
        wave++;
        yield return new WaitForSeconds(delayNextWave);
        EnemySpawner.instance.StartNextWave();
    }

}
