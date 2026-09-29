using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using DG.Tweening;

public class ItemShopPrefab : MonoBehaviour
{
    [SerializeField] private ShopSO itemShop;
    [SerializeField] private int price;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemType;
    [SerializeField] private TMP_Text itemDescription;

    [SerializeField] private CanvasGroup canvasGroup;


    public void SetUp(ShopSO shopSO)
    {
        itemShop = shopSO;
        itemName.text = itemShop.name;
        itemIcon.sprite = itemShop.statIcon;
        itemType.text = itemShop.statType.ToString();
        itemDescription.text = itemShop.statDescription;
        price = itemShop.itemPrice;
        canvasGroup.DOKill();
        canvasGroup.DOFade(1f, 1f).SetUpdate(true);
    }

    public void Buy()
    {
        if (!ShopManager.instance.CheckPlayerCurrency(price)) return;
        PlayerStat.instance.RemoveCoin(price);
        ShopManager.instance.BuyItem(itemShop);
        canvasGroup.DOKill();
        canvasGroup.DOFade(0f, 1f).OnComplete(() =>
        {
            ShopUI.instance.ShopUISetUp();
            Destroy(gameObject);
        }).SetUpdate(true);
    }
}
