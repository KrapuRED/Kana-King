using UnityEngine;

public class Player : MonoBehaviour, IEntity
{
    [Header("Stats")]
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _damage = 10f;

    [Header("Health")]
    [SerializeField] private float currentHealth;
    public float maxHealth => _maxHealth;
    public float damage => _damage;

    private void Start()
    {
        currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {

    }
}
