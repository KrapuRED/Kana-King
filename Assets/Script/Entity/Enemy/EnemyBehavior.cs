using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;

    private Enemy enemy;
    private EnemySO enemyData;

    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;

    [Header("Reference")]
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        enemy = GetComponent<Enemy>();
        enemyData = enemy.GetEnemyData();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if(spriteRenderer != null)
        {
            RotateSprite();
        }
        rb.linearVelocity = direction * enemyData.speed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (!collision.CompareTag("Player")) return;
        
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Player playerScript = collision.GetComponent<Player>();

            if (playerScript != null)
            {
                playerScript.TakeDamage(enemyData.damage);
                lastAttackTime = Time.time;
            }
        }
    }

    private void RotateSprite()
    {
        if(rb.linearVelocityX > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(rb.linearVelocityX < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}