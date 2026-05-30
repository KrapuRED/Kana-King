using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemScript : MonoBehaviour, IPickUp
{
    [SerializeField] private ItemSO itemSO;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool _inRange = false;
    [SerializeField] private float speed = 10f;
    public bool inRange => _inRange;

    [SerializeField] private Transform playerLocation;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = itemSO.itemImage;
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_inRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerLocation.position, speed * Time.deltaTime);
        }
    }

    public void SetUp(ItemSO x)
    {
        itemSO = x;
        spriteRenderer.sprite = itemSO.itemImage;
    }

    public void InRange()
    {
        _inRange = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("hi");
            Destroy(gameObject);
        }
    }
}
