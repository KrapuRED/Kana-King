using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Enemy Data")]
    [SerializeField] private EnemySO enemyData;

    private float currentHealth;

    public float CurrentHealth => currentHealth;
    public float Damage => enemyData.damage;

    private void Awake()
    {
        currentHealth = enemyData.maxHealth;
        currentHealth += WaveManager.instance.ReturnWave() / 3;

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

        Destroy(gameObject);
    }

    public EnemySO GetEnemyData()
    {
        return enemyData;
    }
}