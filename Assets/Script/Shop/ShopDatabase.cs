
using System.Collections.Generic;
using UnityEngine;

public class ShopDatabase : MonoBehaviour
{

    public static ShopDatabase instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    [SerializeField] private List<ShopSO> shopItems = new ();


    public List<ShopSO> GetShopItems()
    {
        return shopItems;
    }
}
