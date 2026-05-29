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

    [Header("Shop Panel")]
    [SerializeField] private GameObject shopPanel;


    [SerializeField] private Transform shopItemSpawner;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private int shopItemCount;

    [SerializeField] private List<ShopSO> allShopItems;
    [SerializeField] private List<ShopSO> currentSpawnedItems;


    private void Start()
    {
        allShopItems = ShopDatabase.instance.GetShopItems();
    }


    public void OpenShop()
    {
        shopPanel.SetActive(true);
        SpawnShopItems();
    }


    public void SpawnShopItems()
    {
        currentSpawnedItems.Clear();
        for (int i = shopItemSpawner.childCount - 1; i >= 0; i--)
        {
            Destroy(shopItemSpawner.GetChild(i).gameObject);
        }

        for (int i = 0; i< shopItemCount; i++)
        {
            int randomIndex = GetRandomIndex();
            GameObject x = Instantiate(shopItemPrefab, shopItemSpawner);
            x.GetComponent<ItemShopPrefab>().SetUp(allShopItems[randomIndex]);
            currentSpawnedItems.Add(allShopItems[randomIndex]);
        }
    }


    private int GetRandomIndex()
    {
        int randomIndex = Random.Range(0, allShopItems.Count);

        while (currentSpawnedItems.Contains(allShopItems[randomIndex]))
        {
            randomIndex = Random.Range(0, allShopItems.Count);
        }

        return randomIndex;
    }

    public void BuyItem(ShopSO shopData)
    {
        switch (shopData.modifierType)
        {
            case ShopModifierType.Flat:

                PlayerStat.instance.AddFlatBuff(shopData.statType, shopData.modifierValue);
                break;

            case ShopModifierType.Percentage:

                PlayerStat.instance.AddPercentBuff(shopData.statType, shopData.modifierValue / 100f);

                break;
        }
    }

    public bool CheckPlayerCurrency(int itemPrice)
    {
        int playerCurrency = PlayerStat.instance.ReturnCoin();
        if(playerCurrency >= itemPrice)
        {
            Debug.Log("Berhasil");
            return true;
        }
        else
        {
            Debug.Log("Ga cukup Uang");
            return false;
        }
    }

}
