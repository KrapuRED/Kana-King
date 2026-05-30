using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BaseStatData
{
    public StatType statType;
    public float baseValue;
}

public class PlayerStat : StatManager
{

    public static PlayerStat instance;

    protected override void Awake()
    {
        base.Awake();

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    [SerializeField] private List<BaseStatData> baseStatDatas = new ();

    [SerializeField] private int coin;

    private void Start()
    {
        foreach(BaseStatData data in baseStatDatas)
        {
            AddBaseValue(data.statType, data.baseValue);
        }
    }

    public void AddCoin(int amount)
    {
        coin += amount;
    }

    public void RemoveCoin(int amount)
    {
        coin -= amount;
    }
    public int ReturnCoin()
    {
        return coin;
    }


}