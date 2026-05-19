using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemShopPrefab : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private int price;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemType;
    [SerializeField] private TMP_Text itemDescription;



    public void SetUp(ItemSO itemSO)
    {
        item = itemSO;
        itemName.text = item.name;
        itemType.text = item.ItemCategory.ToString();
        itemDescription.text = item.itemDescription;
    }

    public void Buy()
    {
        if (!ShopManager.instance.CheckPlayerCurrency(price)) return;
        
    }
}
