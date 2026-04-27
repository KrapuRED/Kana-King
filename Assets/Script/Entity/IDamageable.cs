using UnityEngine;

public interface IDamageable
{
    float CurrentHealth {get; }
    float Damage { get; }
    void TakeDamage(float damage);
}
