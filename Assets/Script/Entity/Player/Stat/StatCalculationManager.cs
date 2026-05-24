using UnityEngine;

public class StatCalculationManager : MonoBehaviour
{
    public static StatCalculationManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public float AttackBoost()
    {
        float x = PlayerStat.instance.GetStat(StatType.Attack);
        float boost = x;
        return boost;
    }

    public float DefendDamage()
    {
        float x = PlayerStat.instance.GetStat(StatType.Defense);
        float boost = x;
        return boost;
    }

    public float HealthBoost()
    {
        float x = PlayerStat.instance.GetStat(StatType.Health);
        float boost = x;
        return boost;
    }

    public bool CritChance()
    {
        float x = PlayerStat.instance.GetStat(StatType.Critical);
        float crit = Random.Range(0,x);
        if(crit < x)
            return true;
        else
            return false;
    }

    public float AgilityBoost()
    {
        float x = PlayerStat.instance.GetStat(StatType.Agility);
        float boost = x;
        return boost;
    }
    public float StaminaBoost()
    {
        float x = PlayerStat.instance.GetStat(StatType.Stamina);
        float boost = x;
        return boost;
    }
}
