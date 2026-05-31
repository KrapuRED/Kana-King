using System.Collections;
using UnityEngine;

public class PlayerAttackMelee : MonoBehaviour
{

    public static PlayerAttackMelee instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }





    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;

    [SerializeField] private float baseDamage;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Attack Cooldown")]
    [SerializeField] private float attackCooldown = 0.5f;
    private bool canAttack = true;

    [Header("Buff Artefact")]
    public bool healingAttack = false;
    public float healingPercentageFromAttack;

    [SerializeField] private Animator animator;


    public void Attack()
    {
        if (!canAttack)
            return;

        animator.SetTrigger("onAttack");
        StartCoroutine(AttackCooldown());
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

    private float AttackCalculation()
    {
        // Jadi attack = baseAttack dari weapon * stat yang dimiliki player
        float x = baseDamage + StatCalculationManager.instance.AttackBoost();

        // Jadi dicek nge crit g nya
        if (StatCalculationManager.instance.CritChance())
            x *= 2;

        Debug.Log($"Damage = {x}");
        return x;
    }

    public void DealsDamage()
    {
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
                float n = AttackCalculation();
                damageable.TakeDamage(n);
                if (healingAttack)
                {
                    float healDamage = n * healingPercentageFromAttack/100;
                    Player.instance.Healing(healDamage);
                    Debug.Log($"Healing From Damage {healDamage})");
                }
            }
        }

    }



    public void AddArtefactBuff(float healPercentage)
    {
        healingAttack = true;
        healingPercentageFromAttack = healPercentage;
    }

    public void RemoveArtefactBuff()
    {
        healingAttack = false;
    }
}

