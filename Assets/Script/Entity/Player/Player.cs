using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float damage = 10f;
    public float CurrentHealth => currentHealth;
    public float Damage => damage;

    [SerializeField] private float currPlayerExp;
    [SerializeField] private float maxPlayerExp;
    [SerializeField] private int level;

    private void Start()
    {
        maxHealth = PlayerStat.instance.GetStat(StatType.Health);
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Die");
        Destroy(gameObject);
    }

    public void AddExp(float amount)
    {
        currPlayerExp += amount;
        if(currPlayerExp >= maxPlayerExp)
        {
            currPlayerExp -= maxPlayerExp;
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        level++;
        ShopManager.instance.OpenShop();
    }
}
