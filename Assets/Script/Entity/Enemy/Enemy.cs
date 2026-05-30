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


    private void Awake()
    {
        currentHealth = enemyData.maxHealth;
        currentHealth += WaveManager.instance.ReturnWave() / 3;
        coinDrop = ((int)currentHealth);
        expDrop = currentHealth;
        //SpriteRenderer sr = GetComponent<SpriteRenderer>();

        //if (sr != null)
        //{
        //    sr.sprite = enemyData.enemySprite;
        //}
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, enemyData.maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(enemyData.enemyName + " Die");
        PlayerStat.instance.AddCoin(coinDrop);
        GameObject x = Instantiate(expPrefab, transform.position, Quaternion.identity);
        x.GetComponent<ExpScript>().SetEXPValue(expDrop);

        Destroy(gameObject);
    }

    public EnemySO GetEnemyData()
    {
        return enemyData;
    }
}