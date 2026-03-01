using UnityEngine;

public interface IEntity
{
    float maxHealth {get; }
    float damage { get; }
    void TakeDamage(float damage);
}
