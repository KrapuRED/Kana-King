using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour
{

    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;

    [SerializeField] private float baseDamage;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Attack Cooldown")]
    [SerializeField] private float attackCooldown = 0.5f;
    private bool canAttack;


    public void Attack()
    {
        if (!canAttack)
            return;


        StartCoroutine(AttackCooldown());

        // Detect enemy
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRadius,
            enemyLayer
        );

        // Damage enemy
        foreach (Collider2D enemy in enemies)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(baseDamage);
            }
        }
    }

    // Visualize attack range in Scene
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

}

