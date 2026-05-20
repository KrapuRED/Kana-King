using UnityEngine;
using System.Collections.Generic;

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
    private float flatBonus;
    private float percentBonus;

    public float TotalValue
    {
        get
        {
            return (baseValue + flatBonus) * (1 + percentBonus);
        }
    }

    // =========================
    // FLAT BUFF
    // =========================

    public void AddFlatBuff(float amount)
    {
        flatBonus += amount;
    }

    public void RemoveFlatBuff(float amount)
    {
        flatBonus -= amount;
    }

    // =========================
    // PERCENT BUFF
    // =========================

    public void AddPercentBuff(float percent)
    {
        percentBonus += percent;
    }

    public void RemovePercentBuff(float percent)
    {
        percentBonus -= percent;
    }
}

public class PlayerStat : MonoBehaviour
{

    public static PlayerStat instance;

    public List<StatData> stats = new List<StatData>();

    private Dictionary<StatType, StatData> statDictionary;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


        statDictionary = new Dictionary<StatType, StatData>();

        foreach (var stat in stats)
        {
            statDictionary.Add(stat.statType, stat);
        }
    }

    public float GetStat(StatType type)
    {
        return statDictionary[type].TotalValue;
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