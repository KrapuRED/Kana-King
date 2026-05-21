using UnityEngine;


[System.Serializable]
public enum ShopModifierType
{
    Flat,
    Percentage
}


[CreateAssetMenu(fileName = "ShopSO", menuName = "Scriptable Objects/ShopSO")]
public class ShopSO : ScriptableObject
{
    public Sprite statIcon;
    public string statName;
    public StatType statType;
    public string statDescription;

    public int itemPrice;

    public ShopModifierType modifierType;
    public int modifierValue;
}
