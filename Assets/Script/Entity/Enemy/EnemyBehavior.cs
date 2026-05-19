using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;

    private Enemy enemy;
    private EnemySO enemyData;

    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        enemy = GetComponent<Enemy>();
        enemyData = enemy.GetEnemyData();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;

        rb.linearVelocity = direction * enemyData.speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();

            if (playerScript != null)
            {
                playerScript.TakeDamage(enemyData.damage);
                lastAttackTime = Time.time;
            }
        }
    }
}