using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float damage = 10f;
    public float CurrentHealth => currentHealth;
    public float Damage => damage;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {

    }
}
