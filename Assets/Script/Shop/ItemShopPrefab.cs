using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ItemShopPrefab : MonoBehaviour
{
    [SerializeField] private ShopSO itemShop;
    [SerializeField] private int price;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemType;
    [SerializeField] private TMP_Text itemDescription;



    public void SetUp(ShopSO shopSO)
    {
        itemShop = shopSO;
        itemName.text = itemShop.name;
        itemType.text = itemShop.statType.ToString();
        itemDescription.text = itemShop.statDescription;
        price = itemShop.itemPrice;
    }

    public void Buy()
    {
        if (!ShopManager.instance.CheckPlayerCurrency(price)) return;
        PlayerStat.instance.RemoveCoin(price);
        ShopManager.instance.BuyItem(itemShop);
        ShopUI.instance.ShopUISetUp();
        Destroy(gameObject);
    }
}
