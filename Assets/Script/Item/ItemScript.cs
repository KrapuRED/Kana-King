using UnityEngine;

public class ItemScript : MonoBehaviour, IPickUp
{
    [SerializeField] private ItemSO itemSO;
    private SpriteRenderer spriteRenderer;
    private bool comeToPlayer = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSO.itemImage;
    }




    public void SetUp(ItemSO x)
    {
        itemSO = x;
        spriteRenderer.sprite = itemSO.itemImage;
    }

    public void Do()
    {
        comeToPlayer = true;
    }
}
