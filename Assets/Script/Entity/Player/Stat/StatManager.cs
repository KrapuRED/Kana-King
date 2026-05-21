using System.Collections.Generic;
using UnityEngine;
public enum StatType
{
    Attack,
    Defense,
    Health,
    Critical,
    Agility,
    Stamina
}

[System.Serializable]
public class StatData
{
    public StatType statType;

    [Header("Base Stat")]
    public float baseValue;

    [Header("Runtime Buff")]
    [SerializeField] private float flatBonus;
    [SerializeField] private float percentBonus;

    [Header("Debug")]
    [SerializeField] private float totalValue;

    public float TotalValue => totalValue;

    public void UpdateTotalValue()
    {
        totalValue = (baseValue + flatBonus) * (1 + percentBonus);
    }

    // =========================
    // BASE VALUE
    // =========================
    public void AddBaseValue(float amount)
    {
        baseValue += amount;
        UpdateTotalValue();
    }

    // =========================
    // FLAT
    // =========================

    public void AddFlatBuff(float amount)
    {
        flatBonus += amount;
        UpdateTotalValue();
    }

    public void RemoveFlatBuff(float amount)
    {
        flatBonus -= amount;
        UpdateTotalValue();
    }

    // =========================
    // PERCENT
    // =========================

    public void AddPercentBuff(float percent)
    {
        percentBonus += percent;
        UpdateTotalValue();
    }

    public void RemovePercentBuff(float percent)
    {
        percentBonus -= percent;
        UpdateTotalValue();
    }
}

public class StatManager : MonoBehaviour
{
    [SerializeField] protected List<StatData> stats = new();
    protected Dictionary<StatType, StatData> statDictionary;

    protected virtual void Awake()
    {
        statDictionary =  new Dictionary<StatType, StatData>();

        foreach (var stat in stats)
        {
            stat.UpdateTotalValue();

            statDictionary.Add(stat.statType, stat);
        }
    }

    public float GetStat(StatType type)
    {
        return statDictionary[type].TotalValue;
    }

    // =========================
    // BASE VALUE
    // =========================

    public void AddBaseValue(StatType type, float amount)
    {
        statDictionary[type].AddBaseValue(amount);
    }


    // =========================
    // FLAT
    // =========================

    public void AddFlatBuff(StatType type, float amount)
    {
        statDictionary[type].AddFlatBuff(amount);
    }

    public void RemoveFlatBuff(StatType type, float amount)
    {
        statDictionary[type].RemoveFlatBuff(amount);
    }

    // =========================
    // PERCENT
    // =========================

    public void AddPercentBuff(StatType type, float percent)
    {
        statDictionary[type].AddPercentBuff(percent);
    }

    public void RemovePercentBuff(StatType type, float percent)
    {
        statDictionary[type].RemovePercentBuff(percent);
    }
}
