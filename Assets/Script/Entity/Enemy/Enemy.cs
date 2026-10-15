using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Data")]
    [SerializeField] private EnemySO enemyData;

    private float currentHealth;

    public float CurrentHealth => currentHealth;
    public float Damage => enemyData.damage;

    [Header("Enemy Drop")]
    [SerializeField] private int coinDrop;
    [SerializeField] private float expDrop;
    [SerializeField] private GameObject expPrefab;

    [Header("VLCC")]
    [SerializeField] private string testName;
    [SerializeField] private float duration = 7f;

    [Header("DamagePopUp")]
    [SerializeField] private GameObject takeDamagePopUp;

    private int n;

    private void Start()
    {
        InitEnemy();
    }

    public void InitEnemy()
    {
        Debug.Log("Enemy Spawned / Re-activated");
        currentHealth = enemyData.maxHealth;

        n = WaveManager.instance.ReturnWave();
        if (n > 1)
        {
            currentHealth = Mathf.CeilToInt(2f * (n / 4f) + (n / 4f) * (n - 4f * (n / 4f) + 1f));
        }

        coinDrop = (int)currentHealth;
        expDrop = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, enemyData.maxHealth);
        InstantiatePopUp(damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void InstantiatePopUp(float damage)
    {
        float randomX = Random.Range(-0.15f, 0.15f);
        Vector3 spawnPosition = transform.position + new Vector3(randomX, 0f, 0f);

        GameObject popUp= Instantiate(takeDamagePopUp, spawnPosition, Quaternion.identity);
        popUp.GetComponent<PopUpDamagePrefab>().SetUpDamagePopUp(damage);
    }

    private void Die()
    {
        Debug.Log(enemyData.enemyName + " Die");
        PlayerStat.instance.AddCoin(coinDrop);
        Player.instance.AddExp(expDrop);
        //GameObject x = Instantiate(expPrefab, transform.position, Quaternion.identity);
        //x.GetComponent<ExpScript>().SetEXPValue(expDrop);
        EnemySpawner.instance.EnemyDeathCount();
        AudioManager.instance.PlaySFX(AudioManager.instance.enemyDeath);

        if(enemyData.enemyType == EnemyType.Boss)
        {
            VLCCManager.instance.VLCCReward += BossReward;
            //VLCCManager.instance.SetDuration(duration, testName);
            VLCCManager.instance.SetDuration(duration);
        }


        Destroy(gameObject);
    }

    public EnemySO GetEnemyData()
    {
        return enemyData;
    }

    private void BossReward()
    {
        ArtefactManager.instance.OpenArtefactManager(ArtefactDatabase.instance.ReturnRandomArtefact());
    }
}