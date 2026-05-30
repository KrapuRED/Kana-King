using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    public static Player instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }






    [Header("Stats")]
    public float maxHealth => PlayerStat.instance.GetStat(StatType.Health);
    [SerializeField] private float currentHealth;
    [SerializeField] private float damage = 10f;
    public float CurrentHealth => currentHealth;
    public float Damage => damage;


    [Header("EXP & LEVEL")]
    [SerializeField] private float currPlayerExp;
    [SerializeField] private float maxPlayerExp;
    [SerializeField] private int level;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        PlayerUI.instance.HealthUISetUp();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Healing(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        PlayerUI.instance.HealthUISetUp();
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
