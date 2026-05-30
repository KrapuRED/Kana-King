using UnityEngine;
using TMPro;
public class ShopUI : MonoBehaviour
{

    public static ShopUI instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private TMP_Text coinText;

    public void ShopUISetUp()
    {
        coinText.text = PlayerStat.instance.ReturnCoin().ToString(); 
    }

}
