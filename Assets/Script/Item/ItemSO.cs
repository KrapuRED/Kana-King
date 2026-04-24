using UnityEngine;

[System.Serializable]
public enum ItemCategory
{
    Attack,
    Defense,
    Artifact,
    Weapon,
    Skill
}


[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public ItemCategory ItemCategory;
}
