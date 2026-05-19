using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private Transform itemShopSpawner;
    [SerializeField] private GameObject itemShopPrefab;
    [SerializeField] private int itemShopCount;


    [Header("Skill Player")]
    private List<string> skillPlayer;

    [Header("Item Attack Player")]
    private List<string> itemAttack;

    [Header("Item Deffense Player")]
    private List<string> itemDeffense;

    private void Spawning()
    {
        for (int i = 0;i< itemShopCount; i++)
        {
            GameObject x = Instantiate(itemShopPrefab, itemShopSpawner);
        }
    }


    public void BuyItem(ItemSO itemSO)
    {
        if(ItemCategory.Weapon == itemSO.ItemCategory)
        {

        }
        else if (ItemCategory.Skill == itemSO.ItemCategory)
        {

        }
        else if (ItemCategory.Defense == itemSO.ItemCategory)
        {

        }
    }

    public bool CheckPlayerCurrency(int itemPrice)
    {
        int playerCurrency = 0; //ambil playerCurrency dari player 
        if(playerCurrency >= itemPrice)
            return true;
        else
            return false;
    }

}
